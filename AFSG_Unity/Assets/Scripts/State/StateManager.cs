using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    [SerializeField] private List<StateBase> _stateList = new List<StateBase>();
    
    private StateBase _currentState;
    private int _currentStateIndex ;
    void Start()
    {
        foreach (var s in _stateList)
        {
            s.SetOwner(this);
            s.Initialize();
            s.Hide();
        }
        
        _currentState = _stateList[0];
        _currentStateIndex = 0;
        _currentState.Show();
    }

    public void NextScene()
    {
        _currentState.Hide();
        _currentStateIndex++;
        if (_currentStateIndex >= _stateList.Count)
        {
            _currentStateIndex = 0;
        }
        _currentState = _stateList[_currentStateIndex];
        _currentState.Show();
    }
}
