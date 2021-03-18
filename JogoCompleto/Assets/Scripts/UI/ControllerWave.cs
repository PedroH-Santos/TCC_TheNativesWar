using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ControllerWave : MonoBehaviour
{
    // Start is called before the first frame update
    private int clicked;
    public GameObject[] rounds;
    public IconWave[] icons;
    private int currentWave;
    private UI ui;

    void Start()
    {
        clicked = 0;
        ui = GameObject.Find("UI").GetComponent<UI>();
     }

    // Update is called once per frame
    void Update()
    {
        currentWave = ui.ChangeWave;
    }
    public void CallOfButton()
    {

        clicked++;
 
        if (clicked == 1)
        {

            gameObject.transform.Find("Panel").gameObject.SetActive(true);
            if (rounds != null)
            {
                drawEnemyIcon();
                writeCountEnemy();
            }
        }else if (clicked == 2) {

            GameObject.FindGameObjectWithTag("GeneralWay").gameObject.GetComponent<ControllerStartWave>().startAllWays(); //Starta Todas as Waves
            clearIcons();
            clicked = 0;
            gameObject.transform.Find("Panel").gameObject.SetActive(false);
            ui.GetComponentInChildren<AnimationWaveTransition>().startAnimation(); // Pede para que a animação da wave inicie
            gameObject.SetActive(false);

        }
    }
    void OnDisable()
    {
        clicked = 0;
        gameObject.transform.Find("Panel").gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
    private void drawEnemyIcon()
    {
        foreach (GameObject waves in rounds)
        {
            for (int i = 0; i < icons.Length; i++)
            {
                if (icons[i].iconEnemy.texture == null)
                {
                    icons[i].iconEnemy.enabled = true;
                    icons[i].iconEnemy.texture = waves.GetComponent<Way>().waves[currentWave].enemyPrefab.GetComponent<SpriteRenderer>().sprite.texture;
                    break; //Uma linha de cada vez para que não tenha uma repetição das imagem formada
                }
                else if (icons[i].iconEnemy.texture == waves.GetComponent<Way>().waves[currentWave].enemyPrefab.GetComponent<SpriteRenderer>().sprite.texture)
                {

                    break;
                }
            }
        }
        for (int i = 0; i < icons.Length; i++)
        {
            if (icons[i].iconEnemy.texture == null)
            {
                icons[i].iconEnemy.enabled = false;
            }
        }

    } //Desenha o sprite do inimigo no quadro informativo da wave
    private void writeCountEnemy()//Escreve a quantidade de inimigos no quadro informativo da wave
    {
        for (int i = 0; i < icons.Length; i++)
        {   
            foreach (GameObject waves in rounds)
            {
                if (icons[i].iconEnemy.texture == waves.GetComponent<Way>().waves[currentWave].enemyPrefab.GetComponent<SpriteRenderer>().sprite.texture)
                {
                    icons[i].totalEnemys += waves.GetComponent<Way>().waves[currentWave].maxInimigos;
                }
            }
            if (icons[i].iconEnemy.texture != null)
            {
                icons[i].textWave.text = "x" + icons[i].totalEnemys;
                icons[i].totalEnemys = 0;
            }
            
        }
    }
    private void clearIcons()
    {
        for (int i = 0; i < icons.Length; i++)
        {
            icons[i].iconEnemy.texture = null;
            icons[i].textWave.text = " ";
            
        }
    }
    [System.Serializable]
    public class IconWave //Classe determinada para criar o quadro informativo da wave
    {
        public TextMeshProUGUI textWave;
        public RawImage iconEnemy;
        [HideInInspector]public int totalEnemys = 0;
    }
}
