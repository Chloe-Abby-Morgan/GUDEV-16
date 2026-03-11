using System;
using System.Linq;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private MusicManager Mm;
    [SerializeField] private Sprite[] arrows;
    [SerializeField] private GameObject cardUI;
    public MusicManager.NoteType noteType;
    public Player player;
    public TimingManager Tim;
    private int position;
    private Sprite displayIm;
    private bool[] taken = new bool[5];
    
    
    private void Start()
    {
        Mm = GameObject.FindWithTag("MusicMan").GetComponent<MusicManager>();
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }
    public void add()
    {
        if((noteType == MusicManager.NoteType.Half && position == 4) || taken[position] || (noteType == MusicManager.NoteType.Half && taken[position+1]))
        {
            return;
        }

        Mm.inputNotes.Add(new MusicManager.NoteInterval {noteType = noteType, startBeat = position, direction = LastSelected.instance.lSel, displaySprite = displayIm});

        if(noteType == MusicManager.NoteType.Half)
        {
            taken[position] = true;
            taken[position+1] = true;
        }
        else
        {
            taken[position] = true;
        }

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
        LastSelected.instance.lSel = MusicManager.NoteDirection.Up;
        displayIm = arrows[0];
    }

    public void down()
    {
        LastSelected.instance.lSel = MusicManager.NoteDirection.Down;
        displayIm = arrows[1];
    }

    public void right()
    {
        LastSelected.instance.lSel = MusicManager.NoteDirection.Right;
        displayIm = arrows[2];
    }

    public void left()
    {
        LastSelected.instance.lSel = MusicManager.NoteDirection.Left;
        displayIm = arrows[3];
    }

    public void heal()
    {
        if (player.health < 4)
        {
            player.health += 1;
        }
        Tim.showingUI = false;
    }

}
