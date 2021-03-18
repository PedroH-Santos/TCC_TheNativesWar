using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerStartWave : MonoBehaviour
{
    private GameObject[] ways;
    private GameObject[] controllers;
    // Start is called before the first frame update
    void Start()
    {
        ways = GameObject.FindGameObjectsWithTag("Way");
        controllers = GameObject.FindGameObjectsWithTag("ControllerWave");
    }

    // Update is called once per frame
    void Update()
    {
        
       
    }
    public bool testIfAllWaysAlreadyFinished() //Testa se todos os caminhos já acabaram 
    {
        foreach (GameObject way in ways)
        {
            if (!way.GetComponent<Way>().finishedWave)
            {
                return false;
            }
        }
        return true;
    }
    public void resetAllWaves() //Reseta as caracteristicas de todas as waves
    {
        foreach (GameObject way in ways)
        {
            way.GetComponent<Way>().finishedWave = false;
            way.GetComponent<Way>().startWave = false;
        }
        modifyControllers(true);
    }
    public void startAllWays() //Inicia todas as waves ao mesmo tempo
    {
        foreach (GameObject way in ways)
        {
            way.GetComponent<Way>().startOfWave();
        }
        modifyControllers(false);
       

    }
    void modifyControllers(bool condition)
    {
        foreach (GameObject controller in controllers)
        {
            controller.transform.GetChild(0).gameObject.SetActive(condition);
        }

    }
}
