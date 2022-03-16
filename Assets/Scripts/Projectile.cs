using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] [Range(0, 40)] protected float speed; //speed of projectiles move

    void Update()
    {
        Move();
    }

    //update position for projectile
    void Move()
    {
        transform.position += transform.up * Time.deltaTime * speed * GlobalSettings.GetTimeSpeed();
    }
}
