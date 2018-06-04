using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActiveValue : ObjectValue {

    public AnimationCurve curve;

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

    public override void setValue(float value) {
        float r = curve.Evaluate(value);
        if (r > 0 && !gameObject.activeSelf) {
            gameObject.SetActive(true);
        } else if(r <= 0 && gameObject.activeSelf) {
            gameObject.SetActive(false);
        }
    }
}
