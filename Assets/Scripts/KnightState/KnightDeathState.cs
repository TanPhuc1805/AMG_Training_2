using UnityHFSM;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KnightDeathState : State {
    private readonly Knight knight;
    public KnightDeathState(Knight knight) => this.knight=knight;

    private bool hasDied = false;
    
    public override void OnEnter()
    {
        Debug.Log("Dead");
        if (hasDied) return; 
        hasDied = true;
        knight.Animator.SetTrigger("Dead");
        knight.StartCoroutine(WaitForDeathAnimation());
    }

    private IEnumerator WaitForDeathAnimation()
    {
        yield return new WaitUntil(() => knight.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);
        knight.gameObject.SetActive(false);
    }

    public override void OnLogic()
    {
        knight.Rigidbody.velocity = new Vector3(0, knight.Rigidbody.velocity.y, 0);
    }

    public override void OnExit()
    {
        knight.Animator.ResetTrigger("Dead");        
    }

}