using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void gameStart()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
