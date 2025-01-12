using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject player;
    public float timeInAir = 5f;
    public float speed = 3f;
    public float turnSpeed = 120f;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("FakePlayer");
        }
    }

    void Start()
    {
        StartCoroutine(SelfDistruck());
    }

    void Update()
    {
        // Make the bullet move and follow player
        rb.velocity = transform.right * speed * 100 * Time.deltaTime;

        float angle = Mathf.Atan2(player.transform.position.y - transform.position.y, player.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);        
    }

    /// <summary>
    /// Destroy bullet after given time
    /// </summary>
    /// <returns></returns>
    IEnumerator SelfDistruck()
    {
        yield return new WaitForSeconds(timeInAir);
        Destroy(gameObject);
    }

    /// <summary>
    /// Destroys bullet if it hits anything
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
