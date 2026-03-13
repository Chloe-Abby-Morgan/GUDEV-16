using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private string lod="SampleScene";
    [SerializeField] private Animator Andy;
    public void gameStart()
    {
        StartCoroutine(loader());
    }

    IEnumerator loader()
    {
        Andy.SetTrigger("fading");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(lod);
    }
}
