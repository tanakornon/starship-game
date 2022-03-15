using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingGun : Gun
{
    void Update()
    {
        if (Time.timeScale == 0) return;

        GameObject player = GameObject.FindGameObjectWithTag("PlayerShipTag");
        if (player == null) return;

        angle = FindAngle(transform.position, player.transform.position);

        delay += Time.deltaTime;

        if (delay > sreload)
        {
            if (delay > sreload + sfireRate)
            {
                Shoot(angle, speed);
                cntBullet++;
                delay = sreload;
            }

            if (cntBullet == maxBullet)
            {
                cntBullet = 0;
                delay = 0f;
            }
        }
    }

    float FindAngle(Vector3 start, Vector3 target)
    {
        return Mathf.Atan2(target.y - start.y, target.x - start.x) * Mathf.Rad2Deg;
    }
}

