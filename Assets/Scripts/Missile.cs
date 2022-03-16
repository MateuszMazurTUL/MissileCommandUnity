using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Projectile
{
    //public delegate void thisDelegate(Vector3 v);
    //public event thisDelegate notify;

    float distanceToDetonationPoint; //it determines how far missile fly / point of explosion

    public void SetDetonationCoord(Vector3 detonationPoint)
    {
        //point where player click on map
        distanceToDetonationPoint = Utility.Pitagoras(transform.position, detonationPoint);
    }

    void checkDetonationPoint()
    {
        //if missile has arrived to detonation point, then uh, explosion
        if (distanceToDetonationPoint <= 0)
        {
            //notify(transform.position);
            PlayerManager.SharedInstance.onDetonationMissile(transform.position);
            gameObject.SetActive(false);
        }
    }

    //override move of projectile, here calculate [distanceToDetonationPoint]
    protected void Move()
    {
        Vector3 tmp = transform.position;

        Vector3 step = transform.up * Time.deltaTime * speed * GlobalSettings.GetTimeSpeed();
        transform.position += step;

        distanceToDetonationPoint -= Utility.Pitagoras(this.transform.position, tmp);
        
    }

    //override move of projectile, additional check detonation condition
    protected void Update()
    {
        Move();
        checkDetonationPoint();
    }
}
