using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaiDoMato : MonoBehaviour
{
    // Start is called before the first frame update
    public float timeOfTransformed;
    public float timeForAttack;
    public GameObject playerPrefab;
    private float currentTimeForAttack;
    private float currentTimeForTransformed;

    public float distanceForThrowRock;
    public float speedForRock;
    public GameObject prefabRock;
  
    public int damageRock;
    

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
            if (GetComponent<PlayerAnimation>().stateOfPlayer == "Idle" || GetComponent<PlayerAnimation>().stateOfPlayer == "Run") { 

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
                    GetComponent<PlayerAnimation>().stateOfPlayer = "Attack";
                    currentTimeForAttack = Time.time;

                }
               

            }
            if (GetComponentInParent<PlayerAnimation>().alertFinishedAnimation)
            {
                GetComponentInParent<PlayerAnimation>().alertFinishedAnimation = false;
                spawnRock();
            }
        }
    }
    private void spawnRock()
    {
        Vector3 startPosition = gameObject.transform.GetChild(3).gameObject.transform.position;
        startPosition.z = prefabRock.transform.position.z;
        GameObject rock = (GameObject)Instantiate(prefabRock);
        rock.transform.position = startPosition;
        Rock scriptRock = rock.GetComponent<Rock>();
        scriptRock.speedForRock = speedForRock;
        scriptRock.damage = damageRock;
        scriptRock.distance = distanceForThrowRock;
        scriptRock.directionPlayer = GetComponent<PlayerAnimation>().getDirection();
    }
}
