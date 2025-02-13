using UnityEngine;

namespace Animation
{
    public class FlowerEndState : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);
            animator.GetComponent<FlowerInteraction>().DestroyAllFlower();
            animator.GetComponent<TakePhotoState>().NextScene();
        }
        
        
    }
}