using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(EasingCurve))]
public class EasingCurveDrawer: PropertyDrawer {

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        EditorGUI.BeginProperty(position, label, property);
        EditorGUILayout.PropertyField(property.FindPropertyRelative("easingtype"), new GUIContent("Easing"));
        int i = property.FindPropertyRelative("easingtype").enumValueIndex;
        if(i == (int)easingTypes.custom || i == (int)easingTypes.customCut)
            EditorGUILayout.PropertyField(property.FindPropertyRelative("curve"), new GUIContent("Curve"));
        EditorGUI.EndProperty();
    }
}