using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage
{
    public int Score { get; set; }

    public int GetScore()
    {
        return Score;
    }

    public void UpdateScore(int value)
    {
        Score = value;
    }

    public void SpawnPlayer(GameObject GO, Vector3 Loc)
    {
        Object.Instantiate(GO, Loc, Quaternion.identity);
    }

    public void SpawnEnemy(GameObject GO, Vector3 Loc, float Speed, int Angle)
    {
        EnemyControl Temp = Object.Instantiate(GO, Loc, Quaternion.identity).GetComponent<EnemyControl>();
        Temp.SetSpeed(Speed);
        Temp.SetFacing(Angle);
    }

    public void SpawnItem(GameObject GO, Vector3 Loc)
    {
        Object.Instantiate(GO, Loc, Quaternion.identity);
    }
}
