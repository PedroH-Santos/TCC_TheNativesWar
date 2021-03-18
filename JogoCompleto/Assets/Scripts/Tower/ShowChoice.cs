using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowChoice : MonoBehaviour
{
    private bool isShow;
    public GameObject chooseGameObject;
    // Start is called before the first frame update
    void Start()
    {
        isShow = false;

    }

    // Update is called once per frame
    void Update()
    {

        //Caso o player saia do range da torre as opções também são desabilitadas
        if (gameObject.transform.parent.tag != "Tower")
        {
            if (GetComponentInParent<DrawCircleInScreen>().enabled == false)
            {
                if (chooseGameObject != null) { 
                    chooseGameObject.SetActive(false);
                    isShow = false;
                }
            }
        }

    }   
    public void showChoiceOfTower()
    {

        if (gameObject.transform.parent.tag != "Tower")
        {
            if (GetComponentInParent<DrawCircleInScreen>().enabled == true && isShow == true) //Caso as opções e o range estejam sendo mostrados eles são desabilitados
            {
                GetComponentInParent<DrawCircleInScreen>().enabled = false;
                GetComponentInParent<LineRenderer>().enabled = false;
                isShow = false;
                chooseGameObject.gameObject.SetActive(false);
            }
            else //Caso as opções e o range não estejam sendo mostrados eles são habilitados
            {
                GetComponentInParent<DrawCircleInScreen>().enabled = true;
                GetComponentInParent<LineRenderer>().enabled = true;
                isShow = true;
                chooseGameObject.gameObject.SetActive(true);

            }
        }
        else
        {
            if (isShow)
            {
                chooseGameObject.gameObject.SetActive(false);
                isShow = false;
            }
            else
            {
                chooseGameObject.gameObject.SetActive(true);
                isShow = true;



            }
        }


    }



}
