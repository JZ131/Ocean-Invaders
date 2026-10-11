using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void StartGame()
    {
        // Jako parametr f-cji nazwa glownej sceny gry
        SceneManager.LoadScene("SampleScene");
    }

    public void OpenOptions()
    {
        Debug.Log("Opening options");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game closed");
    }

}
