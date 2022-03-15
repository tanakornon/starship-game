using System.Collections;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    private Rigidbody2D rb;
    private float angle;
    private float speed;

    public GameObject ExplosionAnim;
    public int Damage;
    public bool Freeze;

    public void SetAngle(float value)
    {
        angle = value % 360;
        if (!Freeze)
        {
            transform.eulerAngles = new Vector3(0, 0, angle - 90);
        }
    }

    public float GetSpeed() { return speed; }
    public void SetSpeed(float value) { speed = value; }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector3 direction = Quaternion.Euler(0, 0, angle) * Vector3.right;
        rb.velocity = direction * speed;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("EnemyShipTag") && CompareTag("PlayerBulletTag"))
        {
            col.GetComponent<EnemyControl>().Damage(Damage);
            DestroyOnCollide();
        }
        else if (col.CompareTag("PlayerShipTag") && CompareTag("EnemyBulletTag"))
        {
            col.GetComponent<PlayerControl>().Damage(Damage);
            DestroyOnCollide();
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void DestroyOnCollide()
    {
        PlayExplosion();
        Destroy(gameObject);
    }

    private void PlayExplosion()
    {
        if (ExplosionAnim != null)
        {
            GameObject explosion = Instantiate(ExplosionAnim, transform.position, Quaternion.identity);
        }
    }
}
