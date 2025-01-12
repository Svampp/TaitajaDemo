using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoleOfDamage : MonoBehaviour
{
    public TreeTrunkBoss TTB;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            TTB.HitTaken();
            gameObject.SetActive(false);
        }
    }
}
