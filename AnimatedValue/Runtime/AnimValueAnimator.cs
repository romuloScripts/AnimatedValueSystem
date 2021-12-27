using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;
using UnityEngine.Events;

namespace AnimatedValue
{
	[AddComponentMenu("AnimValue/Anim Value Animator")]
	public class AnimValueAnimator: AnimValueBase, IActionMonoBehaviour, IStopActionMonoBehaviour {
		
		#region variables
		[SerializeField] private AnimationCurve curve = AnimationCurve.Linear(0,0,1,1);
		[SerializeField,Range(0,1)] private float offset;
		[SerializeField] private bool unscaleTime;
		[SerializeField] private PlayMode playMode = PlayMode.OnStart;
		[SerializeField] private UpdateMode updateMode = UpdateMode.Update;
		[SerializeField,Tooltip("Send Animation Curve Y value To AnimValueBase Objects")] private List<AnimValueBase> setProgression;
		[SerializeField,Tooltip("Send Animation Curve X value To AnimValueAnimator Objects")] private List<AnimValueAnimator> setTime;
		[SerializeField] private Events events;

		private enum UpdateMode{
			Update,
			Fixed,
			LateUpdate,
		}
		
		private enum PlayMode{
			OnStart,
			OnEnable,
			None,
		}

		[Serializable]
		public class Events{
			public UnityEvent onIni;
			public UnityEvent onEnd;
		}

		[SerializeField, HideInInspector] private float time;   
		private bool _playing;
		private bool _reverse;
		private bool _loop;
		[NonSerialized] private float? _duration;
		[NonSerialized] private float? _end;
		[NonSerialized] private float _begin;
		private Coroutine _c;

		private float DeltaTime => (unscaleTime?Time.unscaledDeltaTime:Time.deltaTime);
		private float GetTime(float progress) => Mathf.Lerp(curve.keys[0].time, curve.keys[curve.keys.Length - 1].time, progress);

		private float End{
			get{
				if (!_end.HasValue){
					_end = curve.keys[curve.keys.Length - 1].time;
				}
				return _end.Value;
			}
		}

#if UNITY_EDITOR
		[Space(15), Header("Editor"), Space(5)]

		[SerializeField] private bool validate=true;
		[Range(0,1),SerializeField] private float value = 0f;

		private void OnValidate () {
			if (!validate || Application.isPlaying || setProgression == null){
				return;
			}
			SetValue(value);
		}
#endif

		#endregion

		private void Start(){
			if(playMode == PlayMode.OnStart)
				ResetAndPlay();
		}
	
		private IEnumerator UpdateCoroutine () {
			StartAnim();
			while(_playing && enabled){
				yield return null;
				UpdateAnim();
			}
		}

		private IEnumerator FixedUpdateCoroutine () {
			StartAnim();
			while(_playing && enabled){
				yield return new WaitForFixedUpdate();
				UpdateAnim();
			}
		}

		private IEnumerator LateUpdateCoroutine () {
			StartAnim();
			while(_playing && enabled){
				yield return new WaitForEndOfFrame();
				UpdateAnim();
			}
		}

		private void StartAnim(){
			_playing = true;
			_loop = (curve.postWrapMode == WrapMode.Loop || curve.postWrapMode == WrapMode.PingPong);
			_begin = 0;
			if(time <= _begin){
				events.onIni.Invoke();
			}else if(time >= End){
				events.onEnd.Invoke();
			}
		}

		private void UpdateAnim(){
			if (!_reverse){
				time += DeltaTime;
				if (time >= End && !_loop){
					time = End;
					events.onEnd.Invoke ();
					_playing = false;
				}
			}else{
				time -= DeltaTime;
				if (time <= _begin && !_loop){
					time = _begin;
					events.onIni.Invoke();
					_playing = false;
				}
			}
			SetTime(time);
		}

		private void OnEnable(){	
			if(playMode == PlayMode.OnEnable)
				ResetAndPlay();
			else if(_playing){
				Play();
			}
		}

		public void AddAnimValue(AnimValueBase animValue){
			setProgression.Add(animValue);
		}

		public override void SetValue (float progress){
			SetTime(GetTime(progress));
		}
		
		private void SetTime(float t){
			time = t;
			float progress = curve.Evaluate(time);
			float delay = Mathf.LerpUnclamped (1, setProgression.Count, offset);
			for (int i=0; i<setProgression.Count; i++) {
				if (setProgression [i] != null) {
					float result = Mathf.Clamp01 ((progress*delay) - (offset * i));
					setProgression[i].SetValue(result);
				}
			}
			
			for (int i=0; i<setTime.Count; i++) {
				if (setTime [i] != null) {
					setTime[i].SetTime(time);
				}
			}
		}

		public void Play() {
			if(_c != null)
				StopCoroutine(_c);
			switch (updateMode)
			{
				case UpdateMode.Update:
					_c = StartCoroutine(UpdateCoroutine());
					break;
				case UpdateMode.Fixed: 
					_c = StartCoroutine(FixedUpdateCoroutine());
					break;
				case UpdateMode.LateUpdate: 
					_c = StartCoroutine(LateUpdateCoroutine());
					break;
				default: 
					_c = StartCoroutine(UpdateCoroutine());
					break;
			}
		}

		public void ResetAndPlay(){
			time = _reverse?End:_begin;
			Play ();
		}

		public void PlayIfNotEnable(){
			if(_playing) return;
			ResetAndPlay();
		}

		public void PlayForward(bool resetValue){
			if(_reverse){
				_reverse = !_reverse;
			}
			PlayOrReset (resetValue);
		}

		public void PlayBackward(bool resetValue){
			if(!_reverse){
				_reverse = !_reverse;
			}
			PlayOrReset (resetValue);
		}

		public void RevertDirection(bool resetValue){
			_reverse = !_reverse;
			PlayOrReset (resetValue);
		}

		public bool IsPlaying() {
			return _playing;
		}

		void PlayOrReset(bool resetValue){
			if (resetValue) {
				ResetAndPlay ();
			}else
				Play ();
		}

		public void Pause(){
			enabled = false;
		}

		public void Resume(){
			enabled = true;
		}

		public void StopAction() {
			_playing = false;
			time = _reverse?End:_begin;
		}

		public void InvokeAction(){
			ResetAndPlay();
		}
	}
}
