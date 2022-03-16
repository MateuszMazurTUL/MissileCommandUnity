using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] float startSize; // initial size of explosion
    [SerializeField] float power; // how fast explosion grow
    [SerializeField] float duration; // how long explosion exist, const
    float currentDuration;// left time of explosion exist

    void OnEnable()
    {
        transform.localScale = new Vector3(startSize,startSize,1); // init size of sphere explosion on create
        currentDuration = duration;
    }

    void Update()
    {
        //destroy this.object after time[currentDuration]
        if (this.currentDuration > 0)
            scaleExplosion();
        else 
            gameObject.SetActive(false);

        this.currentDuration -= Time.deltaTime * GlobalSettings.GetTimeSpeed();
    }

    void scaleExplosion()
    {
        //grow up sphere explosion
        float newScale = transform.localScale.x + transform.localScale.x * power * Time.deltaTime * GlobalSettings.GetTimeSpeed();
        transform.localScale = new Vector3(newScale, newScale, 1);
    }
}
