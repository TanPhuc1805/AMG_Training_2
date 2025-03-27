using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityHFSM;

public class KnightIdleState : State {
    private readonly Knight knight;
    public KnightIdleState(Knight knight) => this.knight = knight;
    public override void OnLogic()
    {
        
    }

    public override void OnEnter()
    {
    }

    public override void OnExit()
    {
    }


}