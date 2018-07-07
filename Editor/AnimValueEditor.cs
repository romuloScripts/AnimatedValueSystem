using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AnimValue))]
public class AnimValueEditor : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        if(Application.isPlaying && GUILayout.Button("Play")){
            AnimValue v = (AnimValue)target;
            v.ResetAndPlay();
        }
    }
}