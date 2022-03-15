using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public float X { get; set; }
    public float Y { get; set; }
    public float Speed { get; set; }
    public float Direction { get; set; }
    public int Item_Type { get; set; }

    public void Start(GameObject go)
    {
        Object.Instantiate(go, go.transform.position, Quaternion.identity);
    }

    public bool CheckCollide(Collider2D col)
    {
        GameObject collider = col.gameObject;
        return collider.tag == "PlayerShipTag";
    }
}
