using System;
using UnityEngine;

namespace AnimatedValue{
	[AddComponentMenu("AnimValue/AnimatorValue")]
	public class AnimatorFloatValue : AnimValueBase {

		[SerializeField] private string param;
		[SerializeField] private Animator anim;
		[SerializeField] private AnimationCurve curve;
		[SerializeField,Min(0)] private float velSeekValue;
		[SerializeField] private bool unscaleDeltaTime;
	
		private Func<float> _getDeltaTime;
		private float _targetValue;

		private bool UseSeekValue => velSeekValue > 0;
		private float GetScaledDeltaTime() => Time.deltaTime;
		private float GetSUnscaledDeltaTime() => Time.unscaledTime;

		private void Reset() {
			anim = GetComponent<Animator>();
		}

		private void Start(){
			if (!unscaleDeltaTime){
				_getDeltaTime = GetScaledDeltaTime;
			}
			else{
				_getDeltaTime = GetSUnscaledDeltaTime;
			}
		}

		public override void SetValue(float progress) {
			if(!Application.isPlaying) return;
			if (UseSeekValue){
				_targetValue = progress;
			}
			else{
				anim.SetFloat(param, curve.Evaluate(progress));
			}
		}

		private void SeekValue(float value) {
			value = curve.Evaluate(value);
			float f = anim.GetFloat(param);
			f = Mathf.MoveTowards(f,value,velSeekValue*_getDeltaTime());
			anim.SetFloat(param, f);
		}

		private void Update(){
			if (UseSeekValue){
				SeekValue(_targetValue);
			}
		}
	}
}
