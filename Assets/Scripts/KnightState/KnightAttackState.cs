using UnityHFSM;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KnightAttackState : State {
    private readonly Knight knight;
    public KnightAttackState(Knight knight) => this.knight = knight;
    public override void OnLogic()
    {
        Attack();
        knight.Rigidbody.velocity = new Vector3(0, knight.Rigidbody.velocity.y, 0);
    }
    
    public override void OnEnter()
    {
        knight.Animator.SetBool("IsWalking", false);
    }

    private void Attack(){

        knight.Animator.SetTrigger("Attack");
        knight.StartCoroutine(AttackCoroutine());
    }

    private IEnumerator AttackCoroutine()
    {
        yield return new WaitUntil(() => knight.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);
    }


    public override void OnExit()
    {
        
    }

}