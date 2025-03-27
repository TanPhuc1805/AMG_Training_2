using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightWeapon : MonoBehaviour
{
    public float attackDamage = 10f;
    private void OnTriggerEnter(Collider other)
        {
            // Kiểm tra va chạm với Reaper (hoặc bất kỳ đối tượng nào khác mà Knight tấn công)
            if (other.CompareTag("Enemy"))
            {
                // Gây sát thương cho Reaper
                other.GetComponent<Reaper>().TakeDamage(attackDamage);
            }
        }
}
