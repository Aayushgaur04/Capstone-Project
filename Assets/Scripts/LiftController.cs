using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftManager : MonoBehaviour
{
    public GameObject leftWall, rightWall;
    public Transform liftTargetPosition;
    public float liftSpeed = 2f;
    public GameObject[] enemiesToSpawn;
    public Transform[] spawnPoints;
    public float enemySpawnDelay = 0.5f;
    public float checkDelay = 1f;

    private bool playerOnLift = false;
    private bool liftMoving = false;
    private bool enemiesSpawned = false;
    private bool checkingEnemies = false;
    private Transform player;

    private void Start()
    {
        leftWall.SetActive(false);
        rightWall.SetActive(false);
    }

    private void Update()
    {
        if (playerOnLift && !liftMoving)
        {
            StartCoroutine(StartLiftSequence());
        }

        if (enemiesSpawned && !checkingEnemies)
        {
            StartCoroutine(CheckEnemiesRemaining());
        }
    }

    private IEnumerator StartLiftSequence()
    {
        liftMoving = true;

        // Activate walls and reset their positions
        leftWall.SetActive(true);
        rightWall.SetActive(true);

        // Make player child of lift
        if (player != null)
        {
            player.SetParent(transform);
        }

        // Move lift to destination
        while (Vector2.Distance(transform.position, liftTargetPosition.position) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, liftTargetPosition.position, liftSpeed * Time.deltaTime);
            yield return null;
        }

        // Detach player
        if (player != null)
        {
            player.SetParent(null);
        }

        yield return new WaitForSeconds(0.5f);

        // Spawn enemies
        for (int i = 0; i < enemiesToSpawn.Length; i++)
        {
            Instantiate(enemiesToSpawn[i], spawnPoints[i].position, Quaternion.identity);
            yield return new WaitForSeconds(enemySpawnDelay);
        }

        enemiesSpawned = true;
    }

    private IEnumerator CheckEnemiesRemaining()
    {
        checkingEnemies = true;

        while (GameObject.FindGameObjectsWithTag("Enemy").Length > 0)
        {
            yield return new WaitForSeconds(checkDelay);
        }

        // All enemies defeated
        leftWall.SetActive(false);
        rightWall.SetActive(false);

        checkingEnemies = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!playerOnLift && collision.CompareTag("Player"))
        {
            playerOnLift = true;
            player = collision.transform;
        }
    }
}
