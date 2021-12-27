using UnityEngine;

namespace AnimatedValue
{
	public abstract class AnimValueBase: MonoBehaviour{
		public abstract void SetValue(float progress);
	}
}