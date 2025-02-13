
using UnityEngine;

public class EyeState : StateBase
{
    public override void Initialize()
    {
        
    }
    
    public void NextScene()
    {
        StateManagerRef.NextScene();
    }
}
