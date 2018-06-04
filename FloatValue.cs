using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("AnimValue/FloatValue")]
public class FloatValue : ObjectValue {

	public float min, max;
	public AnimationCurve curve = AnimationCurve.Linear(0,0,1,1);

	[SerializeField, HideInInspector] private float value;

	[System.Serializable] public class FloatEvent: UnityEvent<float>{}
	public FloatEvent onSetValue;

#if UNITY_EDITOR
    [Space(15), Header("Editor"), Space(5)]
    public bool validate=true;
	[Range(0,1)] public float _value = 0f;


	void OnValidate () {
		if (!validate || Application.isPlaying || onSetValue == null)
			return;
		setValue(_value);
	}
#endif

	public override float getValue () {
		return value;
	}

	public override void setValue (float value) {
		value = Mathf.Lerp (0, curve.keys [curve.keys.Length - 1].time, value);
		this.value = Mathf.LerpUnclamped (min, max, curve.Evaluate (value));
		onSetValue.Invoke(this.value);
	}
}
