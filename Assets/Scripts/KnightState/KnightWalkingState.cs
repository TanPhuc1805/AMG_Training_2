using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityHFSM;

public class KnightWalkingState : State {
    private readonly Knight knight;
    public KnightWalkingState(Knight knight) => this.knight=knight;

    private float DirX, DirY;
    public override void OnLogic()
    {
        MoveCharacter();
    }

    public override void OnExit()
    {
        knight.Animator.SetBool("IsWalking", false); // chuyển về trạng thái idle
    }

    void MoveCharacter()
    {
        // Lấy giá trị di chuyển từ input của người chơi
        Vector2 movementInput = knight.HeroInputControl.Player.Move.ReadValue<Vector2>();  // Nhận input di chuyển từ PlayerControls
        DirX = movementInput.x;  // Nhận giá trị trục X
        DirY = movementInput.y;  // Nhận giá trị trục Y

        // Tạo vector di chuyển
        Vector3 moveDirection = new Vector3(DirX, 0, DirY).normalized;

        // Di chuyển nhân vật
        knight.Rigidbody.MovePosition(knight.transform.position + moveDirection * knight.moveSpeed * Time.deltaTime);

        // Cập nhật parameter "IsWalking" trong Animator
        knight.Animator.SetBool("IsWalking", true);
        knight.Animator.SetFloat("DirX", DirX);
        knight.Animator.SetFloat("DirY", DirY);

        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);  // Tính toán góc quay từ hướng di chuyển

            // Làm mượt góc quay bằng cách sử dụng Quaternion.RotateTowards
        float turnSpeed = 10f;  // Điều chỉnh tốc độ quay
        knight.transform.rotation = Quaternion.RotateTowards(knight.transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        
    }

        
    
}