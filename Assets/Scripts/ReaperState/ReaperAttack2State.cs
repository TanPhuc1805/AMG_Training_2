using UnityHFSM;
using UnityEngine;
using System.Collections;

public class ReaperAttack2State : State
{
    private readonly Reaper reaper;

    // Thêm biến cho thời gian hồi chiêu
    public float cooldownTime = 5f;  // Thời gian hồi chiêu cho Attack2
    private float lastAttackTime = -Mathf.Infinity;  // Lưu thời gian của lần ném cuối cùng

    public ReaperAttack2State(Reaper reaper)
    {
        this.reaper = reaper;
    }

    public override void OnLogic()
    {
        // Kiểm tra thời gian hồi chiêu
        if (Time.time < lastAttackTime + cooldownTime)
        {
            return;  // Nếu chưa hết hồi chiêu, không thực hiện Attack2
        }

        Attack2();  // Nếu thời gian hồi chiêu đã hết, thực hiện Attack2
    }

    // Thực hiện ném lưỡi hái
    private void Attack2()
    {
        reaper.Animator.SetTrigger("Atk2");  // Kích hoạt animation Attack2
        ThrowScythe();  // Ném lưỡi hái theo các hướng khác nhau
        lastAttackTime = Time.time;  // Cập nhật thời gian của lần ném
        reaper.StartCoroutine(AttackCoroutine());  // Kiểm tra hoàn thành animation
    }

    // Ném lưỡi hái theo các hướng khác nhau
    private void ThrowScythe()
    {
        Vector3[] directions = { Vector3.forward, Vector3.back, Vector3.left, Vector3.right };

        // Ném 4 lưỡi hái, mỗi cái ở một hướng
        foreach (var direction in directions)
        {
            // Tạo một lưỡi hái mới và ném nó theo các hướng
            GameObject scythe = GameObject.Instantiate(reaper.scythePrefab, reaper.transform.position, Quaternion.identity);
            scythe.GetComponent<Rigidbody>().AddForce(direction * 20f, ForceMode.Impulse);  // Điều chỉnh tốc độ ném
        }
    }

    // Kiểm tra animation hoàn tất
    private IEnumerator AttackCoroutine()
    {
        yield return new WaitUntil(() => reaper.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);
        
    }

    public override void OnExit()
    {
        reaper.Animator.ResetTrigger("Atk2");  // Reset trigger khi animation hoàn tất
    }
}
