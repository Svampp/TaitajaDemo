using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeTrunkBoss : MonoBehaviour
{
    [Header("Map variables")]
    public GameObject leftWall;

    [SerializeField] GameObject obsticles1;
    [SerializeField] GameObject obsCol1;

    [SerializeField] GameObject obsticles2;
    [SerializeField] GameObject obsCol2;

    [Header("Component")]
    Animator anim;

    [Header("Value variables")]
    public float attackSpeed = 5f;
    int health = 3;
    public bool isHit = false;
    public bool isAttacking = false;

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject firingSpot;



    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (!isHit && !isAttacking)
        {
            StartCoroutine(GoAttack());
        }
    }

    public void GoIdle()
    {
        anim.SetTrigger("Idle");
        isAttacking = false;
        isHit = false;
    }

    IEnumerator GoAttack()
    {
        isAttacking = true;
        yield return new WaitForSeconds(attackSpeed);
        anim.SetTrigger("Attack");
    }
    public void Shoot()
    {
        Instantiate(bulletPrefab, firingSpot.transform.position, bulletPrefab.transform.rotation);
    }

    public void HitTaken()
    {
        health--;
        isHit = true;
        anim.SetTrigger("Hit");
    }
}
