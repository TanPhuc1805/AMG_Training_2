using UnityHFSM;

public class KnightDashState : State {
    private readonly Knight knight;
    public KnightDashState(Knight knight) => this.knight=knight;
    public override void OnLogic()
    {
        knight.Animator.SetTrigger("Dash");
    }


}