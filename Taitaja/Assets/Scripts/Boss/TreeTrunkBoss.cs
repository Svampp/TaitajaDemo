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
    float timeCounter;
    int health = 3;
    public bool isHit = false;
    //public bool isAttacking = false;

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject firingSpot;



    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        timeCounter = 0;
    }

    void Update()
    {
        GoAttack();
    }

    /// <summary>
    /// Goes to Idle animation and resets variables
    /// </summary>
    public void GoIdle()
    {
        anim.SetTrigger("Idle");
        timeCounter = 0;
        isHit = false;
    }

    /// <summary>
    /// Counts time and attacks when enough time has passed
    /// </summary>
    void GoAttack()
    {
        timeCounter += Time.deltaTime;
        if (timeCounter > attackSpeed && !isHit)
        {
            timeCounter = 0;
            anim.SetTrigger("Attack");
        }
    }
    /// <summary>
    /// Shoots a bullet
    /// </summary>
    public void Shoot()
    {
        Instantiate(bulletPrefab, firingSpot.transform.position, bulletPrefab.transform.rotation);
    }

    /// <summary>
    /// Boss takes one damage
    /// </summary>
    public void HitTaken()
    {
        isHit = true;
        health--;
        anim.SetTrigger("Hit");
    }
}
