using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet
{
    public float X { get; set; }
    public float Y { get; set; }
    public float Speed { get; set; }
    public float Direction { get; set; }
    public int Damage { get; set; }

    public void Start(GameObject go)
    {
        Object.Instantiate(go, go.transform.position, Quaternion.identity);
    }

    public bool CheckCollide(Collider2D col, GameObject go)
    {
        GameObject collider = col.gameObject;
        if (collider.tag == "EnemyShipTag")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
