using System.CodeDom.Compiler;
using System.Collections;
using UnityEngine;

public class TimingManager : MonoBehaviour
{
    [SerializeField] private GameObject cardUI;
    [SerializeField] private float waitTime=5f;
    private bool generated;
    private bool waiting=false;
    public bool showingUI=true;
    public CardManager cM;

    void Update()
    {
        if(!waiting && !showingUI)
        {
            StartCoroutine(Display());
            waiting = true;
        }
        if(showingUI)
        {
            if(!generated)
            {
                cardUI.SetActive(true);
                cM.SpawnRandomButtons(3);
                generated = true;
            }
        }
        else
        {
            generated = false;
            cardUI.SetActive(false);
        }
    }

    IEnumerator Display()
    {
        yield return new WaitForSeconds(waitTime);
        showingUI = true;
        waiting = false;
    }
}
