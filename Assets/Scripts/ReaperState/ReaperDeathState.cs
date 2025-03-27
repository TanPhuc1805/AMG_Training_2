using UnityHFSM;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class ReaperDeathState : State {
    private readonly Reaper reaper;
    public ReaperDeathState(Reaper reaper) => this.reaper = reaper;

    private bool hasDied = false;

    public override void OnLogic()
    {
        reaper.Rigidbody.velocity = new Vector3(0, reaper.Rigidbody.velocity.y, 0);
    }
    public override void OnEnter()
    {
        Debug.Log("Dead");
        if (hasDied) return; 
        hasDied = true;
        reaper.Animator.SetTrigger("Dead");
        reaper.StartCoroutine(WaitForDeathAnimation());
    }

    private IEnumerator WaitForDeathAnimation()
    {
        yield return new WaitUntil(() => reaper.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);
        reaper.gameObject.SetActive(false);
    }

    public override void OnExit()
    {
        reaper.Animator.ResetTrigger("Dead");
        reaper.Animator.SetBool("isWalking",false);        
    }

}