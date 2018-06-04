using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("AnimValue/ColorValue")]
public class ColorValue : ObjectValue {

    public Gradient gradient;

    [SerializeField, HideInInspector] private float value;

    [System.Serializable] public class ColorEvent : UnityEvent<Color> { }
    public ColorEvent onSetColor;

#if UNITY_EDITOR
    [Space(15), Header("Editor"), Space(5)]
    public bool validate = true;
    [Range(0, 1)] public float _value = 0f;


    void OnValidate() {
        if (!validate || Application.isPlaying || onSetColor == null)
            return;
        setValue(_value);
    }
#endif

    public override float getValue() {
        return value;
    }

    public override void setValue(float value) {
        onSetColor.Invoke(gradient.Evaluate(value));
    }
}
