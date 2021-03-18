using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
public class Menu : MonoBehaviour
{
    public GameObject optionMenu;
    public GameObject imageControl;
    private bool checkShowControl;
    private bool gameIsPaused;


    public TMP_Dropdown resolutionDropDown;
    public AudioMixer audioMixer;
    private Resolution[] resolutions; 


    // Start is called before the first frame update
    void Start()
    {
        resolutions = Screen.resolutions; //Pega cada resoluação possivel dentro do computador do usuário
        resolutionDropDown.ClearOptions();
        List<string> options = new List<string>();

        int currentResolution = 0; //Index do tamanho da tela atual
        
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height; //Cria Texto da resoluação para ser colocado dentro da caixa de resoluções e mostrar ao usuário
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolution = i;
            }

        }
        resolutionDropDown.AddOptions(options); //Adiciona a caixa
        resolutionDropDown.value = currentResolution; //Fornece o valor atual para a caixa
        resolutionDropDown.RefreshShownValue(); //Atualiza a caixa

        


        gameIsPaused = false;
        checkShowControl = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause();
        }
    }
    public void pause()
    {
        if (gameIsPaused)
        {
            gameIsPaused = false;
            Time.timeScale = 1; //Volta ao jogo
            optionMenu.SetActive(false);
            imageControl.SetActive(false); //Minimiza a imagem que mostra os controles quando o jogador querer voltar ao jogo
        }
        else
        {
            gameIsPaused = true;
            Time.timeScale = 0;//Pausa Jogo
            optionMenu.SetActive(true);
            
        }
    }
    public void backToMap()
    {

        GameObject phase = GameObject.FindGameObjectWithTag("ManagerPhase");
        SceneManager.LoadScene("MapGame");
        phase.gameObject.GetComponent<ControllerStartPhases>().disableAllObjects(true); //Habilita o mapa do game novamente

    }
    public void showControl()
    {
        if (checkShowControl)
        {
            checkShowControl = false;
            imageControl.SetActive(false);
           
        }
        else
        {
            imageControl.SetActive(true);
            checkShowControl = true;
        }
    }

    public void setVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
     
     
    }
    public void setQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    public void setFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
    public void setResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

}
