using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

[AddComponentMenu("AnimValue/AnimValue")]
public class AnimValue: ObjectValue {

#region variables
	public AnimationCurve curve = AnimationCurve.Linear(0,0,1,1);
	public float time = 1f;
	public float delayObjectsValue;
	public bool UnscaleTime;
	public bool onStart;
	public UpdateType updateType = UpdateType.Update;

	public enum UpdateType{
		Update,
		Fixed,
		LateUpdate,
	}

	[SerializeField, HideInInspector] float v;   
	public ObjectValue[] objectValue;
	public Events events;

	[System.Serializable]
	public class Events{
		public UnityEvent OnIni; 
		public UnityEvent OnEnd;
	}

	bool playing;
	bool reverse;
	bool loop;
	Coroutine c;

#if UNITY_EDITOR
    [Space(15), Header("Editor"), Space(5)]

    public bool validate=true;
	[Range(0,1)] public float _value = 0f;

	void OnValidate () {
		if (!validate || Application.isPlaying || objectValue == null)
			return;
		setValue(_value);
	}
#endif

#endregion

	void Start(){
		if(onStart)
			ResetAndPlay();
	}

	IEnumerator UpdateCoroutine () {
		StartAnim();
		while(playing){
			yield return null;
			UpdateAnim();
		}
	}

	IEnumerator FixedUpdateCoroutine () {
		StartAnim();
		while(playing){
			yield return new WaitForFixedUpdate();
			UpdateAnim();
		}
	}

	IEnumerator LateUpdateCoroutine () {
		StartAnim();
		while(playing){
			yield return new WaitForEndOfFrame();
			UpdateAnim();
		}
	}

	void StartAnim(){
		playing = true;
		loop = (curve.postWrapMode == WrapMode.Loop || curve.postWrapMode == WrapMode.PingPong);
		if(v == 0){
			events.OnIni.Invoke();
		}else if(v == 1){
			events.OnEnd.Invoke();
		}
	}

	void UpdateAnim(){
		if (!reverse){
			v += (UnscaleTime?Time.unscaledDeltaTime:Time.deltaTime)/time;
			if (v >= 1 && !loop) {
				v = 1;
				events.OnEnd.Invoke ();
				playing = false;
			}
		}else{
			v -= (UnscaleTime?Time.unscaledDeltaTime:Time.deltaTime)/time;
			if (v <= 0 && !loop) {
				v = 0;
				events.OnIni.Invoke();
				playing = false;
			}
		}
		setValue(v);
	}

	private void OnEnable() {
		if(playing){
			Play();
		}
	}

	public override float getValue () {
		return v;
	}
		
	public override void setValue (float value) {
		this.v = value;
		for (int i=0; i<objectValue.Length; i++) {
			if (objectValue [i] != null) {
				float v = curve.Evaluate(value) * Mathf.LerpUnclamped (1, objectValue.Length, delayObjectsValue);
				v =Mathf.Clamp01 (v - (delayObjectsValue * i));
				objectValue [i].setValue (v);
			}
		}
	}

	public void Play() {
		if(c != null)
			StopCoroutine(c);
		switch (updateType)
		{
			case UpdateType.Update:
				c = StartCoroutine(UpdateCoroutine());
				 break;
			case UpdateType.Fixed: 
				c = StartCoroutine(FixedUpdateCoroutine());
				break;
			case UpdateType.LateUpdate: 
				c = StartCoroutine(LateUpdateCoroutine());
				break;
			default: 
				c = StartCoroutine(UpdateCoroutine());
				break;
		}
	}

	public void ResetAndPlay(){
		v = reverse?1:0;
		Play ();
	}

	public void PlayIfNotEnable(){
		if(playing) return;
		ResetAndPlay();
	}

	public void PlayForward(bool resetValue){
		if(reverse){
			reverse = !reverse;
		}
		PlayOrReset (resetValue);
	}

	public void PlayBackward(bool resetValue){
		if(!reverse){
			reverse = !reverse;
		}
		PlayOrReset (resetValue);
	}

	public void RevertDirection(bool resetValue){
		reverse = !reverse;
		PlayOrReset (resetValue);
	}

    public bool IsPlaing() {
        return playing;
    }

	void PlayOrReset(bool resetValue){
		if (resetValue) {
			ResetAndPlay ();
		}else
			Play ();
	}

    public void Pause() {
        playing = false;
    }

	public void Stop() {
        playing = false;
		v = reverse?1:0;
    }

	[ContextMenu("GetChildren")]
	public void GetChildren(){
		List<ObjectValue> obList = new List<ObjectValue>();
     	for (int i = 0; i < transform.childCount; i++){
			 ObjectValue ob = transform.GetChild(i).GetComponent<ObjectValue>();
         	if(ob)
			  obList.Add(ob);
     	}
		if(obList.Count >0)
			objectValue = obList.ToArray();
	}

	[ContextMenu("GetChildrenRevert")]
	public void GetChildrenRevert(){
		List<ObjectValue> obList = new List<ObjectValue>();
     	for (int i = transform.childCount-1; i >=0; i--){
			 ObjectValue ob = transform.GetChild(i).GetComponent<ObjectValue>();
         	if(ob)
			  obList.Add(ob);
     	}
		if(obList.Count >0){
			objectValue = obList.ToArray();
			for (int i = 0; i <objectValue.Length; i++){
				objectValue[i].transform.SetSiblingIndex(i);
			}
		}
	}
}
