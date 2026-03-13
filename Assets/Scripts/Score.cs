using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public static float Points = 0f;
    private float LastPoints = 0f;
    [SerializeField] private TextMeshProUGUI Text;

    void Start()
    {
        LastPoints = 0;
        Points = 0;
    }
    void FixedUpdate()
    {
        LastPoints = Points;
    }
    void Update()
    {
        Text.text = $"Score: {Points}";
    }
}