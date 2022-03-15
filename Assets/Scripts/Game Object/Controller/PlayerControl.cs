using System.Collections;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    const int InvulnerableTime = 120;

    private Player playerGO;
    private Rigidbody2D rb;
    private LifeUI lifeUI;
    private PlayGameManager manager;
    private float invulnerableTimeRemaining;

    public GameObject ExplosionAnim;
    public GameObject Bullet;
    public float speed;
    public int health;
    public int life;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        SetInvulnerable();

        playerGO = new Player
        {
            Speed = speed,
            Reload = 0.1f,
            Health = health,
            Life = life,
            Bullet = Bullet
        };
    }

    void Start()
    {
        lifeUI = GameObject.FindGameObjectWithTag("LifeTextTag").GetComponent<LifeUI>();
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<PlayGameManager>();
    }

    void Update()
    {
        if (Time.timeScale == 0) return;

        // Handle invulnerability
        if (invulnerableTimeRemaining > 0) invulnerableTimeRemaining -= Time.deltaTime;

        // Handle movement
        playerGO.X = Input.GetAxis("Horizontal");
        playerGO.Y = Input.GetAxis("Vertical");
        playerGO.Move(rb);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("EnemyShipTag") && invulnerableTimeRemaining <= 0)
        {
            col.GetComponent<EnemyControl>().Damage(250);
            Damage(250);
        }
    }

    public void AddLife()
    {
        playerGO.UpdateLife(playerGO.Life + 1);
        lifeUI.DisplayLife(playerGO.Life);
    }

    public void Damage(int damageAmount)
    {
        if (invulnerableTimeRemaining <= 0)
        {
            playerGO.DoDamage(damageAmount);
        }

        if (playerGO.Health <= 0)
        {
            playerGO.Health = health;
            playerGO.UpdateLife(playerGO.Life - 1);
            lifeUI.DisplayLife(playerGO.Life);

            SetInvulnerable();
            PlayExplosion();
        }

        if (playerGO.Life <= 0)
        {
            manager.IsVictory(false);
            Destroy(gameObject);
        }
    }

    void PlayExplosion()
    {
        GameObject explosion = Instantiate(ExplosionAnim);
        explosion.transform.position = transform.position;
    }

    void SetInvulnerable()
    {
        invulnerableTimeRemaining = InvulnerableTime / 60f; // Set the invulnerable time to a fraction of a second

        Renderer renderer = GetComponent<Renderer>();
        float blinkRatio = 0.05f;
        int totalBlinks = (int)(invulnerableTimeRemaining / blinkRatio / 2);

        StartCoroutine(DoBlinks(renderer, totalBlinks, blinkRatio));
    }

    IEnumerator DoBlinks(Renderer renderer, int numBlinks, float blinkInterval)
    {
        for (int i = 0; i < numBlinks * 2; i++)
        {
            renderer.enabled = !renderer.enabled;
            yield return new WaitForSeconds(blinkInterval);
        }
        renderer.enabled = true;
    }
}
