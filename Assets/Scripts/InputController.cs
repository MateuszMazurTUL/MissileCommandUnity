using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public delegate void thisDelegate(Vector3 v);
    public delegate void thisDelegateESC();
    public event thisDelegate notify;
    public event thisDelegateESC notifyEsc;

    public static InputController SharedInstance; //singleton
    public static Vector3 mouse_position;


    void Awake()
    {
        SharedInstance = this;
    }

    void Update()
    {
        mouse_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //shoot
        if (Input.GetMouseButtonDown(0))
            notify(mouse_position);
        
        //pause/play
        if (Input.GetKeyDown(KeyCode.Escape))
            notifyEsc();
    }
}
