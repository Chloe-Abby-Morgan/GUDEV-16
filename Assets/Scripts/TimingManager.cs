using System.CodeDom.Compiler;
using UnityEngine;

public class TimingManager : MonoBehaviour
{
    [SerializeField] private GameObject cardUI;
    private bool generated;
    public bool showingUI=true;
    public CardManager cM;

    void Update()
    {
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
}
