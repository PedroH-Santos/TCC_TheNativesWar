using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackMelee : MonoBehaviour
{
    
    private float timeLastDamage;
    public float intervalForDemage;
    public int damage;
    [HideInInspector] public List<GameObject> targetForAttack;
    private GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        timeLastDamage = Time.time;
        targetForAttack = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {

            if (gameObject.transform.parent.position.x > target.transform.position.x)
            {
                GetComponentInParent<Enemy>().direction = "West";
            }
            else
            {
                GetComponentInParent<Enemy>().direction = "East";
            }
            if (Time.time - timeLastDamage > intervalForDemage)
            {
                GetComponentInParent<Enemy>().stateOfEnemy = "Attack";
                timeLastDamage = Time.time;
            }

            if (GetComponentInParent<EnemyAnimation>().finishedAnimation)
            {
                GetComponentInParent<EnemyAnimation>().finishedAnimation = false;    
                if (target.tag == "Player" || target.tag == "Gods")
                {       
                    target.GetComponent<Player>().hurtPlayer(damage);  
                }else if (target.tag == "Warrior") 
                {
                    target.GetComponent<IndioWarrior>().hurtIndio(damage);
                }
                
            }
        }
        else
        {

                target = getNewTarget();
            

        }
        

        
    }
    private GameObject getNewTarget()
    {
        if (targetForAttack.Count > 0)
        {

            GetComponentInParent<Enemy>().isInBattle = true;
            foreach (GameObject enemy in targetForAttack)
            {
                if (enemy != null)
                {
                    bool targetCanAttack = false;

                    if (enemy.tag == "Warrior")
                    {

                        if (enemy.GetComponentInChildren<IndioWarriorAttack>().target != null)
                        {
                            if (enemy.GetComponentInChildren<IndioWarriorAttack>().target == gameObject.transform.parent.gameObject)
                            {


                                targetCanAttack = true;
                            }
                        }

                    }
                    else { 
                       targetCanAttack = true;
                    }
                    if (targetCanAttack)
                    {

                        GetComponentInParent<Enemy>().stateOfEnemy = "Idle";
                        return enemy;

                    }

                }

            }
        }
        GetComponentInParent<Enemy>().isInBattle = false;
        return null;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == collision.gameObject.GetComponent<BoxCollider2D>())
        {
            if (collision.gameObject.tag == "Warrior" || collision.gameObject.tag == "Player" || collision.gameObject.tag == "Gods")
            {
                targetForAttack.Add(collision.gameObject);

            }






        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == collision.gameObject.GetComponent<BoxCollider2D>())
        {
            if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Warrior" || collision.gameObject.tag =="Gods")
            {
                if (target == collision.gameObject)
                {
                    target = null;
                }
                targetForAttack.Remove(collision.gameObject);
                if (targetForAttack.Count <= 0)
                {
                    GetComponentInParent<Enemy>().stateOfEnemy = "Run";
                }


            }
        }
    }
}
