using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    public GameObject player;
    public GameObject winMessage;
    public float messageDuration = 5f;

    private Vector3 playerStartPosition;
    private BallSpawner ballSpawner;

    // For teleporting them back.
    private void Start()
    {
        winMessage.SetActive(false);
        playerStartPosition = player.transform.position;
        ballSpawner = player.GetComponent<BallSpawner>();
    }

    // Wait for Player to touch.
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !ballSpawner.touchingFloor)
        {
            StartCoroutine(ShowMessageAndHideAfterDelay(messageDuration));
            StartCoroutine(TeleportPlayerAfterDelay(collision.gameObject, messageDuration));
        }
    }

    // Show win message.
    private IEnumerator ShowMessageAndHideAfterDelay(float delay)
    {
        winMessage.SetActive(true);
        yield return new WaitForSeconds(delay);
        winMessage.SetActive(false);
    }

    // Teleport them back.
    private IEnumerator TeleportPlayerAfterDelay(GameObject player, float delay)
    {
        yield return new WaitForSeconds(delay);
        player.transform.position = playerStartPosition;
        Rigidbody playerRb = player.GetComponent<Rigidbody>();
        playerRb.velocity = Vector3.zero;
        playerRb.angularVelocity = Vector3.zero;
    }
}