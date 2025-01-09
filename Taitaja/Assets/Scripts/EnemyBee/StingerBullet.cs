using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StingerBullet : MonoBehaviour
{
    public float detonaitonTime = 5; // Time after this object destroys itself

    void Start()
    {
        StartCoroutine(SelfDistruck());
    }

    /// <summary>
    /// Destroy this object after given time
    /// </summary>
    /// <returns></returns>
    IEnumerator SelfDistruck()
    {
        yield return new WaitForSeconds(detonaitonTime);
        Destroy(gameObject);
    }
}
