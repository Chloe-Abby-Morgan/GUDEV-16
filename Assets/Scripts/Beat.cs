using System.Collections;
using Microsoft.Unity.VisualStudio.Editor;
using NUnit.Framework.Constraints;
using UnityEngine;

public class Beat : MonoBehaviour
{
    [SerializeField] private GameObject note;
    private Vector3 startSize;
    private Baguette baguette;

    private void Start()
    {
        startSize = transform.localScale;
        baguette = GameObject.FindGameObjectWithTag("baguette").GetComponent<Baguette>();
    }

    private void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, startSize, Time.deltaTime * 1f);
    }
    public void beating()
    {
        baguette.canAttack = true;
        transform.localScale = startSize * 2f;
    }
    
    public void endAttack()
    {
        baguette.canAttack = false;
    }
}
