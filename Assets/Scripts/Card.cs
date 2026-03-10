using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private MusicManager Mm;
    public MusicManager.NoteType noteType;
    private int position;
    private bool[] taken = new bool[5];

    private void Start()
    {
        Mm = GameObject.FindWithTag("MusicMan").GetComponent<MusicManager>();
    }
    public void add()
    {
        if((noteType == MusicManager.NoteType.Half && position == 4) || taken[position] || (noteType == MusicManager.NoteType.Half && taken[position+1]))
        {
            return;
        }

        Mm.inputNotes.Add(new MusicManager.NoteInterval {noteType = noteType, startBeat = position});

        if(noteType == MusicManager.NoteType.Half)
        {
            taken[position] = true;
            taken[position+1] = true;
        }
        else
        {
            taken[position] = true;
        }
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
}
