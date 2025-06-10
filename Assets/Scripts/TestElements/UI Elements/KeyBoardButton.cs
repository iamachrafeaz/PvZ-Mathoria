using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class KeyboardButton : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public string symbol;

    void Update()
    {
        symbol = gameObject.transform.Find("Text").GetComponent<TextMeshProUGUI>().text;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        GhostButtonController.Instance.Show(symbol, eventData.position);
    }

    public void OnDrag(PointerEventData eventData)
    {
        GhostButtonController.Instance.Move(eventData.position);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GhostButtonController.Instance.Hide();
    }
}