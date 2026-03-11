using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] private float drawtime = 0.6f;
    [SerializeField] private GameObject Prefab;
    [SerializeField] private Transform tis;
    void Start()
    {
        StartCoroutine(SpawnEni());
    }

    IEnumerator SpawnEni()
    {
        yield return new WaitForSeconds(drawtime);
        Instantiate(Prefab, tis.position, tis.rotation);
        Destroy(gameObject);

    }

}