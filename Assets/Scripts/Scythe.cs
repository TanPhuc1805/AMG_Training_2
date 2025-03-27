using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scythe : MonoBehaviour
{
    public float attackDamage = 10f;
    public float destroyDelay = 2f;  // Khoảng thời gian sau đó lưỡi hái sẽ bị destroy (tính bằng giây)
    public float rotationSpeed = 500f;  // Tốc độ xoay của lưỡi hái

    private Rigidbody rb;  // Tham chiếu đến Rigidbody của lưỡi hái

    private void Start()
    {
        rb = GetComponent<Rigidbody>();  // Lấy Rigidbody của lưỡi hái
        if (rb != null)
        {
            // Thêm lực xoay liên tục
            rb.angularVelocity = new Vector3(0f, rotationSpeed, 0f);  // Xoay quanh trục Y
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Gây sát thương cho Knight
            other.GetComponent<Knight>().TakeDamage(attackDamage);

            // Đánh dấu Knight là bị trúng đòn
            other.GetComponent<Knight>().isHit = true;

            Debug.Log("Hit");

            // Tự động destroy lưỡi hái sau một khoảng thời gian
            Destroy(gameObject, destroyDelay);
        }
    }
}
