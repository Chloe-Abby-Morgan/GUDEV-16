using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private string lod="SampleScene";
    public void gameStart()
    {
        SceneManager.LoadScene(lod);
    }
}
