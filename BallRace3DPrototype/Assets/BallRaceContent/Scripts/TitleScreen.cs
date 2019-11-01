using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            LoadGame();
        } else if (Input.GetButtonDown("Cancel"))
        {
            QuitGame();
        }
    }


    // Start is called before the first frame update
    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }


    public void QuitGame()
    {
        Debug.Log("Quitting Application");
       Application.Quit();
    }
}
