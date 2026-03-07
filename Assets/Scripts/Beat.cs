using System.Collections;
using Microsoft.Unity.VisualStudio.Editor;
using NUnit.Framework.Constraints;
using UnityEngine;

public class Beat : MonoBehaviour
{
    [SerializeField] private GameObject note;
    private Vector3 startSize;

    private void Start()
    {
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
