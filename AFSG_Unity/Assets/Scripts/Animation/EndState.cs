using UnityEngine;

namespace Animation
{
    public class EndState : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);
            animator.GetComponent<TakePhotoState>().NextScene();
        }
        
        
    }
}