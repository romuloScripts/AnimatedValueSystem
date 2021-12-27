using UnityEditor;
using UnityEngine;

namespace AnimatedValue
{
	[CustomEditor(typeof(RectTransformValue)), CanEditMultipleObjects]
	public class RectTransformValueEditor: Editor {

		private static RectTransformValue _aux;

		public override void OnInspectorGUI () {
			_aux = (RectTransformValue)target;
			base.OnInspectorGUI();
			GUILayout.BeginHorizontal();
			if ( GUILayout.Button("Save p1") ) {
				ApplyTransform(ref _aux.p1,_aux.name +" p1");
			}
			if ( GUILayout.Button("Save p2") ) {
				ApplyTransform(ref _aux.p2,_aux.name + " p2");
			}
			GUILayout.EndHorizontal();
		}

		private void ApplyTransform (ref RectTransform targetRectTransform, string newObjectName) {
			if (targetRectTransform==null) {
				GameObject obj = new GameObject(newObjectName,typeof(RectTransform));
				targetRectTransform = obj.GetComponent<RectTransform>();
				obj.transform.SetParent(_aux.transform.parent);
			}
			targetRectTransform.rotation = _aux.transform.rotation;
			targetRectTransform.localScale = _aux.transform.localScale;
			targetRectTransform.anchoredPosition = _aux.transform.anchoredPosition;
		}

	}
}
