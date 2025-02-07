using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateBase : MonoBehaviour
{
    protected StateManager StateManagerRef;
    public void SetOwner(StateManager owner) => StateManagerRef = owner;
    public abstract void Initialize();
    public virtual void Show() => gameObject.SetActive(true);
    public virtual void Hide() => gameObject.SetActive(false);
}
