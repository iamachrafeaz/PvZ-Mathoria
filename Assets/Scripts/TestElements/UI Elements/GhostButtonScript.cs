using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GhostButtonController : MonoBehaviour
{
    public static GhostButtonController Instance;

    public TextMeshProUGUI text;
    private RectTransform rect;
    [SerializeField] CanvasGroup canvasGroup;

    public string CurrentSymbol { get; private set; } = "";

    void Awake()
    {
        Instance = this;

        rect = GetComponent<RectTransform>();
        if (text == null) text = GetComponentInChildren<TextMeshProUGUI>();

        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;

        Hide();
    }

    public void Show(string symbol, Vector2 screenPosition)
    {
        CurrentSymbol = symbol;
        text.text = symbol;
        canvasGroup.alpha = 1f;
        rect.position = screenPosition;
    }

    public void Move(Vector2 screenPosition)
    {
        rect.position = screenPosition;
    }

    public void Hide()
    {
        canvasGroup.alpha = 0f;
        CurrentSymbol = "";
    }
}