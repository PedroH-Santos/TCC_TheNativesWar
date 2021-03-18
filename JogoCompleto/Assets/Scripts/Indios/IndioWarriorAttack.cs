using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndioWarriorAttack : MonoBehaviour
{
    [HideInInspector] public List<GameObject> targetForAttack;
    [HideInInspector] public GameObject target;
    public int damage;
    public float intervalForDamage;

   
    private float timelastDamage;

    // Start is called before the first frame update
    void Start()
    {
        timelastDamage = Time.time;
        targetForAttack = new List<GameObject>();
        target = null;
    }

    // Update is called once per frame
    void Update()
    {

        if (target != null)
        {
                if (gameObject.transform.parent.position.x > target.transform.position.x)
                {
                    GetComponentInParent<IndioAnimation>().direction = "West";
                }
                else
                {
                    GetComponentInParent<IndioAnimation>().direction = "East";
                }

            if (GetComponentInParent<IndioAnimation>().stateWarrior == "Idle")
                {
                    if (Time.time - timelastDamage > intervalForDamage)
                    {
                        GetComponentInParent<IndioAnimation>().stateWarrior = "Attack";
                        timelastDamage = Time.time;
                    }
                    if (GetComponentInParent<IndioAnimation>().alertFinishedAnimation)
                    {
                    GetComponentInParent<IndioAnimation>().alertFinishedAnimation = false;
                    target.GetComponent<Enemy>().hurtEnemy(damage,true);
                        GetComponentInParent<IndioAnimation>().stateWarrior = "Idle";
                    }
                }
            }
            else
            {
                if (GetComponentInParent<IndioAnimation>().stateWarrior == "Idle")
                {
                    target = getNewTarget();
                }
    
            }
        
                        
    }
   private GameObject getNewTarget()
    {
        if (targetForAttack.Count > 0)
        {
            foreach (GameObject enemy in targetForAttack)
            {
                if (enemy != null)
                {
                    if (GetComponentInParent<IndioWarrior>().towerOfWarrior.GetComponent<TowerWarrior>().checkTargetInWarriors(gameObject, enemy))
                    {

                        GetComponentInParent<IndioAnimation>().stateWarrior = "Run";
                        GetComponentInParent<IndioWarrior>().targetPosition = new Vector3(enemy.transform.position.x - 0.5f, enemy.transform.position.y, 0);
                        enemy.GetComponent<Enemy>().stateOfEnemy = "ComingEnemy"; //Para o Inimigo
                        enemy.GetComponent<Enemy>().isInBattle = true;
                        return enemy;
                    }
                }
            }
        }
        return null;
    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == collision.gameObject.GetComponent<BoxCollider2D>()) {
            if(collision.gameObject.tag == "Enemy") {
                   
                targetForAttack.Add(collision.gameObject);   
            }
        }
  
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == collision.gameObject.GetComponent<BoxCollider2D>())
        {
            if (collision.gameObject.tag == "Enemy")
            {
                if (target == collision.gameObject)
                {
                    if (GetComponentInParent<IndioAnimation>().stateWarrior == "Die") //Caso o warrior morra ele livra o target 
                    {
                        target.GetComponent<Enemy>().stateOfEnemy = "Run";

                    }
                    target = null;
                    
                }
                targetForAttack.Remove(collision.gameObject);
                if (targetForAttack.Count <= 0) //Caso nao tiver mais inimigos na area do warrior ele volta para a ultima posicao
                {
                    GetComponentInParent<IndioAnimation>().stateWarrior = "Run";
                }
            }
        }
    }

}
