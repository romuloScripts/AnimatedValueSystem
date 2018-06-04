using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("AnimValue/LocalOffsetValue")]
public class LocalOffsetValue : ObjectValue {

	public float amplitude=1;
	public AnimationCurve anim;
	public Vector3 localPos,localPos2;

	Vector3 posIni;

	void Awake(){
		posIni = transform.localPosition;
	}

	public void SetPosIni(Vector3 pos){
		posIni = pos;
	}

	public override void setValue (float value){
		transform.localPosition = posIni + Vector3.LerpUnclamped (localPos, localPos2, anim.Evaluate (value))*amplitude;
	}

    public void SetAmplitude(float n) {
        amplitude = n;
    }
}
