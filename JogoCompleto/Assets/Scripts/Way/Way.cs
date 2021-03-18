using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Way : MonoBehaviour
{
   [HideInInspector] public bool startWave;
    [HideInInspector] public bool finishedWave;
    public GameObject[] waypoints;
    public Wave[] waves;
    public int timeBetweenWaves;
    
    private float lastTimeSpawn;
    private int enemySpawned;
    private int currentWave;
    private UI ui;

    [System.Serializable]
    public class Wave{
        public GameObject enemyPrefab;
        public float intervalSpawn;
        public int maxInimigos;
    }

    void Start()
    {
        finishedWave = false;
        startWave = false;
        enemySpawned = 0;
        lastTimeSpawn = Time.time;
        ui = GameObject.Find("UI").GetComponent<UI>();
       
    }

    // Update is called once per frame
    void Update()
    {
        if (startWave) {
            currentWave = ui.ChangeWave;
            if (currentWave < waves.Length)
            {
                float intervalTime = Time.time - lastTimeSpawn;
                float intervalSpawn = waves[currentWave].intervalSpawn;
                if (((enemySpawned == 0 && intervalTime > timeBetweenWaves) || intervalTime > intervalSpawn)
                    && enemySpawned < waves[currentWave].maxInimigos)
                {
                    lastTimeSpawn = Time.time;
                    GameObject newEnemy = (GameObject)Instantiate(waves[currentWave].enemyPrefab);
                    newEnemy.transform.parent = gameObject.transform.parent.transform.parent;
                    newEnemy.GetComponent<Enemy>().waypoints = waypoints;
                    newEnemy.GetComponent<Enemy>().stateOfEnemy = "Run";

                    enemySpawned++;
                }
                if (enemySpawned == waves[currentWave].maxInimigos && GameObject.FindGameObjectWithTag("Enemy") == null) 
                {
                    finishedWave = true; //Caso não haja mais inimigos na wave todas os caminhos pedem para trocar a wave
                    startWave = false;
                    enemySpawned = 0;
                    lastTimeSpawn = Time.time;

                }
            }
        }

    }
   public void startOfWave()
    {
        startWave = true;

    }
}
