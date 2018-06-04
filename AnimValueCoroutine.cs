using UnityEngine;
using System.Collections;
using UnityEngine.Events;

[AddComponentMenu("AnimValue/AnimValueCoroutine")]
public class AnimValueCoroutine: ObjectValue {

#region variables
	public AnimationCurve curve = AnimationCurve.Linear(0,0,1,1);
	public float time = 1f;
	public float delayObjectsValue;
	public bool UnscaleTime,ignorePause=true;

	[SerializeField, HideInInspector] float v;   
	public ObjectValue[] objectValue;
	public UnityEvent OnIni; 
	public UnityEvent OnEnd;
	bool reverse;
	bool loop;

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

	public void OnEnable() {
		loop = (curve.postWrapMode == WrapMode.Loop || curve.postWrapMode == WrapMode.PingPong);
		StartCoroutine(UpdateCoroutine());
	}

	IEnumerator UpdateCoroutine () {
		while(UpdateAnim()){
			yield return null;
		}
		enabled = false;
	}

	bool UpdateAnim(){
		if (!reverse){
			if(UnscaleTime)
				v += Time.unscaledDeltaTime/time * (ignorePause?1:TimeManager.GetInstance().pause);
			else
				v += Time.deltaTime/time;
			if (v >= 1 && !loop) {
				v = 1;
				setValue(v);
				OnEnd.Invoke ();
				return false;
			}
		}else{
			v -= Time.deltaTime/time;
			if (v <= 0 && !loop) {
				v = 0;
				setValue(v);
				return false;
			}
		}
		setValue(v);
		return true;
	}

	public override float getValue () {
		return v;
	}
		
	public override void setValue (float value) {
		this.v = value;
		for (int i=0; i<objectValue.Length; i++) {
			if (objectValue [i] != null) {
				float v = curve.Evaluate(value) * Mathf.LerpUnclamped (1, objectValue.Length, delayObjectsValue);
				objectValue [i].setValue (v - (delayObjectsValue * i));
			}
		}
	}

	public void Play(){
		enabled = true;
	}

	public void ResetAndPlay(){
		if (reverse)
			v = 1;
		else
			v = 0;
		Play ();
		OnIni.Invoke();
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
        return enabled;
    }


	void PlayOrReset(bool resetValue){
		if (resetValue) {
			ResetAndPlay ();
		}else
			Play ();
	}

    public void Stop() {
        enabled = false;
    }
}
