using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    public GameObject[] buttonPrefabs; 
    public Transform[] spawnParent;
    public Card card;      

    void Start()
    {
        SpawnRandomButtons(3);
    }

    void SpawnRandomButtons(int count)
    {
        for (int i = 0; i < count; i++)
        {
            int index = Random.Range(0, buttonPrefabs.Length);
            GameObject newButton = Instantiate(buttonPrefabs[index], spawnParent[i]);

            RectTransform rt = newButton.GetComponent<RectTransform>();
            rt.anchoredPosition = Vector2.zero;
            rt.localScale = Vector3.one;
            rt.offsetMin = Vector2.zero;
            rt.offsetMax = Vector2.zero;

            Button btn = newButton.GetComponent<Button>();

            string name = newButton.name.ToLower();

            if(name.Contains("up"))
            {
                btn.onClick.AddListener(card.up);
            }
            else if (name.Contains("down"))
            {
                btn.onClick.AddListener(card.down);
            }
            else if (name.Contains("left"))
            {
                btn.onClick.AddListener(card.left);
            }
            else if (name.Contains("right"))
            {
                btn.onClick.AddListener(card.right);
            }
        }
    }
}
