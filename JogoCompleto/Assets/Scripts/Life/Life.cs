using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{

    public float maxLife = 100;
    [HideInInspector]public float life;
    private float scaleStart;
    // Start is called before the first frame update
    void Start()
    {
        scaleStart = gameObject.transform.localScale.x;
        if (life == 0) //Caso o player trocar de heroi ou se transformar de volta ele continua com a vida que o mesmo perdeu
        {
            life = maxLife;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (life >= 0)
        {
            
            Vector3 scaleFinal = gameObject.transform.localScale;
            scaleFinal.x = life / maxLife * scaleStart;
            gameObject.transform.localScale = scaleFinal;
            Vector3 tmpScale = gameObject.transform.localScale;
            gameObject.transform.localScale = tmpScale;
        }



    }
}
