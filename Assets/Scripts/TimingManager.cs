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
    public MusicManager Mm;

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
            if(Mm.inputNotes.Count >= 3)
            {
                var removed = Mm.inputNotes[0];

                Mm.taken[removed.sourcePosition] = false;

                if (removed.noteType == MusicManager.NoteType.Half)
                {
                    Mm.taken[removed.sourcePosition + 1] = false;
                }

                Mm.inputNotes.RemoveAt(0);
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
