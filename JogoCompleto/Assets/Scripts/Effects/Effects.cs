using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effects : MonoBehaviour
{
    private List<enemyWhenHisArrived> targetForAttack;
    // Start is called before the first frame update
    void Start()
    {
        targetForAttack = new List<enemyWhenHisArrived>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void fire(int damageOfFire,int tickOfFire)
    {

        if (targetForAttack.Count > 0)
        {
            foreach (enemyWhenHisArrived target in targetForAttack)
            {
                if (target.gameObjectEnemy != null) 
                {
                    Life life = target.gameObjectEnemy.GetComponentInChildren<Life>();
                    if (target.lifeEnemyWhenHisArrivedTile != life.life + damageOfFire * tickOfFire) // testa se foi dado a quantidade certa de ticks de queima no inimigo
                    {
                        if (life.life - damageOfFire > 0)
                        {
                            life.life -= damageOfFire;
                        }
                        else
                        {
                            life.life = 0;
                        }
                    }

                }
            }
        }
    }
    private class enemyWhenHisArrived { 
        public GameObject gameObjectEnemy;
        public float lifeEnemyWhenHisArrivedTile; //pega a vida do inimigo quando ele chega no tile
    }
    public void clearListGameObject()
    {
        targetForAttack.Clear();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision = collision.gameObject.GetComponent<BoxCollider2D>())
        {
            if (collision.gameObject.tag == "Enemy")
            {
                enemyWhenHisArrived enemy = new enemyWhenHisArrived();
                enemy.gameObjectEnemy = collision.gameObject;
                enemy.lifeEnemyWhenHisArrivedTile = collision.gameObject.GetComponentInChildren<Life>().life;
                targetForAttack.Add(enemy);
            }
        }   
    }

}
