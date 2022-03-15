using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleGun : Gun
{
    public int Direction;
    public float AngleRatio;

    private float delay = 0f;
    private int cntBullet = 0;

    void Update()
    {
        if (Time.timeScale == 0) return;

        delay += Time.deltaTime;

        if (delay > sreload)
        {
            if (delay > sreload + sfireRate)
            {
                for (int i = 0; i < Direction; i++)
                {
                    Shoot(angle + (i * (360f / Direction)), speed);
                }

                angle = (angle + AngleRatio) % 360f;

                cntBullet++;
                delay = sreload; // maintain fire mode
            }

            if (cntBullet == maxBullet)
            {
                cntBullet = 0;
                delay = 0f;
            }
        }
    }
}

