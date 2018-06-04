using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor(typeof(RectTransformValue)), CanEditMultipleObjects]
public class RectTransformValueEditor: Editor {

	private static RectTransformValue aux;

	public override void OnInspectorGUI () {
		aux = (RectTransformValue)target;
		base.OnInspectorGUI();
		GUILayout.BeginHorizontal();
		if ( GUILayout.Button("Save p1") ) {
			ApplyTransform(ref aux.p1,aux.name +" p1");
		}
		if ( GUILayout.Button("Save p2") ) {
			ApplyTransform(ref aux.p2,aux.name + " p2");
		}
		GUILayout.EndHorizontal();
	}

	void ApplyTransform (ref RectTransform p, string nome) {
		if (p==null) {
			GameObject obj = new GameObject(nome,typeof(RectTransform));
			p = obj.GetComponent<RectTransform>();
			obj.transform.SetParent(aux.transform.parent);
		}
		p.rotation = aux.transform.rotation;
		p.localScale = aux.transform.localScale;
		p.anchoredPosition = aux.transform.anchoredPosition;
	}

}
