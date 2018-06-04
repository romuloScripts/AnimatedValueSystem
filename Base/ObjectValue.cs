﻿using UnityEngine;
using System.Collections;
using UnityEngine.Events;

[System.Serializable]
public class OnSetValue: UnityEvent<float>{}

public abstract class ObjectValue : MonoBehaviour {

	public virtual void setValue (float value) {}

	public virtual float getValue () {
		return 0f;
	}

}
