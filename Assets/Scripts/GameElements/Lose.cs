using System.Collections;
using UnityEngine;

public class Lose : MonoBehaviour
{
    [SerializeField] GameObject DangerLayer; // GameObject that contains animation and warning elements.

    // When the zombies arrive at the house, show a warning to the player and decrease their won suns.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 7)
        {
            DangerLayer.SetActive(true); // Show Warning GameObject
            DangerLayer.transform.GetComponent<Animation>().Play(); // Play animation

            StartCoroutine(WaitAndHideDangerPanel(1f)); // Wait one second before hiding the warning gameObject (for a better user experience)

            // Decrease the amount of won suns only if it greater than 0
            if (GameManager.Instance.getSuns() > 0)
            {
                GameManager.Instance.setSuns(-50);
            }
        }
    }

    IEnumerator WaitAndHideDangerPanel(float delay)
    {
        yield return new WaitForSeconds(delay);
        DangerLayer.SetActive(false);
    }
}
