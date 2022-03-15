using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanGun : Gun
{
    public int maxFan;
    public int angleRatio;

    private float delay = 0f;
    private int cntBullet = 0;

    private void Update()
    {
        if (Time.timeScale == 0) return;

        delay += Time.deltaTime;

        if (delay > sreload)
        {
            if (delay > sreload + sfireRate)
            {
                for (int i = -maxFan / 2; i <= maxFan / 2; i++)
                {
                    Shoot(angle - (i * angleRatio), speed);
                }

                cntBullet++;
                delay = sreload; // stay in fire mode until maxBullet
            }

            if (cntBullet == maxBullet)
            {
                cntBullet = 0;
                delay = 0f; // reset cycle
            }
        }
    }
}
