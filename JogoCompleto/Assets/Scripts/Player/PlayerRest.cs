using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRest : MonoBehaviour
{
    public int intervalForRest;
    private Life lifePlayer;
    private float timeOfDeadPlayer;
    private float currentTimeForRegen;
    public float timeOfRegenLifePlayer;
    public float lifeRegen;
    // Start is called before the first frame update
    void Start()
    {
        lifePlayer = GetComponentInChildren<Life>();
        currentTimeForRegen = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (lifePlayer.life <= 0 && GetComponent<PlayerAnimation>().stateOfPlayer != "Die")//Caso o player morra desabilita a função de atacar, seu animator e seu collisor
        {

            timeOfDeadPlayer = Time.time;
            GetComponent<PlayerAnimation>().stateOfPlayer = "Die";
        }
        if (GetComponent<PlayerAnimation>().stateOfPlayer == "Die")
        {
            if (Time.time - timeOfDeadPlayer > intervalForRest) //Retorna o player a vida em um certo periodo
            {
                lifePlayer.life = lifePlayer.maxLife;
                gameObject.GetComponentInChildren<PlayerAttack>().enabled = true;
                gameObject.GetComponent<BoxCollider2D>().enabled = true;
                gameObject.GetComponent<Animator>().enabled = true;
                GetComponent<PlayerAnimation>().stateOfPlayer = "Idle";
            }
        }
        if (GetComponent<PlayerAnimation>().stateOfPlayer != "Die") //Regenera a vida do player
        {
            if (Time.time - currentTimeForRegen > timeOfRegenLifePlayer)
            {
                if (GetComponentInChildren<Life>().life <= GetComponentInChildren<Life>().maxLife - lifeRegen)
                {
                    GetComponentInChildren<Life>().life += lifeRegen;
                }
                currentTimeForRegen = Time.time;


            }
        }
        
    }
    public void setNewLifeRegen(float setLifeRegen, bool changeTheLifeRegen) //Chaged the life regen
    {
        if (changeTheLifeRegen)
        {
            lifeRegen += setLifeRegen;
        }
        else
        {
            lifeRegen -= setLifeRegen;
        }
    }

}
