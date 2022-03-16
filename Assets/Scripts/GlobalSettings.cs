using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSettings : MonoBehaviour
{
    static float timeSpeed = 1;
    static float timeSpeedNormal = 1;

    [SerializeField] [Range(0, 8)] float timeSpeedSerializeField = 1;

    void Awake()
    {
        timeSpeedNormal = timeSpeedSerializeField;
        timeSpeed = timeSpeedNormal;
        //DontDestroyOnLoad(this.gameObject);
    }

    //override setter, set default time speed
    static public void SetTimeSpeed()
    {
        SetTimeSpeed(timeSpeedNormal);
    }

    //set time speed
    static public void SetTimeSpeed(float newTime)
    {
        if(newTime >= 0 && newTime <= 8)
            timeSpeed = newTime;
    }

    //return time speed
    static public float GetTimeSpeed()
    {
        return timeSpeed;
    }
}
