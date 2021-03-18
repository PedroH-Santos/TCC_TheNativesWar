using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rigidBody;
    private float moveHorizontal, moveVertical;
    [HideInInspector] public Vector2 direction;
    public float moveSpeed;


    private GameObject lighPlayer;
    void Start()
    {
        GameObject.FindGameObjectWithTag("LightPlayer").GetComponent<Light2D>().intensity = 0.0f;
        lighPlayer = GameObject.FindGameObjectWithTag("PointLight");

    }
   void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    private void FixedUpdate()
    {

        moveHorizontal = Input.GetAxis("Horizontal")* moveSpeed;
        moveVertical = Input.GetAxis("Vertical") * moveSpeed;
        direction = new Vector2(moveHorizontal, moveVertical);

        if (GetComponent<PlayerAnimation>().stateOfPlayer == "Run") 
        {
            rigidBody.velocity = new Vector2(moveHorizontal, moveVertical); //caso o estado do player for run ele corre

            if (direction.magnitude < 0.01) //Verifica se o player esta parado
            {
                GetComponent<PlayerAnimation>().stateOfPlayer = "Idle"; 
            }
        }
        else
        {
            rigidBody.velocity = new Vector2(0,0);
        }
        
      
    }
    public void setNewSpeed(float setSpeed,bool changeTheSpeed) //Chaged the speed
    {
        if (changeTheSpeed)
        {
            moveSpeed += setSpeed;
        }
        else
        {
            moveSpeed -= setSpeed;
        }
    }
    public void hurtPlayer(float damage)
    {
        if (GetComponentInChildren<Life>().life - damage <= 0)
        {
            GetComponentInChildren<Life>().life = 0;

        }
        else
        {
            GetComponentInChildren<Life>().life -= damage;
            GetComponent<PlayerAnimation>().stateOfPlayer = "Hurt";

        }
    }
    public void darknessLightPlayer()
    {
        lighPlayer.GetComponent<Light2D>().color = Color.Lerp(lighPlayer.GetComponent<Light2D>().color, Color.grey, 10);
    }
    public void whatHero(GameObject hero)
    {

            GameObject newHero = (GameObject)Instantiate(hero);
            newHero.transform.position = gameObject.transform.position;
            lighPlayer.GetComponent<Light2D>().color = Color.Lerp(lighPlayer.GetComponent<Light2D>().color, Color.white, 10);
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FollowPaje>().cameraInTransfomationPaje(false);
            Destroy(gameObject);
        


    }



}
