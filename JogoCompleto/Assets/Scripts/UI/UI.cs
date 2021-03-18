using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class UI : MonoBehaviour
{
    public TextMeshProUGUI lifeText;
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI goldText;
    public int lifePlayer;
    public int goldPlayer;
    public GameObject gameWonImage;
    public GameObject gameOverImage;
    private GameObject[] Ways;
    private int currentWave;
    private ControllerStartPhases controllerPhase;
    // Start is called before the first frame update
    void Start()
    {
        Ways = GameObject.FindGameObjectsWithTag("Way");
        ChangeGold = goldPlayer;
        ChangeLife = lifePlayer;
        ChangeWave = 0;
        Time.timeScale = 1; //Despausa o jogo
    }

    // Update is called once per frame
    void Update()
    {
        if (lifePlayer <= 0)
        {
            lifePlayer = 0;
            gameOver();
            
        }
        else
        {
            gameWon();
        }

    }
    private void gameWon()
    {
        bool won = false;

        foreach (GameObject way in Ways)
        {
            if (currentWave > way.GetComponent<Way>().waves.Length -1)//Testa se o player já passou todas as waves
            {
                won = true;
            }
            else
            {
                won = false;
            }
        }
            if (won) 
            {
                Time.timeScale = 0;
                gameWonImage.SetActive(true);
            }
            else
            {
                if (GameObject.FindGameObjectWithTag("GeneralWay").gameObject.GetComponent<ControllerStartWave>().testIfAllWaysAlreadyFinished()) //Testa se todas os caminhos estão prontas para passar de wave
                {
                    ChangeWave++;
                    GameObject.FindGameObjectWithTag("GeneralWay").gameObject.GetComponent<ControllerStartWave>().resetAllWaves();
                }

            }



    }
  
    private void gameOver()
    {
        gameOverImage.SetActive(true);
        Time.timeScale = 0;
    }
    public void restart()
    {
       SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void addPhase() //Chama a cena principal (mapGame) para alterar a fase
    {
        GameObject phase = GameObject.FindGameObjectWithTag("ManagerPhase");
        SceneManager.LoadScene("MapGame");
        phase.GetComponentInChildren<ControllerStartPhases>().addsPhase();
  
    }
    public int ChangeWave
    {
        get
        { 
            return currentWave;
        }
        set
        { 
           
            currentWave = value;
            waveText.text = "" + currentWave;

          


        }
    }
    public int ChangeGold
    {
        get
        {
            return goldPlayer;
        }
        set
        {
            goldPlayer = value;
            goldText.text = "" + goldPlayer;


        }
    }
    public int ChangeLife
    {
        get
        {
            return lifePlayer;

        }
        set
        {

            lifePlayer = value;
            lifeText.text = "" + lifePlayer;

        }
    }

}
