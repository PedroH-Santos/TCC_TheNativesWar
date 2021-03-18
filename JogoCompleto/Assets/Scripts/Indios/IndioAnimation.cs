using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndioAnimation : MonoBehaviour
{
    [HideInInspector] public string stateWarrior;
    private string[] attackAnimation = { "Attack_E", "Attack_W" };
    private string[] idleAnimation = { "Idle_E", "Idle_W" };
    private string[] runAnimation = { "Run_E", "Run_W" };
    private string[] hurtAnimation = { "Hurt_E", "Hurt_W" };
    private string[] dieAnimation = { "Die_E", "Die_W" };

    private int lastDirection;
    private Animator animator;
    [HideInInspector] public string direction;
    [HideInInspector] public bool alertFinishedAnimation;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        alertFinishedAnimation = false;
    }

    // Update is called once per frame
    void Update()
    {
        setAnimation();
    }
    private string[] getAnimation()
    {
        switch (stateWarrior)
        {
            case "Attack":
                return attackAnimation;
            case "Idle":
                return idleAnimation;
            case "Run":
                return runAnimation;
            case "Hurt":
                return hurtAnimation;
            case "Die":
                return dieAnimation;
        }
        return idleAnimation;
    }
    private void setAnimation()
    {
        string[] directionAnimation = null;
        if (direction == "West")
        {
            lastDirection = 1;
        }else if (direction == "East")
        {
            lastDirection = 0;
        }
        directionAnimation = getAnimation();
        animator.Play(directionAnimation[lastDirection]);
    }
    private void finishedAnimation()
    {
        if (stateWarrior == "Attack")
        {
            stateWarrior = "Idle";
            alertFinishedAnimation = true;
        }else if(stateWarrior == "Hurt")
        {
            stateWarrior = "Idle";
        }else if (stateWarrior == "Die")
        {
            alertFinishedAnimation = true;
        }
    }
}
