using UnityEngine;
using System.Collections;
using System;

public class Timer : MonoBehaviour
{
        DateTime mLastTime;

        public Timer()
        { }

        public void Start()
        {
            mLastTime = DateTime.Now;
        }

        public float GetElapsedSeconds()
        {
            DateTime now = DateTime.Now;
            TimeSpan elasped = now - mLastTime;
            mLastTime = now;
            return (float)elasped.Ticks / TimeSpan.TicksPerSecond;
        }
    
}
