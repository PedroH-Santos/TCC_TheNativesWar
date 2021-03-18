using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class LoadingGodsButton : MonoBehaviour
{
    private Vector3 startScale;
    private float startTime;
    public float timeForTransformation;
    public GameObject Hero;
    [HideInInspector] public bool readyForTransformation;
    // Start is called before the first frame update
    void Start()
    {
        readyForTransformation = true;
        startTime = Time.time;
        startScale = gameObject.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 localScale = gameObject.transform.localScale;
        if (gameObject.GetComponent<Slider>().value < 1)
        {
            gameObject.GetComponent<Slider>().value = (Time.time - startTime) / timeForTransformation;
        }
        else
        {
            readyForTransformation = true;
            gameObject.GetComponent<Slider>().value = 0;
            gameObject.SetActive(false);
        }
    }
    public void resetStart()
    {
        startTime = Time.time;
    }
    
}
