using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndioWarrior : MonoBehaviour
{
    [HideInInspector] public GameObject towerOfWarrior;
    [HideInInspector] public Vector3 startPosition;
    [HideInInspector] public Vector3 targetPosition;
    [HideInInspector] public Vector3 lastPosition;

    public float timeForSound;
    public float speed;

    private Life lifeWarrior;
    private float lastTime;

    // Start is called before the first frame update
    void Start()
    {
        lastTime = Time.time;
        gameObject.transform.position = startPosition;
        lifeWarrior = GetComponentInChildren<Life>();
        GetComponent<IndioAnimation>().stateWarrior = "Run";
    }

    // Update is called once per frame
    void Update()
    {



        if (lifeWarrior.life <= 0)
        {
            GetComponent<IndioAnimation>().stateWarrior = "Die";
            if (GetComponent<IndioAnimation>().alertFinishedAnimation)
            {
                if (towerOfWarrior != null)
                {
                    if (towerOfWarrior.GetComponent<TowerWarrior>().positionOfwarriorsDead.Contains(lastPosition) == false) // testa se a posição que o inimigo morreu ja possui um guerreio     
                    {
                        towerOfWarrior.GetComponent<TowerWarrior>().positionOfwarriorsDead.Add(lastPosition); //Adiciona a posição na lista de guerreiros mortos no script da torre   
                        towerOfWarrior.GetComponent<TowerWarrior>().warriors.Remove(gameObject);
                    }
                    Destroy(gameObject);

                }
            }
        }

    
        if (GetComponent<IndioAnimation>().stateWarrior == "Run")
        {
            
            if (gameObject.transform.position != targetPosition)
            {

                moveWarrior();
            }else{

                testIfWarriorCanBackLastPosition();

            }

        }
        if (GetComponent<IndioAnimation>().stateWarrior == "Idle")
        {

            lastTime = Time.time;
            startPosition = gameObject.transform.position;
            testIfWarriorCanBackLastPosition();
        }
        



    }
    
    void testIfWarriorCanBackLastPosition()//Testa se o warrior pode voltar a ultima posição
    {
      
        if (gameObject.transform.position != lastPosition && GetComponentInChildren<IndioWarriorAttack>().targetForAttack.Count <= 0) 
        {
            
            lastTime = Time.time;
            startPosition = gameObject.transform.position;
            targetPosition = lastPosition;
            GetComponent<IndioAnimation>().stateWarrior = "Run";
        }
        else
        {
            GetComponent<IndioAnimation>().stateWarrior = "Idle";
        }
    }

    public void hurtIndio(float damage)
    {
        if (GetComponentInChildren<Life>().life - damage <= 0)
        {
            GetComponentInChildren<Life>().life = 0;
        }
        else
        {
            GetComponentInChildren<Life>().life -= damage;
            GetComponent<IndioAnimation>().stateWarrior = "Hurt";

        }
    }
    private void moveWarrior()
    {
        if (gameObject.transform.position.x > targetPosition.x)
        {
            GetComponent<IndioAnimation>().direction = "West";
        }
        else
        {
            GetComponent<IndioAnimation>().direction = "East";
        }
        float distance = Vector3.Distance(startPosition, targetPosition);
        float intervalTime = Time.time - lastTime;
        gameObject.transform.position = Vector3.Lerp(startPosition, targetPosition, intervalTime * speed / distance);
        if (gameObject.transform.position.Equals(targetPosition))
        {
            GetComponent<IndioAnimation>().stateWarrior = "Idle";
            lastTime = Time.time;
        }
    }
}
