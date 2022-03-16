using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : Projectile
{
    //when meteor contact with explosion of missile then score up
    //if contact other collider deactive
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Explosion")
        {
            GameManager.SharedInstance.OnMeteorShotDown();
        }
        //TODO: BOOM
        gameObject.SetActive(false);
    }
}
