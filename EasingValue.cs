using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("AnimValue/EasingValue")]
public class EasingValue : ObjectValue {

	public EasingCurve curve;
	public FloatEvent onChangeValue;

	[System.Serializable]
	public class FloatEvent : UnityEvent<float>{}

#if UNITY_EDITOR
    [Space(10), Header("Editor"), Space(5)]

    public bool validate=true;
	[Range(0,1)] public float _value = 0f;

	void OnValidate () {
		if (!validate || Application.isPlaying)
			return;
		setValue(_value);
	}
#endif

	public override void setValue (float value) {
		onChangeValue.Invoke(curve.Evaluate(value));
	}
}
