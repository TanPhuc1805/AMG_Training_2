using UnityHFSM;
using UnityEngine;

public class ReaperSpawnState : State {
    private readonly Reaper reaper;
    public ReaperSpawnState(Reaper reaper) => this.reaper = reaper;

    public override void OnEnter()
    {
        Debug.Log("Spawn");
        //reaper.Animator.SetTrigger("Spawn");
    }
    public override void OnLogic()
    {
       reaper.Rigidbody.velocity = new Vector3(0, reaper.Rigidbody.velocity.y, 0);
    }

    private void Spawn(){
        
    }

    public override void OnExit()
    {
        //reaper.Animator.ResetTrigger("Spawn");
        
    }

}