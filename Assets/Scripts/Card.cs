using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private MusicManager Mm;
    [SerializeField] private AudioSource AS;
    [SerializeField] private Sprite[] arrows;
    [SerializeField] private GameObject cardUI;
    public MusicManager.NoteType noteType;
    public Player player;
    public TimingManager Tim;
    private int position;
    private Sprite displayIm;
    
    
    private void Start()
    {
        Mm = GameObject.FindWithTag("MusicMan").GetComponent<MusicManager>();
    }
    public void add()
{
    if ((noteType == MusicManager.NoteType.Half && position == 4) ||
        Mm.taken[position] ||
        (noteType == MusicManager.NoteType.Half && Mm.taken[position + 1]))
    {
        return;
    }

    Mm.AddNote(new MusicManager.NoteInterval
    {
        noteType = noteType,
        startBeat = position,
        direction = LastSelected.instance.lSel,
        displaySprite = displayIm,
        sourceCard = this,
        sourcePosition = position
    });

    if (noteType == MusicManager.NoteType.Half)
    {
        Mm.taken[position] = true;
        Mm.taken[position + 1] = true;
    }
    else
    {
        Mm.taken[position] = true;
    }
    AS.Play();
    Tim.showingUI = false;
}

    public void quarter()
    {
        noteType = MusicManager.NoteType.Quarter;
    }

    public void half()
    {
        noteType = MusicManager.NoteType.Half;
    }

    public void one()
    {
        position = 1;
    }

    public void two()
    {
        position = 2;
    }

    public void three()
    {
        position = 3;
    }

    public void four()
    {
        position = 4;
    }

    public void up()
    {
        AS.Play();
        LastSelected.instance.lSel = MusicManager.NoteDirection.Up;
        displayIm = arrows[0];
    }

    public void down()
    {
        AS.Play();
        LastSelected.instance.lSel = MusicManager.NoteDirection.Down;
        displayIm = arrows[1];
    }

    public void right()
    {
        AS.Play();
        LastSelected.instance.lSel = MusicManager.NoteDirection.Right;
        displayIm = arrows[2];
    }

    public void left()
    {
        AS.Play();
        LastSelected.instance.lSel = MusicManager.NoteDirection.Left;
        displayIm = arrows[3];
    }

    public void heal()
    {
        AS.Play();
        if (player.health < 4)
        {
            player.health += 1;
        }

        if(Mm.inputNotes.Count > 0)
        {
            Mm.taken[Mm.inputNotes[0].sourcePosition] = false;
            Mm.inputNotes.RemoveAt(0);
        }
        Tim.showingUI = false;
    }

}
