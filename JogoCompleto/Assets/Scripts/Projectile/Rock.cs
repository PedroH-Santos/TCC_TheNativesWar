using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{

    [HideInInspector] public float speedForRock;
    [HideInInspector] public int damage;
    private Vector3 startPosition;
    private List<GameObject> targetForAttack;
    private float lastTime;
    [HideInInspector] public float distance;
    [HideInInspector] public Vector2 directionPlayer;
    private Vector3 targetPosition;
    private float timeForDead = 1.0f;
    private float currentTime;
    
    // Start is called before the first frame update
    void Start()
    {
        lastTime = Time.time;
        targetForAttack = new List<GameObject>();

        startPosition = gameObject.transform.position;
        targetPosition = new Vector3(startPosition.x + (distance * directionPlayer.x), startPosition.y + (distance * directionPlayer.y), 0); // calcula a posição de chegada do projetil
        currentTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float intervalTime = Time.time - lastTime;
        gameObject.transform.position = Vector3.Lerp(startPosition, targetPosition, intervalTime * speedForRock / distance);
        if (gameObject.transform.position.Equals(targetPosition))
        {
            if (targetForAttack.Count > 0)
            {
                foreach (GameObject target in targetForAttack)
                {
                    if (target != null)
                    {
                        target.GetComponent<Enemy>().hurtEnemy(damage,true);
                    }
                }
            }
                gameObject.GetComponent<CircleCollider2D>().enabled = false;
                targetForAttack.Clear();


        }
        else
        {
            currentTime = Time.time;


        }
        if (Time.time - currentTime > timeForDead)
        {
            Destroy(gameObject);
        }

    }
  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision == collision.gameObject.GetComponent<BoxCollider2D>())
        {
            if(collision.gameObject.tag == "Enemy")
            {
                targetForAttack.Add(collision.gameObject);
            }
        }
    }
    
}
