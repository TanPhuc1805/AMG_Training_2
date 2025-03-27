using UnityHFSM;
using UnityEngine;

public class ReaperWalkingState : State {
    private readonly Reaper reaper;

    public ReaperWalkingState(Reaper reaper) => this.reaper = reaper;

    public override void OnLogic()
    {
        MoveTowardsKnight();

    }

    void MoveTowardsKnight()
    {
        if (reaper.knight != null)
        {
            // Di chuyển Reaper về phía Knight
            reaper.transform.position = Vector3.MoveTowards(reaper.transform.position, reaper.knight.position, reaper.moveSpeed * Time.deltaTime);

            // Xoay Reaper hướng về Knight
            Vector3 direction = reaper.knight.position - reaper.transform.position;
            if (direction != Vector3.zero)
            {
                reaper.transform.rotation = Quaternion.Slerp(reaper.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 5f);
            }
        }
    }

    public override void OnEnter()
    {
        reaper.Animator.SetBool("isWalking", true);
    }

    public override void OnExit()
    {
        reaper.Animator.SetBool("isWalking", false);
    }
}
