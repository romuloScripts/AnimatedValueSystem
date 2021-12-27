using UnityEngine;

namespace AnimatedValue
{
    [AddComponentMenu("AnimValue/ActiveValue")]
    public class ActiveValue : AnimValueBase {

        [SerializeField] private AnimationCurve curve = AnimationCurve.Linear(0,0,1,1);

#if UNITY_EDITOR
        [Space(15), Header("Editor"), Space(5)]
        public bool validate = true;
        [Range(0, 1)] public float value = 0f;
        
        private void OnValidate() {
            if (!validate || Application.isPlaying)
                return;
            SetValue(value);
        }
#endif

        public override void SetValue(float progress) {
            float r = curve.Evaluate(progress);
            if (r > 0 && !gameObject.activeSelf) {
                gameObject.SetActive(true);
            } else if(r <= 0 && gameObject.activeSelf) {
                gameObject.SetActive(false);
            }
        }
    }
}
