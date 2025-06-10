using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputDrop : MonoBehaviour, IDropHandler
{
    public TextMeshProUGUI slotText;

    void Start()
    {
        slotText = gameObject.transform.Find("InputText").GetComponent<TextMeshProUGUI>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        string symbol = GhostButtonController.Instance.CurrentSymbol;

        slotText.text = symbol;
    }
}


