using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerSound : MonoBehaviour
{
    private GameObject native;
    private GameObject enemy;
    public float timeForSoundNative;
    public float timeForSoundEnemy;
    private float currentTimeNative;
    private float currentTimeEnemy;
    // Start is called before the first frame update
    void Start()
    {
        currentTimeNative = Time.time;
        currentTimeEnemy = Time.time;
        native = null;
        enemy = null;
    }

    // Update is called once per frame
    void Update()
    {
        native = GameObject.FindGameObjectWithTag("Warrior");
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        if (native != null)
        {
            if (Time.time - currentTimeNative > timeForSoundNative)
            {
                native.GetComponent<Sound>().playAudio("Idle");
                currentTimeNative = Time.time;
                native = null;
            }

        }else if (enemy != null)
        {
            if (Time.time - currentTimeEnemy > timeForSoundEnemy)
            {
                enemy.GetComponent<Sound>().playAudio("Run");
                currentTimeEnemy = Time.time;
                enemy = null;
            }

        }

    }
}
