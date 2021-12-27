using UnityEditor;
using UnityEngine;

namespace AnimatedValue
{
	[CustomEditor(typeof(TransformValue)), CanEditMultipleObjects]
	public class TransformValueEditor: Editor {

		private static TransformValue _aux;

		public override void OnInspectorGUI () {
			_aux = (TransformValue)target;
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

		private void ApplyTransform(ref Transform transform, string newObjectName) {
			if (transform==null) {
				transform = new GameObject(newObjectName).transform;
				transform.transform.parent = _aux.transform.parent;
			}

			var auxTransform = _aux.transform;
			transform.position = auxTransform.position;
			transform.rotation = auxTransform.rotation;
			transform.localScale = auxTransform.localScale;	
		}

	}
}
