using UnityEngine;
using System.Collections;
using UnityEngine.Events;


[AddComponentMenu("AnimValue/TransformValue")]
public class TransformValue: ObjectValue {

	[System.Serializable]
	public struct TransformType {
		public bool position;
		public bool rotation;
		public bool scale;

		public TransformType (bool p, bool r, bool e) {
			position = p;
			rotation = r;
			scale = e;
		}
	}

	public Transform p1;
	public Transform p2;
	public AnimationCurve curve = AnimationCurve.Linear(0,0,1,1);
	public TransformType transformType = new TransformType(true, true, false);

	[SerializeField, HideInInspector] private float value;

#if UNITY_EDITOR
    [Space(15), Header("Editor"), Space(5)]
    public bool validate=true;
	[Range(0,1)] public float _value = 0f;

	void OnValidate () {
		if (!validate || p1==null || p2==null || Application.isPlaying)
			return;
		setValue(_value);
	}
#endif

	public override float getValue () {
		return value;
	}

	public override void setValue (float value) {
		this.value = Mathf.Lerp (0, curve.keys [curve.keys.Length - 1].time, value);
		if (transformType.position)
			transform.position = Vector3.LerpUnclamped(p1.position, p2.position, curve.Evaluate(this.value));
		if (transformType.rotation)
			transform.rotation = Quaternion.Lerp(p1.rotation, p2.rotation, curve.Evaluate(this.value));
		if (transformType.scale)
			transform.localScale = Vector3.LerpUnclamped(p1.localScale, p2.localScale, curve.Evaluate(this.value));
	}
}