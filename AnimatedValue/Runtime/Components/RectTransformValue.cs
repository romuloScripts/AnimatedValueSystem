using UnityEngine;

namespace AnimatedValue
{
	[AddComponentMenu("AnimValue/RectTransformValue")]
	public class RectTransformValue: AnimValueBase {

		[System.Serializable]
		public struct TransformType {
			public bool position;
			public bool rotation;
			public bool scale;

			public TransformType (bool p, bool r, bool e) {
				position = p;
				rotation = r;
				scale = e;
			}
		}

		public new RectTransform transform;
		public RectTransform p1;
		public RectTransform p2;
		[SerializeField] private AnimationCurve curve = AnimationCurve.Linear(0,0,1,1);
		[SerializeField] private TransformType transformType = new TransformType(true, true, false);

#if UNITY_EDITOR
		[Space(15), Header("Editor"), Space(5)]
		public bool validate=true;
		[Range(0,1)] public float _value = 0f;

		private void OnValidate () {
			if (!validate || p1==null || p2==null || Application.isPlaying) return;
			SetValue(_value);
		}
#endif
		
		private void Reset() {
			if(!transform) transform = base.transform as RectTransform;
		}

		public override void SetValue (float progress) {
			float value = curve.Evaluate(progress);
			if (transformType.position) transform.anchoredPosition = Vector2.LerpUnclamped(p1.anchoredPosition, p2.anchoredPosition, value);
			if (transformType.rotation) transform.rotation = Quaternion.Lerp(p1.rotation, p2.rotation, value);
			if (transformType.scale) transform.localScale = Vector3.LerpUnclamped(p1.localScale, p2.localScale, value);
		}
#if UNITY_EDITOR	
		private void OnDrawGizmos(){
			if(transformType.position && p1 && p2) UnityEditor.Handles.DrawDottedLine(p1.anchoredPosition,p2.anchoredPosition,2);
		}
#endif
	}
}