using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    public GameObject[] buttonPrefabs; 
    public Transform[] spawnParent;
    public Card card;
    private List<GameObject> spawnedButtons = new List<GameObject>();

    void Start()
    {
        SpawnRandomButtons(3);
    }

    public void SpawnRandomButtons(int count)
    {
        ClearButtons();

        for (int i = 0; i < count; i++)
        {
            int index = Random.Range(0, buttonPrefabs.Length);
            GameObject newButton = Instantiate(buttonPrefabs[index], spawnParent[i]);

            spawnedButtons.Add(newButton);

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
            else if (name.Contains("heal"))
            {
                btn.onClick.AddListener(card.heal);
            }
        }
    }
    private void ClearButtons()
    {
        foreach (GameObject btn in spawnedButtons)
        {
            if (btn != null)
                Destroy(btn);
        }

        spawnedButtons.Clear();
    }
}
