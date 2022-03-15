using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFanGun : FanGun
{

    void Update()
    {
        if (Time.timeScale == 0) return;

        if (Input.GetKey(KeyCode.Space))
        {
            delay += Time.deltaTime;

            if (delay > sreload)
            {
                for (int i = -maxFan / 2; i <= maxFan / 2; i++)
                {
                    Shoot(angle - (i * angleRatio), speed);
                }
                delay = 0f;
            }
        }
        else
        {
            // Optional: reset delay if you want single-shot style instead of holding fire
            delay = sreload; // keeps it ready for next press
        }
    }
}
