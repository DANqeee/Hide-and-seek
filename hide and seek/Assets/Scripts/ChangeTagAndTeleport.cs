using UnityEngine;
using System.Collections;

public class ChangeTagAndTeleport : MonoBehaviour
{
    public GameObject player;
    public Transform playerTeleportPoint;
    public Transform hinterTeleportPoint;

    private void Start()
    {
        // Ensure player is set
        if (player == null)
        {
            player = this.gameObject;
        }
    }

    public void SetTagAndTeleport(string newTag)
    {
        Debug.Log("Setting tag to: " + newTag);
        player.tag = newTag;
        if (newTag == "Player")
        {
            Debug.Log("Teleporting Player");
            TeleportPlayer();
        }
        else if (newTag == "Hinter")
        {
            Debug.Log("Teleporting Hinter after 30 seconds");
            StartCoroutine(TeleportHinterWithDelay(15.0f));
        }
    }

    private void TeleportPlayer()
    {
        CharacterController con = player.GetComponent<CharacterController>();
        con.enabled = false;
        player.transform.position = playerTeleportPoint.position;
        con.enabled = true;
    }

    private IEnumerator TeleportHinterWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        CharacterController con = player.GetComponent<CharacterController>();
        Debug.Log("Teleporting to HinterTeleportPoint");
        con.enabled = false;
        player.transform.position = hinterTeleportPoint.position;
        con.enabled = true;
    }
}