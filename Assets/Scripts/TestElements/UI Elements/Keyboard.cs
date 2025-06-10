using TMPro;
using UnityEngine;

public class Keyboard : MonoBehaviour
{
    public GameObject ButtonPrefab;
    public GameObject Numbers;

    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject btn = Instantiate(ButtonPrefab, Numbers.transform);
            btn.name = "KeyboardButton - " + i;
            btn.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = i.ToString();
        }
    }
}
