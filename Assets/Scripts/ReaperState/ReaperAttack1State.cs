using UnityHFSM;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ReaperAttack1State : State {
    private readonly Reaper reaper;
    public ReaperAttack1State(Reaper reaper) => this.reaper = reaper;

    public override void OnEnter()
    {
        reaper.Animator.SetBool("isWalking", false);
        Attack1();
    }
    public override void OnLogic()
    {
        reaper.Rigidbody.velocity = new Vector3(0, reaper.Rigidbody.velocity.y, 0);
    }

    private IEnumerator AttackCoroutine()
    {
        yield return new WaitUntil(() => reaper.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);
    }

    private void Attack1(){
        reaper.Animator.SetTrigger("Atk1");
        reaper.StartCoroutine(AttackCoroutine());
    }

    public override void OnExit()
    {
        reaper.Animator.ResetTrigger("Atk1");
    }

}