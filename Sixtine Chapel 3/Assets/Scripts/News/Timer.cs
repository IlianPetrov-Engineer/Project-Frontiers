using System;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public float timeInSeconds;
    public UnityEvent onTimerElapsed;
    
    private float timer;

    private void Start()
    {
        timer = timeInSeconds;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0) { onTimerElapsed.Invoke(); }
    }
}
