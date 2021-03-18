using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [HideInInspector]
    public GameObject target;
    [HideInInspector]
    public float speedBullet;
    [HideInInspector]
    public int damage;
    private Vector3 startPosition;
    private Vector3 targetPosition;

    private float distance;
    private float lastTime;
    

    // Start is called before the first frame update
    void Start()
    {
        lastTime = Time.time;
        startPosition = gameObject.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            targetPosition = target.transform.position;
        }
        else
        {
            Destroy(gameObject);
        }
        distance = Vector3.Distance(startPosition, targetPosition);
        float intervalTime = Time.time - lastTime;
        gameObject.transform.position = Vector3.Lerp(startPosition,targetPosition,intervalTime * speedBullet/distance);

        if (gameObject.transform.position.Equals(targetPosition))
        {

            if (target != null)
            {
                if(target.tag == "Player" || target.tag == "Gods"  )
                {
                    target.GetComponent<Player>().hurtPlayer(Mathf.Max(damage, 0));
                }
                else if (target.tag == "Warrior")
                {
                    target.GetComponent<IndioWarrior>().hurtIndio(Mathf.Max(damage, 0));
                }else if (target.tag == "Enemy")
                {
                    target.GetComponent<Enemy>().hurtEnemy(Mathf.Max(damage, 0),false);
                }
            }
            
            Destroy(gameObject);

        }
    }
}
