using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] PlayerHandler playerHandler;
   


    public void OnRestartButton()
    {
        playerHandler.BlockShoot();
        playerHandler.isSpawnable = false;
        Time.timeScale = 0.1f;
        panel.SetActive(true);


    }

    public void OnConfirm()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        playerHandler.isSpawnable = true;
        playerHandler.ResumeShoot();
    }

    public void OnDeny()
    {
        Time.timeScale = 1f;
        panel.SetActive(false);
        playerHandler.ResumeShoot();
    }

   
}
