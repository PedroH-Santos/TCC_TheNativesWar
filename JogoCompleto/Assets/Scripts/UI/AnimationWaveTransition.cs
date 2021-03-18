using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class AnimationWaveTransition : MonoBehaviour
{
    // Start is called before the first frame update
    private UI ui;
    void Start()
    {
        ui = GetComponentInParent<UI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void startAnimation() //Inicia a animação
    {
            gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Wave " + ui.ChangeWave;
            gameObject.GetComponent<Animator>().SetBool("StartAnimation", true);
            gameObject.GetComponent<Animator>().Play("WaveAnimation");
        

    }
    public void stopAnimation() //Desabilita a animação para que ela não fique em loop
    {
        gameObject.GetComponent<Animator>().SetBool("StartAnimation", false);
    }
 
}
