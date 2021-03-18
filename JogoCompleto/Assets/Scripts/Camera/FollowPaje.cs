using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPaje : MonoBehaviour
{
    private float lastSizeCamera;
    // Start is called before the first frame update
    void Start()
    {
        lastSizeCamera = GetComponent<Camera>().orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = null;
        if (GameObject.FindGameObjectWithTag("Player")){
             player = GameObject.FindGameObjectWithTag("Player");
        }else if (GameObject.FindGameObjectWithTag("Gods"))
        {
            player = GameObject.FindGameObjectWithTag("Gods");
        }
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
    }
    public void cameraInTransfomationPaje(bool transformation)
    {
        if (transformation)
        {
 
            GetComponent<Camera>().orthographicSize = 1;
        }
        else
        {
            GetComponent<Camera>().orthographicSize = lastSizeCamera;
        }

    }
}
