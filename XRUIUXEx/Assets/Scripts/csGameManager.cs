using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class csGameManager : MonoBehaviour
{
    float maxTime = 20f;
    float timeLeft;

    public Button restartButton;
    public GameObject panel;
    public Text timer;

    // Start is called before the first frame update
    void Start()
    {
        timeLeft = maxTime;
        restartButton.onClick.AddListener(OnButtonClick);

        
    }
    // Update is called once per frame
    void Update()
    {
        /*if(timeLeft>0) {
            timeLeft -= Time.deltaTime;
            timer.text = ((int)timeLeft).ToString();
        }
        else
        {
            panel.SetActive(true);
        }*/
    }

    public void OnButtonClick()
    {
        Debug.Log("Restart");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
