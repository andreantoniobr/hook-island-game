using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DirectionUtility
{

    public static int Angle(Vector2 direction)
    {
        int angle = 0;

        if (direction == Vector2.up)
        {
            angle = 90;
        }
        if (direction == Vector2.down)
        {
            angle = 270;
        }
        else if(direction == Vector2.left)
        {
            angle = 180;
        }
        else if(direction == Vector2.right)
        {
            angle = 0;
        }
        return angle;
    }
}
