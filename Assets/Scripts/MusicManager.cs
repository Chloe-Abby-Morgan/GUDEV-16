using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private float bpm;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private NoteInterval[] intervals;
    [SerializeField] private Baguette baguette;
    private int currentIndex = 0;
    private float nextTriggerTime = 0f;

    private void Start()
    {
        nextTriggerTime = 0f;
    }

    private void Update()
    {
        if (!audioSource.isPlaying || intervals.Length == 0)
            return;

        float currentTime = audioSource.time;

        if (currentTime >= nextTriggerTime)
        {
            NoteInterval interval = intervals[currentIndex];

            if (!interval.isRest)
            {
                baguette.canAttack = true;
                interval.Trigger();
            }
            else
            {
                baguette.canAttack = false;
            }

            float beatLength = 60f / bpm;
            nextTriggerTime += beatLength * interval.GetBeatMultiplier();

            currentIndex = (currentIndex + 1) % intervals.Length;
        }
    }

    [System.Serializable]
    public class NoteInterval
    {
        public NoteType noteType;
        public bool isRest;
        public UnityEvent trigger;

        public void Trigger()
        {
            trigger.Invoke();
        }

        public float GetBeatMultiplier()
        {
            switch (noteType)
            {
                case NoteType.Eighth: return 0.5f;
                case NoteType.Quarter: return 1f;
                case NoteType.Half: return 2f;
                default: return 1f;
            }
        }
    }

    public enum NoteType
    {
        Eighth,
        Quarter,
        Half
    }
}
