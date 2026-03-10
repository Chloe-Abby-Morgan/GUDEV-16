using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private MusicManager Mm;
    public MusicManager.NoteType noteType;
    private int position;

    private void Start()
    {
        Mm = GameObject.FindWithTag("MusicMan").GetComponent<MusicManager>();
    }
    public void add()
    {
        Mm.inputNotes.Add(new MusicManager.NoteInterval {noteType = noteType, startBeat = position});
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
