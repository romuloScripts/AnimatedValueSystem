using TweenSystem;
using UnityEngine;

namespace AnimatedValue
{
	[AddComponentMenu("AnimValue/LocalOffsetValue")]
	public class LocalOffsetValue : AnimValueBase {

		[SerializeField] private float amplitude=1;
		[SerializeField] private EasingCurve curve;
		[SerializeField] private Vector3 localPos,localPos2;

		private void Awake(){
			transform.localPosition = localPos;
		}

		public override void SetValue (float progress){
			transform.localPosition = Vector3.LerpUnclamped(localPos, localPos2, curve.Evaluate (progress))*amplitude;
		}
	}
}
