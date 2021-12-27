#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace TweenSystem{
    [CustomPropertyDrawer(typeof(EasingCurve))]
    public class EasingCurveDrawer: PropertyDrawer {

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        
            EditorGUI.BeginProperty(position, label, property);
            var prop = property.FindPropertyRelative(nameof(EasingCurve.easingtype));
            bool showCurve = ShowCurve(prop.enumValueIndex);
            if(showCurve) position.height /=2;
            EditorGUI.PropertyField(position,prop, label);
            ShowCurve(position, property, showCurve);
            EditorGUI.EndProperty();
        }
        
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            int i = property.FindPropertyRelative(nameof(EasingCurve.easingtype)).enumValueIndex;
            return EditorGUIUtility.singleLineHeight * (ShowCurve(i)? 2 : 1);
        }
        
        private bool ShowCurve(int i){
            return i == (int) EasingTypes.custom;
        }

        private static void ShowCurve(Rect position, SerializedProperty property, bool showCurve){
            if (showCurve){
                var newPos = position;
                newPos.y += 20;
                EditorGUI.PropertyField(newPos,
                    property.FindPropertyRelative(nameof(EasingCurve.curve)),
                    new GUIContent("Animation Curve"));
            }
        }
    }
}
#endif
