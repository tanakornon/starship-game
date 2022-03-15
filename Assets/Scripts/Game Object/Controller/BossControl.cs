using System.Collections;
using UnityEngine;

public class BossControl : EnemyControl
{

    private PlayGameManager Manager;
    private float delay;

    void Start()
    {
        var scoreObj = GameObject.FindGameObjectWithTag("ScoreTextTag");
        if (scoreObj != null)
        {
            scoreUI = scoreObj.GetComponent<ScoreUI>();
        }

        var managerObj = GameObject.FindGameObjectWithTag("Manager");
        if (managerObj != null)
        {
            Manager = managerObj.GetComponent<PlayGameManager>();
        }
    }

    void Update()
    {
        if (Time.timeScale == 0) return;

        // Update movement
        Vector3 moveDir = Quaternion.Euler(0, 0, enemyGO.Angle) * Vector3.right;
        rb.velocity = moveDir * enemyGO.Speed;

        // Boss action delay logic
        delay += Time.deltaTime;
        if (delay > 2f)
        { // Delay condition adjusted to time-based logic
            enemyGO.Angle = Random.Range(enemyGO.Angle, enemyGO.Angle + 90f);
            SetSpeed(5f); // Adjust speed for the boss
            delay = 0f;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("BoundTag"))
        {
            enemyGO.Angle -= 180f; // Reverse angle on boundary hit
        }
    }

    void OnBecameInvisible()
    {
        enemyGO.Angle -= 180f; // Reverse angle when going off screen
    }

    void OnDestroy()
    {
        Manager.IsVictory(true); // Notify manager upon boss destruction
    }
}
