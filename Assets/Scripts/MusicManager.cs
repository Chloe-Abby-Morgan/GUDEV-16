using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private float bpm;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Image[] beatImages = new Image[4];
    [SerializeField] private Beat[] beatComponents = new Beat[4];
    [SerializeField] private Player player;
    public List<NoteInterval> inputNotes;
    public Sprite quarterNoteSprite;
    public Sprite halfNoteSprite;
    public Sprite halfFollowerSprite;
    public Sprite restSprite;
    private int currentBeatIndex = 0;
    private NoteInterval[] measure = new NoteInterval[4];
    private float nextTriggerTime = 0f;

    private void Start()
    {
        nextTriggerTime = 0f;
    }

    private void Update()
    {
        BuildMeasure();
        AssignUIImages();

        float currentTime = audioSource.time;

        if (currentTime >= nextTriggerTime)
        {
            var interval = measure[currentBeatIndex];

            bool isRealNote = !interval.isRest && !interval.isFollower;

            if (isRealNote)
            {
                interval.Trigger();
                beatComponents[currentBeatIndex].beating();
                player.DashFromMusic(interval.direction);
            }

            float beatLength = 60f / bpm;
            nextTriggerTime += beatLength;
            currentBeatIndex = (currentBeatIndex + 1) % 4;
        }
    }

    private void BuildMeasure()
    {
        for (int i = 0; i < 4; i++)
            measure[i] = null;

        var explicitNotes = new List<NoteInterval>();
        var autoNotes = new List<NoteInterval>();

        foreach (var n in inputNotes)
        {
            if (n.startBeat > 0) explicitNotes.Add(n);
            else autoNotes.Add(n);
        }

        explicitNotes.Sort((a, b) => a.startBeat.CompareTo(b.startBeat));

        foreach (var note in explicitNotes)
        {
            PlaceNote(note);
        }

        int nextFreeBeat = 1;

        foreach (var note in autoNotes)
        {
            int autoBeat = FindNextAvailableBeat(note.noteType, nextFreeBeat);

            if (autoBeat == -1)
            {
                continue;
            }

            note.startBeat = autoBeat;
            PlaceNote(note);
            nextFreeBeat = autoBeat + (note.noteType == NoteType.Half ? 2 : 1);
        }

        for (int i = 0; i < 4; i++)
        {
            if (measure[i] == null)
            {
                measure[i] = CreateInterval(NoteType.Quarter, true, false);
            }
        }
    }

    private void PlaceNote(NoteInterval note)
    {
        int index = note.startBeat - 1;

        if (note.noteType == NoteType.Half)
        {
            if (note.startBeat >= 4)
            {
                return;
            }

            if (measure[index] != null || measure[index + 1] != null)
            {
                return;
            }

            measure[index] = CreateInterval(NoteType.Half, false, false);
            measure[index + 1] = CreateInterval(NoteType.Half, false, true);
            measure[index].direction = note.direction;
            measure[index + 1].direction = note.direction;
        }
        else
        {
            if (measure[index] != null)
            {
                return;
            }

            measure[index] = CreateInterval(NoteType.Quarter, false, false);
            measure[index].direction = note.direction;
        }
    }

    private int FindNextAvailableBeat(NoteType type, int startSearch)
    {
        for (int beat = startSearch; beat <= 4; beat++)
        {
            int index = beat - 1;

            if (type == NoteType.Half)
            {
                if (beat >= 4)
                {
                    return -1;
                }

                if (measure[index] == null && measure[index + 1] == null)
                {
                    return beat;
                }
            }
            else
            {
                if (measure[index] == null)
                {
                    return beat;
                }
            }
        }

        return -1;
    }

    private void AssignUIImages()
    {
        for (int i = 0; i < 4; i++)
        {
            beatImages[i].sprite = measure[i].displaySprite;
            beatImages[i].gameObject.SetActive(true);
        }
    }

    private NoteInterval CreateInterval(NoteType type, bool rest, bool follower)
    {
        var interval = new NoteInterval
        {
            noteType = type,
            isRest = rest,
            isFollower = follower,
            trigger = new UnityEvent(),
            direction = NoteDirection.Up
        };

        if (rest)
        {
            interval.displaySprite = restSprite;
        }
        else if (follower)
        {
            interval.displaySprite = halfFollowerSprite;
        }
        else
        {
            interval.displaySprite = (type == NoteType.Quarter) ? quarterNoteSprite : halfNoteSprite;
        }

        return interval;
    }

    [System.Serializable]
    public class NoteInterval
    {
        public NoteType noteType;
        public bool isRest;
        public bool isFollower;
        public int startBeat;
        public Sprite displaySprite;
        public UnityEvent trigger;
        public NoteDirection direction;

        public void Trigger()
        {
            trigger?.Invoke();
        }
    }

    public enum NoteType
    {
        Quarter, Half
    }

    public enum NoteDirection
    {
        Up, Down, Left, Right
    }
}
