using System.Collections;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    private bool addedScore;
    private bool sleep;

    protected Enemy enemyGO;
    protected Rigidbody2D rb;
    protected ScoreUI scoreUI;

    public GameObject ExplosionAnim;
    public float speed;
    public int health;
    public int score;
    public float decay;

    void Awake()
    {
        addedScore = false;
        sleep = false;

        rb = GetComponent<Rigidbody2D>();

        enemyGO = new Enemy
        {
            Speed = speed,
            Reload = 0.1f,
            Health = health,
            Angle = transform.eulerAngles.z + 90f
        };
    }

    void Start()
    {
        var scoreObj = GameObject.FindGameObjectWithTag("ScoreTextTag");
        if (scoreObj != null)
        {
            scoreUI = scoreObj.GetComponent<ScoreUI>();
        }
    }

    void Update()
    {
        if (Time.timeScale == 0) return;

        if (!sleep)
        {
            enemyGO.Speed -= decay * Time.deltaTime;
            if (enemyGO.Speed < 0f)
            {
                enemyGO.Speed = 0f;
                sleep = true;
            }
        }
        else
        {
            enemyGO.Speed += decay * Time.deltaTime;
        }

        Vector3 moveDir = Quaternion.Euler(0, 0, enemyGO.Angle) * Vector3.right;
        rb.velocity = moveDir * enemyGO.Speed;
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void PlayExplosion()
    {
        if (ExplosionAnim != null)
        {
            GameObject explosion = Instantiate(ExplosionAnim, transform.position, Quaternion.identity);
        }
    }

    public void SetFacing(float angleDelta)
    {
        enemyGO.ChangeAngle(angleDelta);
        transform.eulerAngles = new Vector3(0, 0, enemyGO.Angle - 90f);
    }

    public void SetSpeed(float newSpeed)
    {
        enemyGO.Speed = newSpeed;
    }

    public void Damage(int damage)
    {
        enemyGO.DoDamage(damage);

        if (enemyGO.Health <= 0)
        {
            if (!addedScore)
            {
                addedScore = true;
                scoreUI.AddScore(score);
            }

            PlayExplosion();
            Destroy(gameObject);
        }
    }
}
