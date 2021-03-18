using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;
public class ChooseHeroes : MonoBehaviour
{
    private GameObject player;
    private Animator animator;
    private bool animationFinished;
    private bool isTransforming;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animationFinished = false;

    }

    // Update is called once per frame
    void Update()
    {
 
    }
    public void soundOfTransformation()
    {

    }
    public void chooseHero(GameObject buttonOfHero)
    {

        if (GameObject.FindGameObjectWithTag("Player"))
        {

                buttonOfHero.GetComponent<LoadingGodsButton>().readyForTransformation = false;
                player = GameObject.FindGameObjectWithTag("Player");
                if (player.GetComponentInChildren<Life>().life > 0)
                {

                    if (player.GetComponent<PlayerAnimation>().stateOfPlayer != "Transformation")
                    {
                        if (!buttonOfHero.activeSelf)
                        {
                            player.GetComponent<Player>().darknessLightPlayer();
                        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FollowPaje>().cameraInTransfomationPaje(true);
                            buttonOfHero.SetActive(true);
                            buttonOfHero.GetComponent<LoadingGodsButton>().resetStart(); //Reseta o inicio do couldown


                            player.GetComponent<PlayerAnimation>().stateOfPlayer = "Transformation";
                            if (buttonOfHero.GetComponent<LoadingGodsButton>().Hero.name == "PaiDoMato")
                            {

                                player.GetComponent<PlayerAnimation>().animator.Play("Transformation_PaiDoMato");
                                player.GetComponent<Sound>().playAudio("Transformation_PaiDoMato");
                            }

                            else if (buttonOfHero.GetComponent<LoadingGodsButton>().Hero.name == "Boitata")
                            {
                                player.GetComponent<PlayerAnimation>().animator.Play("Transformation_Boitata");
                                player.GetComponent<Sound>().playAudio("Transformation_Boitata");
                            }
                            else if (buttonOfHero.GetComponent<LoadingGodsButton>().Hero.name == "MaeDoOuro")
                            {
                                player.GetComponent<PlayerAnimation>().animator.Play("Transformation_MaeDoOuro");
                                player.GetComponent<Sound>().playAudio("Transformation_MaeDoOuro");
                            }
                            Time.timeScale = 0;
                        }

                    }
                }
        }
    }
    public void startAnimationChoose()
    {

        if (animationFinished == false)
        {
            animator.Play("Choose");
        }else if (animationFinished == true)
        {
            animator.Play("ChooseEnd");
        }
    }
    public void animationChoose()
    {
        if (animationFinished == true)
        {
            animationFinished = false;
        }else if (animationFinished == false)
        {
            animationFinished =  true;
        }
        
        

    }

}
