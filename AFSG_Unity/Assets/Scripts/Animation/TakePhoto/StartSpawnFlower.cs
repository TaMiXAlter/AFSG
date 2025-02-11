
using UnityEngine;

public class StartSpawnFlower : StateMachineBehaviour
{
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<FlowerInteraction>().ToogleSpawningFlower(true);
    }
    
}
