using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeTrunkBoss : MonoBehaviour
{
    [Header("Map variables")]
    public GameObject leftWall;
    // Obsticle set 1
    [SerializeField] GameObject obsticles1;
    [SerializeField] GameObject obsCol1;
    // Obsticle set 2
    [SerializeField] GameObject obsticles2;
    [SerializeField] GameObject obsCol2;

    [Header("Components")]
    Animator anim;

    [Header("Value variables")]
    public float attackSpeed = 5f;
    float timeCounter;
    int health = 3;
    public bool isHit = false;

    [Header("Bullet realted variables")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject firingSpot;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        timeCounter = 0;
        leftWall.SetActive(true);
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
        StartCoroutine(NextStage());
        anim.SetTrigger("Hit");
    }

    /// <summary>
    /// After taking damage start the next stage and make attackSpeed lower
    /// </summary>
    /// <returns></returns>
    IEnumerator NextStage()
    {
        if (health == 2)
        {
            attackSpeed *= 0.7f;
            yield return new WaitForSeconds(4);
            obsticles1.SetActive(true);
            obsCol1.SetActive(true);
        }
        else if (health == 1)
        {
            attackSpeed *= 0.5f;
            obsticles1.SetActive(false);
            obsCol1.SetActive(false);
            yield return new WaitForSeconds(6);
            obsticles2.SetActive(true);
            obsCol2.SetActive(true);
        }
        else if (health == 0)
        {
            attackSpeed *= 100;
            obsticles2.SetActive(false);
            obsCol2.SetActive(false);
            yield return new WaitForSeconds(2f);
            gameObject.SetActive(false);
        }
    }
}
