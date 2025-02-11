using UnityEngine;

namespace Animation
{
    public class StopSpawnFlower : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            animator.GetComponent<FlowerInteraction>().ToogleSpawningFlower(false);
        }
    }
}