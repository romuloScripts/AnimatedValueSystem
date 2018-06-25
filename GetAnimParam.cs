using UnityEngine;
using System.Collections;

public class GetAnimCurve : MonoBehaviour {

	public ObjectValue[] objectValue;
	public string curveName;

	private Animator anim;
	private float value;

	void Start () {
		anim = GetComponent<Animator>();
	}

	void Update () {
		SetValue();
	}

	public void SetValue(){
		value = anim.GetFloat(curveName);
		for (int i=0; i<objectValue.Length; i++) {
			if (objectValue[i]!=null)
				objectValue[i].setValue(value);
		}
	}

}
