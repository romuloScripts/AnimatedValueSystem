using UnityEngine;

namespace AnimatedValue
{
    [AddComponentMenu("AnimValue/ScaleShakeValue")]
    public class ScaleShakeValue : AnimValueBase{
        [SerializeField] private new Transform transform;
        [SerializeField] private float modulation=0.1f;
        [SerializeField] private float speed=1f;
        
        private void Reset() {
            if(!transform) transform = base.transform;
        }
        
        public override void SetValue(float progress){
            float add = Mathf.PerlinNoise(Time.time*speed, Time.time*speed) * 2 - 1;
            float value = progress * (1 + add * modulation);
            transform.localScale = Vector3.one * value;
        }
    }
}
