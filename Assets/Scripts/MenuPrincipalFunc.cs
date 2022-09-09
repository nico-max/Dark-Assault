using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipalFunc : MonoBehaviour
{
    public void Jugar()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }

    public void Salir()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        Destroy(GameManager._instance.gameObject);
    }

    public void resumeGame()
    {
        GameManager._instance.ResumeGame();
    }

    public void restartLevel()
    {
        GameManager._instance.reiniciarNivel();
        Time.timeScale = 1;
        UIManager._instance.unsetMurio();
    }
}
