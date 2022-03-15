using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starship
{
    public float X { get; set; }
    public float Y { get; set; }
    public int Health { get; set; }
    public float Speed { get; set; }
    public float Reload { get; set; }
    public GameObject Bullet { get; set; }

    public void Start(GameObject go)
    {
        Object.Instantiate(go, go.transform.position, Quaternion.identity);
    }

    public void Shoot(GameObject shooter, int Angle, int Speed)
    {
        Bullet.GetComponent<BulletControl>().SetAngle(Angle);
        Bullet.GetComponent<BulletControl>().SetSpeed(Speed);
        Object.Instantiate(Bullet, shooter.transform.position, Quaternion.identity);
    }

    public void DoDamage(int value)
    {
        if (Health - value > 0)
        {
            Health -= value;
        }
        else
        {
            Health = 0;
        }
    }
}
