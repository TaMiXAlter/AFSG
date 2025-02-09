using UnityEngine;
using UnityEngine.Animations;

namespace Animation
{
    public class TakePhotoBehavior:StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex,
            AnimatorControllerPlayable controller)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex, controller);
            animator.GetComponent<TakePhotoState>().TryShowPhoto();
        }
    }
}