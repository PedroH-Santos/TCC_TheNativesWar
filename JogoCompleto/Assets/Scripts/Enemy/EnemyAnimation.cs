using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Animator animator;
    [HideInInspector] public bool finishedAnimation;
    private string[] runAnimation = {"Run_W","Run_E"};
    private string[] idleAnimation = {"Idle_W","Idle_E"};
    private string[] dieAnimation = { "Die_W", "Die_E" };
    private string[] hurtAnimation = {"Hurt_W","Hurt_E"};
    private string[] attackAnimation = {"Attack_W","Attack_E"};
    private Enemy scriptEnemy;
    private int lastDirection;
    // Start is called before the first frame update
    void Start()
    {
        finishedAnimation = false;
    }
    private void Awake()
    {
        scriptEnemy = GetComponent<Enemy>();
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        string[] directionArray = null;
        if (scriptEnemy.direction == "West")
        {
            lastDirection = 0;
        }else if(scriptEnemy.direction == "East")
        {
            lastDirection = 1;
        }
        directionArray = getAnimation();
        animator.Play(directionArray[lastDirection]);
    }
    private string[] getAnimation()
    {
        switch (scriptEnemy.stateOfEnemy)
        {
            case "Idle":
                return idleAnimation;
            case "Run":
                return runAnimation;
            case "Hurt":
                return hurtAnimation;
            case "Die":
                return dieAnimation;
            case "Attack":
                return attackAnimation;

        }
        return idleAnimation;

    }

    public void alertWhenAnimationEnded()
    {
        if(scriptEnemy.stateOfEnemy == "Attack")
        {
            finishedAnimation = true;
            scriptEnemy.stateOfEnemy = "Idle";
        }else if(scriptEnemy.stateOfEnemy == "Hurt")
        {
            if (GetComponentInChildren<EnemyAttackMelee>())
            {
                if (GetComponentInChildren<EnemyAttackMelee>().targetForAttack.Count == 0 )
                {
                    scriptEnemy.stateOfEnemy = "Run";
                }
                else
                {
                    scriptEnemy.stateOfEnemy = "Idle";
                }


            } //Testa se o inimigo esta em combate, caso nao estiver ele so volta a andar
            else if (GetComponentInChildren<EnemyAttackShooter>())
            {
                if (GetComponentInChildren<EnemyAttackShooter>().targetForAttack.Count == 0 )
                {
                     scriptEnemy.stateOfEnemy = "Run";
                }
                else
                {
                    scriptEnemy.stateOfEnemy = "Idle";
                }

            }



        }
        else if(scriptEnemy.stateOfEnemy == "Die")
        {
            finishedAnimation = true;  
          

        }
    }
}
