using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public GameObject bouncyBallPrefab;
    public bool touchingFloor = false;
    public int ballsToSpawn = 50;
    public float teleportDelay = 2f;
    public float spawnDuration = 1f;
    public float destructionDelay = 3f;
    public Vector3 spawnOffset;
    

    private Vector3 startingPosition;

    // Log starting position for teleporting back to.
    private void Start()
    {
        startingPosition = transform.position;
    }

    // Check if the user has hit the lava.
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            touchingFloor = true;
            StartCoroutine(SpawnBalls(ballsToSpawn, spawnDuration));
            StartCoroutine(TeleportAfterDelay(spawnDuration + destructionDelay));
        }
    }

    // Mark when they arent touching lava anymore.
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            touchingFloor = false;
        }
    }

    // Spawn a ton of spheres super fast!
    private IEnumerator SpawnBalls(int count, float duration)
    {
        float interval = duration / count;

        for (int i = 0; i < count; i++)
        {
            GameObject newBall = Instantiate(bouncyBallPrefab, transform.position+spawnOffset, Quaternion.identity);
            newBall.GetComponent<Rigidbody>().velocity = Random.insideUnitSphere * 5f;

            StartCoroutine(DestroyAfterDelay(newBall, destructionDelay));
            yield return new WaitForSeconds(interval);
        }
    }

    // Bring them back to spawn, they failed.
    private IEnumerator TeleportAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        transform.position = startingPosition;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }

    // A neat way to destroy a given object after some time.
    private IEnumerator DestroyAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(obj);
    }
}