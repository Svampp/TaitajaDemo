using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBee : MonoBehaviour
{
    public bool attack; // Is bee attacking
    [SerializeField] GameObject stingerBullet; // Bullet to shoot in the Shoot method

    Animator anim;


    void Start()
    {
        anim = GetComponent<Animator>();
        attack = true;
    }

    void Update()
    {
        if (attack)
        {
            anim.SetBool("IsAttack", true);
        }
        else
        {
            anim.SetBool("IsAttack", false);
        }
    }

    /// <summary>
    /// Instantiates a bee stinger bulletPrefab
    /// </summary>
    public void Shoot()
    {
        Instantiate(stingerBullet, transform.position, stingerBullet.transform.rotation);
    }
}
