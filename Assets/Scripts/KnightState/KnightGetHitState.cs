using UnityHFSM;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KnightGetHitState : State {
    private readonly Knight knight;
    public KnightGetHitState(Knight knight) => this.knight=knight;
    public override void OnEnter()
    {
        
        knight.isHit = false;
        GetHit();
    }
    public override void OnLogic()
    {

    }

    private void GetHit(){

        knight.Animator.SetTrigger("GetHit");
        knight.StartCoroutine(GetHitCoroutine());
    }

    private IEnumerator GetHitCoroutine()
    {
        yield return new WaitUntil(() => knight.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);
    }


    public override void OnExit()
    {
        knight.Animator.ResetTrigger("GetHit");
    }

}