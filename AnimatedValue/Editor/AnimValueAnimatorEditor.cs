using UnityEditor;
using UnityEngine;

namespace AnimatedValue{
    [CustomEditor(typeof(AnimValueAnimator))]
    public class AnimValueAnimatorEditor: Editor {
        public override void OnInspectorGUI() {
            if(Application.isPlaying && GUILayout.Button("Play")){
                AnimValueAnimator v = (AnimValueAnimator)target;
                v.ResetAndPlay();
            }
            base.OnInspectorGUI();
        }
    }
}