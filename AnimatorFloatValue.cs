using UnityEngine;
using System.Collections;

[AddComponentMenu("AnimValue/AnimatorValue")]
public class AnimatorFloatValue : ObjectValue {

	public string param;
	public Animator anim;
	public AnimationCurve curve;

	void Reset() {
		anim = GetComponent<Animator>();
	}
	
	public override void setValue(float value) {
		anim.SetFloat(param, curve.Evaluate(value) );
	}
}
