using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // Start is called before the first frame update
   
        
    private GameObject target;
    public float timeForAttack;
    public int damageForAttack;
    
    private float timeCurrent;

    void Start()
    {
        timeCurrent = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetAxis("AttackNormalPaje")) == 1)
        {
            if (Time.time - timeCurrent > timeForAttack) //Testa se o tempo para atacke foi alcançado
            {
                timeCurrent = Time.time;
                GetComponentInParent<PlayerAnimation>().stateOfPlayer = "Attack";
                
            }
        }  
        if (GetComponentInParent<PlayerAnimation>().alertFinishedAnimation)
        { 
            GetComponentInParent<PlayerAnimation>().alertFinishedAnimation = false;

            if (target != null) 
            {
                Life lifeBar = target.GetComponentInChildren<Life>();
                if (lifeBar.life - damageForAttack <= 0)
                {
                    lifeBar.life = 0;
                    target = null;
                }
                else
                {
                    lifeBar.life -= damageForAttack;
                    target.GetComponent<Enemy>().stateOfEnemy = "Hurt";
                }
            }
        }
        




    }
    public void setNewAttack(int setAttack, bool changeTheAttack) //Chaged the Attack
    {
        if (changeTheAttack)
        {
        
            damageForAttack += setAttack;
        }
        else
        {
            damageForAttack -= setAttack;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<BoxCollider2D>() == collision) //testa se o collisor é um box collider
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                if (target == null) 
                {
                    target = collision.gameObject;
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<BoxCollider2D>() == collision) //testa se o collisor é um box collider
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                target = null;
            }
        }
    }




}
