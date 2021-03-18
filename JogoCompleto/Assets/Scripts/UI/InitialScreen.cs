using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class InitialScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void startGame()
    {
        SceneManager.LoadScene("MapGame");
    }
    public void exitGame()
    {
        Application.Quit();
    }
    public void config(GameObject config)
    {
        if (!config.activeInHierarchy)
        {
            config.gameObject.SetActive(true);
        }
        else
        {
            config.gameObject.SetActive(false);
        }
        
    }

}
