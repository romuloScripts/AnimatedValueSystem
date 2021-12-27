using UnityEngine;

namespace AnimatedValue
{
    [AddComponentMenu("AnimValue/AnimatorSpeedValue")]
    public class AnimatorSpeedValue : AnimValueBase{
        [SerializeField] private Animator animator;
        [SerializeField] private AnimationCurve animationCurve = AnimationCurve.Linear(0,0,1,1);

        public override void SetValue(float progress){
            if(!Application.isPlaying) return;
            animator.speed = animationCurve.Evaluate(progress);
        }
    }
}
