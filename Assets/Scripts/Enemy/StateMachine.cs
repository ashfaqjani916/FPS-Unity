using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{

    public BaseState activeState;

    public void Initialise()
    {
        ChangeState( new PatrolState());
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         if(activeState != null)
        {
           activeState.Perform();
        }
        
    }
    public void ChangeState(BaseState newState)
    {
        // check if active state is not null
        if(activeState != null)
        {
            // run cleanup on active state.
            activeState.Exit();
        }
        // change to a new state.
        activeState = newState;

        // fail-safe null check to make sure new state was not null.
        if(activeState != null)
        {
            //setup new state.
            activeState.stateMachine = this;
            activeState.enemy = GetComponent<Enemy>();
            activeState.Enter();
        }
    }
}
