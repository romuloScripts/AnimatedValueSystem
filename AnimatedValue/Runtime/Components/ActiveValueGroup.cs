using UnityEngine;

namespace AnimatedValue
{
    [AddComponentMenu("AnimValue/ActiveValueGroup")]
    public class ActiveValueGroup : AnimValueBase{
        [SerializeField] private AnimationCurve curve =AnimationCurve.Linear(0,0,1,1);
        [SerializeField] private GameObject[] group;

#if UNITY_EDITOR
        [Space(15), Header("Editor"), Space(5)]
        [SerializeField] private bool validate = true;
        [SerializeField,Range(0, 1)] private float value = 0f;

        private void OnValidate() {
            if (!validate || Application.isPlaying) return;
            SetValue(value);
        }
#endif

        public override void SetValue(float progress){
            if(group.Length <= 0) return;
            float r = curve.Evaluate(progress);
            if (r > 0 && !group[0].activeSelf){
                ActiveGroup(true);
            }else if(r <= 0 && group[0].activeSelf) {
                ActiveGroup(false);
            }
        }

        private void ActiveGroup(bool active){
            for (var i = 0; i < group.Length; i++){
                group[i].SetActive(active);
            }
        }
    }
}
