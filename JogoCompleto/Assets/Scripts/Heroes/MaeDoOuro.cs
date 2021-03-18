using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaeDoOuro : MonoBehaviour
{
    public float timeForAttack;
    public float timeOfTransformed;
    private float currentTimeForAttack;
    private float currentTimeForTransformed;
    public GameObject playerPrefab;

    public float timeForParalyzing;
    public int  multipledGoldEnemy;
    private float currentTimeForParalyzing;
    private GameObject[] enemyInScene;
    private List<GameObject> enemyParalyzing;
    // Start is called before the first frame update
    void Start()
    {
        enemyParalyzing = new List<GameObject>();
        currentTimeForAttack = Time.time;
        currentTimeForTransformed = Time.time;
        currentTimeForParalyzing = Time.time;
        enemyInScene = GameObject.FindGameObjectsWithTag("Enemy");
        multipledGold("*"); // dobra o dinheiro dos inimigos
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - currentTimeForTransformed > timeOfTransformed || GetComponentInChildren<Life>().life <= 0 || Input.GetAxis("FinishedPowerOfGods") == 1)
        {
            if (GetComponent<PlayerAnimation>().stateOfPlayer == "Idle" || GetComponent<PlayerAnimation>().stateOfPlayer == "Run") {
                returnEnemyBeforeParalyzing(); //remove paralisia
                multipledGold("/"); //remove multiplicador no gold
                GetComponent<PlayerAnimation>().stateOfPlayer = "Transformation";
                GetComponent<PlayerAnimation>().animator.Play("Transformation_Paje");
                GetComponent<BoxCollider2D>().enabled = false;
                gameObject.GetComponent<Player>().darknessLightPlayer();

            }
        }

        else
        {
            if (Input.GetAxis("AttackSpecialPaje") == 1)
            {
                if (Time.time - currentTimeForAttack > timeForAttack)
                {

                    currentTimeForAttack = Time.time;
                    GetComponent<PlayerAnimation>().stateOfPlayer = "Attack";

                }

            }
            if (GetComponentInParent<PlayerAnimation>().alertFinishedAnimation)
            {
                currentTimeForParalyzing = Time.time;
                GetComponentInParent<PlayerAnimation>().alertFinishedAnimation = false;
                enemyInScene = GameObject.FindGameObjectsWithTag("Enemy"); // pega todos os inimigos em cena
                paralyzingEnemy(); //paraliza todos os inimigos
            }

        }
        if (enemyParalyzing.Count > 0)
        {
            if (Time.time - currentTimeForParalyzing > timeForParalyzing)
            {
                currentTimeForAttack = Time.time;
                currentTimeForParalyzing = Time.time;
                returnEnemyBeforeParalyzing();
                enemyParalyzing.Clear();
            }
        }
    }
    void multipledGold(string operato)
    {
        foreach (GameObject target in enemyInScene)
        {
            if(target != null)
            {
                if (operato == "*")
                {
                    target.GetComponent<Enemy>().goldDead *= 2;
                }
                else if (operato == "/")
                {
                    target.GetComponent<Enemy>().goldDead /= 2;
                }

            }
        }

    }
    void returnEnemyBeforeParalyzing()
    {

        foreach (GameObject enemy in enemyParalyzing)
        {
            if (enemy != null)
            {
                enemy.GetComponent<Enemy>().stateOfEnemy = "Run";
                enemy.GetComponent<Enemy>().startPosition = enemy.transform.position;
                enemy.GetComponent<Enemy>().lastTimeSwithWaypoints = Time.time;

            }
        }
        
    }
    void paralyzingEnemy()
    {

        foreach (GameObject target in enemyInScene)
        {
            if (target != null)
            {

                        
                        
                target.GetComponent<Enemy>().startPosition = target.transform.position;
                target.GetComponent<Enemy>().stateOfEnemy = "Stun";      
                target.GetComponent<Enemy>().lastTimeSwithWaypoints = Time.time;
                enemyParalyzing.Add(target);
            }
        }
        
    }
}
