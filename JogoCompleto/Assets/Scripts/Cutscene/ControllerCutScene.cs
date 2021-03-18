using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ControllerCutScene : MonoBehaviour
{
    // Start is called before the first frame update

    private float timeForDeadButtonOfJumpAnimation = 4f;
    private float currentTime;
    void Awake()
    {
        currentTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - currentTime >= timeForDeadButtonOfJumpAnimation)
        {
            gameObject.SetActive(false);
        }
    }
    public void jumpAnimation()
    {
        GameObject.FindGameObjectWithTag("Director").SetActive(false);
    }
}
