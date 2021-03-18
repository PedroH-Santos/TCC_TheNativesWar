using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackShooter : MonoBehaviour
{


    public float intervalForDemage;
    public int damage;
    public GameObject prefabBullet;
    public float speedForBullet;

    [HideInInspector] public List<GameObject> targetForAttack;
    private float timeLastDamage;
    private GameObject bullet;
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
                shoot(); 
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
            foreach (GameObject getTarget in targetForAttack)
            {
                if (getTarget != null)
                {
                    GetComponentInParent<Enemy>().stateOfEnemy = "Idle";
                   return getTarget;
                }
            }
        }
        GetComponentInParent<Enemy>().isInBattle = false;
        return null;

    }
    private void shoot()
    {

        Vector3 startPosition = gameObject.transform.parent.GetChild(2).transform.position;
        Vector3 targetPosition = target.transform.position;
        startPosition.z = prefabBullet.transform.position.z;
        targetPosition.z = prefabBullet.transform.position.z;
        bullet = (GameObject)Instantiate(prefabBullet);
        if (GetComponentInParent<Enemy>().direction == "West")
        {
            bullet.transform.Rotate(0, 0, 180); //Rotaciona a bala para 180 
        }
        bullet.transform.position = startPosition;
        Projectile scriptBullet = bullet.GetComponent<Projectile>();
        scriptBullet.target = target;
        scriptBullet.speedBullet = speedForBullet;
        scriptBullet.damage = damage;
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
            if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Warrior" || collision.gameObject.tag == "Gods")
            {
                if(collision.gameObject == target) //Pega outro inimigo
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
