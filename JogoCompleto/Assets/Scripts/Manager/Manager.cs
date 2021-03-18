using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    private int currentWave;
    // Start is called before the first frame update
    void Start()
    {
        Wave = 0;
        currentWave = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int Wave
    {
        get
        {
            return currentWave;
        }
        set
        {
            currentWave++;
        }
    }
}
