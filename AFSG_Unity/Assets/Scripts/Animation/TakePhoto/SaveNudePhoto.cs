using UnityEngine;

namespace Animation
{
    public class SaveNudePhoto : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);
            animator.GetComponent<TakePhotoState>().TrySaveAIPhoto();
        }
        
    }
}