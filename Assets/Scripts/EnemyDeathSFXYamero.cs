using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathSFXYamero : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(Yamero());
    }

    IEnumerator Yamero()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
