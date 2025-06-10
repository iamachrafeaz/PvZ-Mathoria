using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasScaler))]
public class LockUIScale : MonoBehaviour
{
    public float maxWidth = 2424f;
    public float maxHeight = 1080f;

    private CanvasScaler scaler;

    void Start()
    {
        scaler = GetComponent<CanvasScaler>();
        UpdateScale();
    }

    void Update()
    {
        UpdateScale();
    }

    void UpdateScale()
    {
        float width = Screen.width;
        float height = Screen.height;


        if (width > maxWidth && height > maxHeight)
        {
            Debug.Log(Screen.width + Screen.height);

            scaler.uiScaleMode = CanvasScaler.ScaleMode.ConstantPixelSize;
        }
        else
        {
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(maxWidth, maxHeight);
        }
    }
}
