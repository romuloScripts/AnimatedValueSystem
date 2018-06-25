using UnityEngine;
using System.Collections;

public class LocalTranslateValue : ObjectValue {

	public Vector3 distance = Vector3.up;
	public AnimationCurve curve;
	public float frequencia = 1f;

	Vector3 posAnterior;

	void Start () {
		posAnterior = transform.localPosition;
	}

	public override void setValue (float value) {
		transform.localPosition = posAnterior + distance*curve.Evaluate(value * frequencia);
	}

}
