using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Starship
{

    public float Angle { get; set; }

    public void ChangeAngle(float value)
    {
        Angle = value % 360;
    }
}
