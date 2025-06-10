using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ZXing;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;
using System.Collections;

public class QRScaner : MonoBehaviour
{
    // UI Elements
    [SerializeField] public RawImage _rawImageBackground;
    [SerializeField] private AspectRatioFitter _aspectRatioFitter;
    [SerializeField] private TextMeshProUGUI _textOut;

    // Camera variables
    private WebCamTexture _cameraTexture;
    private bool _isCamAvailable;
    bool _isScanning;

    // Initializes the camera setup
    void Start()
    {
        SetUpCamera();
    }

    // Called every frame to update camera feed and scan QR codes
    void Update()
    {
        UpdateCameraRender();

        if (_isCamAvailable == true && !_isScanning)
        {
            Scan();
        }
    }

    // Updates the UI to properly render the webcam feed
    private void UpdateCameraRender()
    {
        if (_isCamAvailable == false)
        {
            return;
        }

        // Adjust vertical mirroring based on device orientation
        float scaleY = _cameraTexture.videoVerticallyMirrored ? -1f : 1f;
        _rawImageBackground.transform.localScale = new Vector3(1f, (float)scaleY, 1f);

        // Rotate the image based on camera rotation
        int orientation = -_cameraTexture.videoRotationAngle;
        _rawImageBackground.rectTransform.localEulerAngles = new Vector3(0, 0, orientation);
    }

    // Sets up the webcam for QR scanning
    private void SetUpCamera()
    {
        WebCamDevice[] devices = WebCamTexture.devices;

        if (devices.Length == 0)
        {
            _isCamAvailable = false;
            return;
        }

      //  For Android
                for (int i = 0; i < devices.Length; i++)
                {
                    if (devices[i].isFrontFacing == false)
                    {
                        _cameraTexture = new WebCamTexture(devices[i].name, 1280, 720);
                        break;
                    }

                }

        // Use the first available camera (macOS example)
        // _cameraTexture = new WebCamTexture(devices[0].name, Screen.width, Screen.width);

        if (_cameraTexture != null)
        {
            _cameraTexture.Play();
            _rawImageBackground.texture = _cameraTexture;
            _isCamAvailable = true;
        }
        else
        {
            _textOut.text = "No camera";
        }
    }

    // Scans the current camera frame for a QR code
    private void Scan()
    {
        try
        {
            IBarcodeReader barcodeReader = new BarcodeReader(); // ZXing reader
            var result = barcodeReader.Decode(_cameraTexture.GetPixels32(), _cameraTexture.width, _cameraTexture.height);

            _textOut.text = "Scan QR Code";

            if (result != null)
            {
                _isScanning = true; // Prevent further scans while processing
                if (result != null)
                {
                    string rawText = result.Text; // The scanned string, e.g., {"uid":"stu_xxxx","pin":"xxxx"}

                    try
                    {
                        QRCodeData data = JsonUtility.FromJson<QRCodeData>(rawText);

                        OnQRCodeScanned(data.uid, data.pin);
                    }
                    catch (Exception ex)
                    {
                        _isScanning = false;
                        Debug.LogError("Failed to parse QR code JSON: " + ex.Message);
                    }
                }

            }
        }
        catch (Exception ex)
        {
            Debug.LogWarning("QR Scan Error: " + ex.Message);
        }
    }

    // Handles actions after a QR code is successfully scanned
    async void OnQRCodeScanned(string scannedId, string password)
    {
        Dictionary<string, object> PlayerData = await FirebaseManager.Instance.GetPlayerData(scannedId);

        if (PlayerData != null && password.Equals(PlayerData["password"].ToString()))
        {
            Handheld.Vibrate();

            // Create and set the Player instance in GameManager
            Game.Instance.SetPlayer(
                new Player(
                    scannedId,
                    PlayerData["firstName"].ToString(),
                    PlayerData["lastName"].ToString(),
                    int.Parse(PlayerData["schoolGrade"].ToString())
                )
            );

            // Return to the main menu
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }
        else
        {
            _textOut.text = "Player not found or password incorrect. Try again";

            // Retry scanning after a short delay
            StartCoroutine(ResetScanAfterDelay(2f));
        }
    }

    // Waits before allowing the next scan attempt
    private IEnumerator ResetScanAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        _isScanning = false; // Re-enable scanning
    }

    // Called when the "Go Back" button is clicked
    public void OnGoBackButtonClick()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
