using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private float bpm;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Intervals[] intervals;
    private int lastBeat = -1;

    private void Update()
    {

        float beatLength = 60f / bpm;
        int currentBeat = Mathf.FloorToInt(audioSource.timeSamples / (audioSource.clip.frequency * beatLength));

        if (currentBeat != lastBeat)
        {
            lastBeat = currentBeat;

            int index = currentBeat % intervals.Length;
            intervals[index].Trigger();
        }
    }

    [System.Serializable]
    public class Intervals
    {
        [SerializeField] private UnityEvent trigger;

        public void Trigger()
        {
            trigger.Invoke();
        }
    }
}
