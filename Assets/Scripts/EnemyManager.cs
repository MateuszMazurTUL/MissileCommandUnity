using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager SharedInstance; //singleton

    [SerializeField] ObjectPool objectPool; // pool of meteors

    [SerializeField] GameObject homePool; // parent gameobject of homes
    [SerializeField] List<GameObject> homes; //list of homes

    [SerializeField] [Range(0,6)] float spawnTime; //period time of spawn new meteor, const
    [SerializeField] [Range(0, 12)] float shiftMaxMeteorSpawn; //shift spawn for new meteor in x axis
    [SerializeField] [Range(0, 12)] float shiftHomeMissMeteorSpawn; //meteor will target home or miss it

    float time; //time left to spawn meteore

    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        GameManager.SharedInstance.notify += initStartValues; //set values for start/restart game

        foreach (GameObject home in homes)
            home.GetComponent<Home>().notify += OnHomeDestroy; //observe destroy home, 'cuz meteor target active homes

        this.time = this.spawnTime;
    }

    void FixedUpdate()
    {
        //if time to spawn then spawn new meteore
        if (time <= 0)
        {
            NewMeteor(this.transform);
            time = this.spawnTime;
        }
        //decresre time to spawn left
        time -= Time.fixedDeltaTime * GlobalSettings.GetTimeSpeed();
    }

    void NewMeteor(Transform spawn)
    {
        //random values of meteor's spawn
        float randX = Random.Range(-shiftMaxMeteorSpawn, shiftMaxMeteorSpawn); //shift in x axis of spawn
        float randR = Random.Range(-shiftHomeMissMeteorSpawn, shiftHomeMissMeteorSpawn); //shift in x axis of target, meteor can miss home
        int randH = Random.Range(0, homes.Count - 1); //random target home

        //temp variables of spwan and target
        Vector3 newSpawn = new Vector3(spawn.position.x + randX, spawn.position.y, spawn.position.z);
        Vector3 home = new Vector3(homes[randH].transform.position.x + randX, homes[randH].transform.position.y, homes[randH].transform.position.z);
        //calc new rotation
        float AngleRad = Mathf.Atan2(home.y - newSpawn.y, home.x + randR - newSpawn.x);
        float AngleDeg = (180 / Mathf.PI) * AngleRad - 90;

        //get from pool and active
        GameObject meteor = objectPool.GetPooledObject();
        if (meteor != null)
        {
            meteor.transform.position = newSpawn;
            meteor.transform.rotation = Quaternion.Euler(0, 0, AngleDeg); //spawn.rotation;
            meteor.SetActive(true);
        }
    }

    //when home destroy remove it from list, meteor won't target this home again
    void OnHomeDestroy(GameObject home)
    {
        homes.Remove(home);
    }

    //set values for start/restart game
    void initStartValues()
    {
        foreach (Transform meteor in objectPool.gameObject.transform) //deactive meteores
            meteor.gameObject.SetActive(false);

        homes.Clear();

        foreach (Transform home in homePool.gameObject.transform) //active all homes and readd to list
        {
            home.gameObject.SetActive(true);
            this.homes.Add(home.gameObject);
        }

    }
}