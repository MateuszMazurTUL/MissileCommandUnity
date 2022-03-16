using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager SharedInstance; //singleton
    [SerializeField] ObjectPool objectPoolMissiles; // pool of missiles
    [SerializeField] ObjectPool objectPoolExplosion; // pool of explosions
    [SerializeField] Transform cannon; // determinate rotation and dicreton for missiles


    [SerializeField] [Range(0, 6)] float reloadTime = 1; //how fast u can shooting
    float reloadTimeLeft; //left time to next shoot


    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        GameManager.SharedInstance.notify += initStartValues; //set values for start/restart game
        InputController.SharedInstance.notify += onActionButton; // shoot action btn
    }

    void Update()
    {
        this.reloadTimeLeft -= Time.deltaTime * GlobalSettings.GetTimeSpeed(); //update left time of relode(shoot)
    }

    //just shoot new missile from pool
    void NewMissile(Transform spawn, Vector3 detonationPoint)
    {
        GameObject missile = objectPoolMissiles.GetPooledObject();
        if (missile != null)
        {
            missile.transform.position = spawn.position;
            missile.transform.rotation = spawn.rotation;
            missile.GetComponent<Missile>().SetDetonationCoord(detonationPoint);
            missile.SetActive(true);
        }

    }

    //just create explosion from pool
    void NewExplosion(Vector3 spawn)
    {
        GameObject explosion = objectPoolExplosion.GetPooledObject();
        
        if (explosion != null)
        {
            explosion.transform.position = spawn;
            //explosion.transform.rotation = spawn.rotation;
            //explosion.GetComponent<Missile>().SetDetonationCoord(detonationPoint);
            explosion.SetActive(true);
        }
    }

    // shoot action btn
    void onActionButton(Vector3 coord)
    {
        if (ableFireNewMissile())
        {
            this.reloadTimeLeft = this.reloadTime ;
            NewMissile(cannon, coord);
        }
    }

    //when missile has arrived to detonation point
    internal void onDetonationMissile(Vector3 coord)
    {
        NewExplosion(coord);
    }

    //check left reload time
    bool ableFireNewMissile()
    {
        if (this.reloadTimeLeft <= 0 && GlobalSettings.GetTimeSpeed() > 0) // && prevent shoot when pause
            return true;
        else
            return false;
    }

    //set values for start/restart game
    void initStartValues()
    {

        foreach (Transform missile in objectPoolMissiles.gameObject.transform) //deactive missiles
        {
            missile.gameObject.SetActive(false);
        }

        foreach (Transform explosion in objectPoolExplosion.gameObject.transform) //deactive explosions
        {
            explosion.gameObject.SetActive(false);
        }
        
    }
}
