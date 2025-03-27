using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperWeapon : MonoBehaviour
{
    public float attackDamage = 5f;
    private void OnTriggerEnter(Collider other)
        {
            
            if (other.CompareTag("Player"))
            {
                
                other.GetComponent<Knight>().TakeDamage(attackDamage);
                other.GetComponent<Knight>().isHit = true;
                Debug.Log("Hit");
            }
        }
}
