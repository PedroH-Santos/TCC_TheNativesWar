using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy: MonoBehaviour
{
     public GameObject[] waypoints;
    [HideInInspector] public string direction;
    [HideInInspector] public int currentWaypoint;
    //Controla as animações dos inimigos
    [HideInInspector] public string stateOfEnemy;
    [HideInInspector] public float lastTimeSwithWaypoints;
    [HideInInspector] public Vector3 startPosition;
    [HideInInspector] public bool isInBattle;
    // Todo codigo que modifica a speed do enemy modifica o setSpeed
    public int goldDead;
    public float speed;
    private Life lifeEnemy;
    private UI ui;
    private Vector3 nextPosition;



    // Start is called before the first frame update
    void Start()
    {
        lastTimeSwithWaypoints = Time.time;
        currentWaypoint = 0;
        ui = GameObject.Find("UI").GetComponent<UI>();
        startPosition = waypoints[currentWaypoint].transform.position;
        nextPosition = waypoints[currentWaypoint + 1].transform.position;
        lifeEnemy = GetComponentInChildren<Life>();
        stateOfEnemy = "Run";
    }

    // Update is called once per frame
    void Update()
    {
        if (lifeEnemy.life <= 0)
        {
            stateOfEnemy = "Die";
            if (GetComponent<EnemyAnimation>().finishedAnimation)
            {
                ui.ChangeGold += goldDead;
                Destroy(gameObject);
            }
        }

        if (stateOfEnemy == "Run")
        {
            if (nextPosition.x > startPosition.x)
            {
                direction = "East";
            }
            else
            {
                direction = "West";
            }

            move();

        }
        else
        {

            startPosition = gameObject.transform.position;
            lastTimeSwithWaypoints = Time.time;

        }
        if (!isInBattle) {
            if (stateOfEnemy == "Idle") //Caso o inimigo esteja parado sem inimigos ele começa a andar
            {
                if (speed != 0) {
                    stateOfEnemy = "Run";
                }
            }
        }

    }

    void move()
    { 
       
        float distance = Vector3.Distance(startPosition, nextPosition);

        float totalTimeForPath = distance / speed;
        
        float currentTimeOnPath = Time.time - lastTimeSwithWaypoints;
        gameObject.transform.position = Vector2.Lerp(startPosition, nextPosition,  currentTimeOnPath / totalTimeForPath);
        if (Vector3.Distance(gameObject.transform.position,startPosition) == distance)
        { 
            if (currentWaypoint < waypoints.Length - 2)
            {
               
                currentWaypoint++;
                startPosition = waypoints[currentWaypoint].transform.position;
                nextPosition = waypoints[currentWaypoint + 1].transform.position;
                lastTimeSwithWaypoints = Time.time;

            }
            else
            {
                Destroy(gameObject);
                ui.ChangeLife--;

            }
        }

    }
    public void hurtEnemy(float damage,bool animation) //Função que tira dano do gameobject
    {
        if(GetComponentInChildren<Life>().life - damage <= 0)
        {
            GetComponentInChildren<Life>().life = 0;
        }
        else
        {
            GetComponentInChildren<Life>().life -= damage;
            if (animation) {
                stateOfEnemy = "Hurt";
            }
        }


    }


}
