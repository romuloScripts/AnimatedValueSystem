using TweenSystem;
using UnityEngine;

namespace AnimatedValue
{
	[AddComponentMenu("AnimValue/TransformValue")]
	public class TransformValue: AnimValueBase {

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

		[SerializeField] private new Transform transform;
		public Transform p1;
		public Transform p2;
		[SerializeField] private EasingCurve curve;
		[SerializeField] private TransformType transformType = new TransformType(true, true, false);

#if UNITY_EDITOR
		[Space(15), Header("Editor"), Space(5)]
		[SerializeField] private bool validate=true;
		[Range(0,1)] [SerializeField] private float _value = 0f;

		private void OnValidate () {
			if (!validate || p1==null || p2==null || Application.isPlaying)
				return;
			if(!transform) transform = base.transform;
			SetValue(_value);
		}
#endif

		private void Reset() {
			if(!transform) transform = base.transform;
		}

		public override void SetValue (float progress){
			float value = curve.Evaluate(progress);
			if (transformType.position) transform.position = Vector3.LerpUnclamped(p1.position, p2.position, value);
			if (transformType.rotation) transform.rotation = Quaternion.SlerpUnclamped(p1.rotation, p2.rotation, value);
			if (transformType.scale) transform.localScale = Vector3.LerpUnclamped(p1.localScale, p2.localScale, value);
		}
		
#if UNITY_EDITOR		
		private void OnDrawGizmos(){
			if(transformType.position && p1 && p2) UnityEditor.Handles.DrawDottedLine(p1.position,p2.position,2);
		}
	}
#endif
}