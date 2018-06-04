using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("AnimValue/FloatValueMultiple")]
public class FloatValueMultiple : ObjectValue {

    public FloatValueEvent[] floatValueEvents;

    [System.Serializable]
    public class FloatValueEvent {
        public string name;
        public float min, max;
        public AnimationCurve curve = AnimationCurve.Linear(0, 0, 1, 1);
        public FloatEvent onSetValue;
    }

    [SerializeField, HideInInspector] private float value;

    [System.Serializable] public class FloatEvent : UnityEvent<float> { }

#if UNITY_EDITOR
    [Space(15), Header("Editor"), Space(5)]
    public bool validate = true;
    [Range(0, 1)] public float _value = 0f;


    void OnValidate() {
        if (!validate || Application.isPlaying)
            return;
        setValue(_value);
    }
#endif

    public override float getValue() {
        return value;
    }

    public override void setValue(float value) {
        for (int i = 0; i < floatValueEvents.Length; i++) {
            FloatValueEvent floatvalue = floatValueEvents[i];
            value = Mathf.Lerp(0, floatvalue.curve.keys[floatvalue.curve.keys.Length - 1].time, value);
            this.value = Mathf.LerpUnclamped(floatvalue.min, floatvalue.max, floatvalue.curve.Evaluate(value));
            floatvalue.onSetValue.Invoke(this.value);
        }
    }
}
