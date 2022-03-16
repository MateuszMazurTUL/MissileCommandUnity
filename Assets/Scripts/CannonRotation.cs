using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonRotation : MonoBehaviour
{
    GameObject pivot_Cannon; // pivot conon should be center of base
    Vector3 pC_pos; //position  of pivot_cannon

    void Start()
    {
        //TODO chack is gameObject child is Pivot_Cannon
        pivot_Cannon = this.transform.GetChild(0).gameObject;
        pC_pos = pivot_Cannon.transform.position;
    }

    void Update()
    {
        if (GlobalSettings.GetTimeSpeed() > 0) // prevent rotate when pause
            this.Rotate();
        
    }

    void Rotate()
    {
        //Mouse position
        Vector3 mouse_pos = InputController.mouse_position;

        // Get Angle in Radians
        float AngleRad = Mathf.Atan2(mouse_pos.y - pC_pos.y, mouse_pos.x - pC_pos.x);
        // Get Angle in Degrees
        float AngleDeg = (180 / Mathf.PI) * AngleRad - 90;
        //Max rotate on the left/right
        if (AngleDeg < -60) AngleDeg = -60;
        if (AngleDeg > 60) AngleDeg = 60;

        // Rotate Object
        pivot_Cannon.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);
    }
}
