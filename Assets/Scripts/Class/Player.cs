using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Starship
{
    public int Life { get; set; }

    public void Move(Rigidbody2D rb)
    {
        rb.velocity = new Vector2(X * Speed, Y * Speed);
        //GO.transform.Translate(X * Speed, Y * Speed, 0);
    }

    public void UpdateLife(int value)
    {
        if (value > 9)
        {
            value = 9;
        }
        else if (value < 0)
        {
            value = 0;
        }

        Life = value;
    }
}
