using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public void MainGame()
    {
        FindObjectOfType<AudioManager>().Play("tombol");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void KeluarGame()
    {
        FindObjectOfType<AudioManager>().Play("tombol");
        Application.Quit();
        Debug.Log("Keluar");
    }
    public void tombolAudio()
    {
        FindObjectOfType<AudioManager>().Play("tombol");
    }
}
