using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Boitata : MonoBehaviour
{
    private float currentTimeForAttack;
    private float currentTimeForTransformed;
    public float timeOfTransformed;
    public GameObject playerPrefab;
    public float timeForAttack;

    public float distanceForBallOfFire;
    public float speedOfBall;
    public int damageOfFire;
    public float timeForEndFire;
    public int ticksOfFire;
    public GameObject prefabBallOfFire;



    
    // Start is called before the first frame update
    void Start()
    {
  
        currentTimeForAttack = Time.time;
        currentTimeForTransformed = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - currentTimeForTransformed > timeOfTransformed || GetComponentInChildren<Life>().life <= 0 || Input.GetAxis("FinishedPowerOfGods") == 1)
        {
            if (GetComponent<PlayerAnimation>().stateOfPlayer == "Idle" || GetComponent<PlayerAnimation>().stateOfPlayer == "Run")
            {
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
                GetComponentInParent<PlayerAnimation>().alertFinishedAnimation = false;
                spawnBallOfFire();
            }
        }

    }
    void spawnBallOfFire()
    {
        Vector3 startPosition = gameObject.transform.GetChild(3).gameObject.transform.position;
        startPosition.z = prefabBallOfFire.transform.position.z;
        GameObject ballOfFire = (GameObject)Instantiate(prefabBallOfFire);
        ballOfFire.transform.position = startPosition;
        BallOfFire scriptBallOfFire = ballOfFire.GetComponent<BallOfFire>();

        scriptBallOfFire.speedForBallOfFire = speedOfBall;
        scriptBallOfFire.damageOfFire = damageOfFire;
        scriptBallOfFire.distance = distanceForBallOfFire;
        scriptBallOfFire.timeForEndFire = timeForEndFire;
        scriptBallOfFire.ticksOfFire = ticksOfFire;
        scriptBallOfFire.directionPlayer = GetComponent<PlayerAnimation>().getDirection();
    }
}
