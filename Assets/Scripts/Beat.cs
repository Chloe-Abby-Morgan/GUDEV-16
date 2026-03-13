using System.Collections;
using NUnit.Framework.Constraints;
using UnityEngine;

public class Beat : MonoBehaviour
{
    private Vector3 startSize;
    GameObject note;

    private void Start()
    {
        note = gameObject;
        startSize = transform.localScale;
    }

    private void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, startSize, Time.deltaTime * 1f);
    }
    public void beating()
    {
        transform.localScale = startSize * 2f;
    }
}
