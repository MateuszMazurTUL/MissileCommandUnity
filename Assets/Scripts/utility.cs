using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility
{
    //return hypotenuse of pitagoras
    public static float Pitagoras(Vector3 a, Vector3 b)
    {
        float x = (a.x - b.x);
        float y = (a.y - b.y);
        return Mathf.Sqrt(x * x + y * y);
    }
   
/*    public static float RotationFTO(Vector3 a, Vector3 b) // rotation face to object
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
    }*/
}
