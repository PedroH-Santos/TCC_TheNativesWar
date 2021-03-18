using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [HideInInspector] public Animator animator;
    private string[] staticDirections = { "Static_N", "Static_NW", "Static_W", "Static_SW", "Static_S", "Static_SE", "Static_E", "Static_NE" };
    private string[] runDirections = { "Run_N", "Run_NW", "Run_W", "Run_SW", "Run_S", "Run_SE", "Run_E", "Run_NE" };
    private string[] hurtDirections = { "Hurt_N", "Hurt_NW", "Hurt_W", "Hurt_SW", "Hurt_S", "Hurt_SE", "Hurt_E", "Hurt_NE" };
    private string[] dieDirections = { "Die_N", "Die_NW", "Die_W", "Die_SW", "Die_S", "Die_SE", "Die_E", "Die_NE" };
    private string[] attackDirections = { "Attack_N", "Attack_NW", "Attack_W", "Attack_SW", "Attack_S", "Attack_SE", "Attack_E", "Attack_NE" };
    [HideInInspector] public string stateOfPlayer;
    [HideInInspector] public bool alertFinishedAnimation;
    public float timeForSound;
    private float currentTime;
    private int lastDirection;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = Time.time;
        alertFinishedAnimation = false;
        stateOfPlayer = "Idle";
        lastDirection = 0;
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {

        if (stateOfPlayer != "Transformation")
        {
            setDirection();
        }

    }
    public Vector2 getDirection()
    {
        Vector2 direction = new Vector2(0, 0);
        switch (lastDirection)
        {
            case 0:
                direction = new Vector2(0, 1);
                break;
            case 1:
                direction = new Vector2(-1, 1);
                break;
            case 2:
                direction = new Vector2(-1, 0);
                break;
            case 3:
                direction = new Vector2(-1, -1);
                break;
            case 4:
                direction = new Vector2(0, -1);
                break;
            case 5:
                direction = new Vector2(1, -1);
                break;
            case 6:
                direction = new Vector2(1, 0);
                break;
            case 7:
                direction = new Vector2(1, 1);
                break;
        }
        return direction;
    } //Pega direção do player EXATAMENTE
    private string[] getAnimation(){
        switch (stateOfPlayer)
        {
            case "Idle":
                return staticDirections;
            case "Run":
                return runDirections;
            case "Hurt":
                return hurtDirections;
            case "Die":
                return dieDirections;
            case "Attack":
                return attackDirections;
           
                
        }
        return staticDirections;
    } //Pega a animação desejada
    private void playAudio()
    {
        if (gameObject.tag == "Player")
        {
            if ((stateOfPlayer == "Idle" || stateOfPlayer == "Run"))
            {
                if (Time.time - currentTime > timeForSound)
                {
                    GetComponent<Sound>().playAudio(stateOfPlayer);
                    currentTime = Time.time;
                }

            }
            else
            {
                GetComponent<Sound>().playAudio(stateOfPlayer);
            }
        }

    }
    public void setDirection()
    {
        string[] directionArray = null;
       
            if (GetComponent<Player>().direction.magnitude > 0.01) //caso o player aperte o botao de andar entra aqui
            {

                if (stateOfPlayer == "Idle") {  //O player so se movimenta caso estiver Idle
                        stateOfPlayer = "Run";
                }
                if (stateOfPlayer == "Run") //Troca direção
                {
                    lastDirection = DirectionToIndex(GetComponent<Player>().direction);
                }

             
            }
        directionArray = getAnimation();
        animator.Play(directionArray[lastDirection]); //Play animação
        playAudio();

    } //Determina qual animação deve ser iniciada

    private int DirectionToIndex(Vector2 direction)
    {
        Vector2 newDirection = direction.normalized;
        float allDirection = 360 / 8; //Divide a etapa em 8 direções
        float offset = allDirection / 2; //Tira o OFFSet

        float angle = Vector2.SignedAngle(Vector2.up, newDirection); //Calcula o angule entre o atual vetor e a nova direção

        angle += offset; 
        if (angle < 0) {
            angle += 360;
        }
        float directionCount = angle / allDirection; //Divide a etapa para ver qual direção o mesmo está
        return Mathf.FloorToInt(directionCount);
    } //Determina qual direção o player está
    public void finishedAnimation()
    {
        if (stateOfPlayer == "Attack")
        {
            alertFinishedAnimation = true;
            stateOfPlayer = "Idle";
        }else if (stateOfPlayer == "Die")
        {
            gameObject.GetComponent<Animator>().enabled = false;
            GetComponentInChildren<PlayerAttack>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
        }
        else if (stateOfPlayer == "Hurt")
        {
            stateOfPlayer = "Idle";
        }

    } //Da um alerta quando a animação termina

}
