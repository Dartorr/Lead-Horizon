using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public delegate void TimeOverDel();
    public TimeOverDel onTimeOver;

    public void TimerStart(float time)
    {
        StartCoroutine(TimerFunction(time));

    }

    public Timer(float t)
    {
        TimerStart(t);
    }

    IEnumerator TimerFunction(float t)
    {
        yield return new WaitForSeconds(t);
        onTimeOver.Invoke();
        Destroy(this);
    }
}
