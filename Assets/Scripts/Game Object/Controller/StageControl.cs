using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageControl : MonoBehaviour {

    Stage stage;

    float enemyTimer = 0f;
    float itemTimer = 0f;
    int nStage = 1;

    public GameObject LBLStage;
    public GameObject player;
    public GameObject[] enemies;
    public GameObject[] items;
    public int Score { get; set; }

    public bool GameEnd { get; set; }

    Queue<char> AI = new Queue<char>();

    void Start() {
        InitializeComponent();
    }

    void InitializeComponent() {
        GameEnd = false;
        stage = new Stage();
        stage.SpawnPlayer(player, new Vector3(0, -8.5f, 0));
        InitAI();
    }

    void InitAI() {
        string ai = "";

        //ai = "s1213124_";
        ai = "s1234_";
        foreach (char c in ai) AI.Enqueue(c);

        //ai = "s13624152_7_";
        ai = "s567_";
        foreach (char c in ai) AI.Enqueue(c);

        //ai = "s816_7_861_8wb";
        ai = "s876_8w_b";
        foreach (char c in ai) AI.Enqueue(c);

    }

    void Update() {
        if (Time.timeScale == 0 || GameEnd) return;

        enemyTimer += Time.deltaTime;
        itemTimer += Time.deltaTime;

        if (enemyTimer >= 6f && AI.Count > 0)
        { // 3 seconds
            StartCoroutine(Pattern(AI.Dequeue()));
            enemyTimer = 0f;
        }

        if (itemTimer >= 10f)
        { // 10 seconds
            Vector3 loc = new Vector3(Random.Range(-10.0f, 10.0f), 10, 0);
            stage.SpawnItem(items[0], loc);
            itemTimer = 0f;
        }
    }

    IEnumerator Pattern(char c) {
        if (c == '1') {
            StartEnemy(0, 0, 4, 270);
            yield return new WaitForSeconds(0.5f);
            StartEnemy(1, -2.5f, 6, 270);
            StartEnemy(1, 2.5f, 6, 270);
            yield return new WaitForSeconds(0.5f);
            StartEnemy(0, -5, 4, 270);
            StartEnemy(0, 5, 4, 270);
        } else if (c == '2') {
            StartEnemy(1, -5, 6, 270);
            StartEnemy(1, 5, 6, 270);
            yield return new WaitForSeconds(0.5f);
            StartEnemy(0, -2.5f, 4, 270);
            StartEnemy(0, 2.5f, 4, 270);
            StartEnemy(0, -7.5f, 5, 270);
            StartEnemy(0, 7.5f, 5, 270);
        } else if (c == '3') {
            for (int i = 0; i < 3; i++) {
                StartEnemy(0, -1.5f, 6, 270);
                yield return new WaitForSeconds(0.25f);
                StartEnemy(0, 1.5f, 6, 270);
                yield return new WaitForSeconds(0.25f);
            }
        } else if (c == '4') {
            for (int i = 1; i <= 3; i++) {
                StartEnemy(1, -i * 2, 6, 270);
                yield return new WaitForSeconds(0.25f);
                StartEnemy(1, +i * 2, 6, 270);
                yield return new WaitForSeconds(0.25f);
            }
        } else if (c == '5') {
            StartEnemy(1, 0, 4, 270);
            yield return new WaitForSeconds(0.5f);
            StartEnemy(2, -2.5f, 6, 270);
            StartEnemy(2, 2.5f, 6, 270);
            yield return new WaitForSeconds(0.5f);
            StartEnemy(1, -5, 4, 270);
            StartEnemy(1, 5, 4, 270);
        } else if (c == '6') {
            StartEnemy(3, -5, 2, 270);
            StartEnemy(3, 5, 2, 270);
            yield return new WaitForSeconds(0f);
        } else if (c == '7') {
            for (int i = 1; i <= 5; i++) {
                StartEnemy(i % 2, -i * 2, 6, 270);
                yield return new WaitForSeconds(0.25f);
            }
            for (int i = 1; i <= 5; i++) {
                StartEnemy(i % 2, +i * 2, 6, 270);
                yield return new WaitForSeconds(0.25f);
            }
            for (int i = -1; i <= 1; i++) {
                StartEnemy(2, i * 5, 6, 270);
                yield return new WaitForSeconds(0.25f);
            }
        } else if (c == '8') {
            StartEnemy(4, -7.5f, 2, 270);
            StartEnemy(4, 7.5f, 2, 270);
            yield return new WaitForSeconds(0.5f);
            StartEnemy(4, -2.5f, 2, 270);
            StartEnemy(4, 2.5f, 2, 270);
            yield return new WaitForSeconds(0.5f);
        } else if (c == 'b') {
            StartEnemy(5, 0, 3, 270);
        } else if (c == 's') {
            StartCoroutine(UpdateLabel(LBLStage));
        } else if (c == 'w') {
            StartCoroutine(Warning(LBLStage));
        }
    }

    IEnumerator UpdateLabel(GameObject Label) {
        Label.GetComponent<Text>().text = "S ta g e   " + (nStage++).ToString();
        Label.SetActive(true);
        yield return new WaitForSeconds(3);
        Label.SetActive(false);
    }

    IEnumerator Warning(GameObject Label) {
        Label.GetComponent<Text>().text = "W  A  R  N  I  N  G";
        Label.GetComponent<Text>().color = Color.red;

        for (int i = 0; i < 3; i++) {
            Label.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            Label.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
        Label.SetActive(false);
    }

    void StartEnemy(int Num, float X, float Speed, int Angle) {
        Vector3 Loc = new Vector3(X, 11f, 0);
        stage.SpawnEnemy(enemies[Num], Loc, Speed, Angle);
    }

    float FindAngle(float startX, float startY, float targetX, float targetY) {
        return Mathf.Atan2(targetY - startY, targetX - startX) * Mathf.Rad2Deg;
    }

}