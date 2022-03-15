using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    protected float delay;
    protected float sreload;
    protected float sfireRate;

    protected float angle;
    protected int cntBullet;

    public GameObject bullet;
    public int maxBullet;
    public float reload;    // seconds
    public float fireRate;  // seconds
    public float speed;

    protected virtual void Start()
    {
        angle = transform.eulerAngles.z + 90f;
        sreload = reload;       // in seconds now
        sfireRate = fireRate;   // in seconds
        delay = 0f;
        cntBullet = 0;
    }

    protected virtual void Update()
    {
        if (Time.timeScale == 0) return;

        delay += Time.deltaTime;

        if (delay > sreload)
        {
            if (delay > sreload + sfireRate)
            {
                Shoot(angle, speed);
                cntBullet++;
                delay = sreload; // lock in until bullets exhausted
            }

            if (cntBullet == maxBullet)
            {
                cntBullet = 0;
                delay = 0f;
            }
        }
    }

    protected void Shoot(float Angle, float Speed)
    {
        GameObject temp = Instantiate(bullet, transform.position, Quaternion.identity);
        var bc = temp.GetComponent<BulletControl>();
        bc.SetAngle(Angle);
        bc.SetSpeed(Speed);
    }
}
