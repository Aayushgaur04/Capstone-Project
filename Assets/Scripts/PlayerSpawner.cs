using UnityEngine;
using System.Collections;

public class PlayerSpawner : MonoBehaviour
{
    void Start()
    {
        // Wait one frame to ensure all SpawnPoints are in the scene
        StartCoroutine(DoSpawn());
    }

    private IEnumerator DoSpawn()
    {
        yield return null;

        string id = GameManager.Instance.nextSpawnID;
        if (!string.IsNullOrEmpty(id))
        {
            SpawnPoint[] points = FindObjectsOfType<SpawnPoint>();
            foreach (var sp in points)
            {
                if (sp.spawnID == id)
                {
                    transform.position = sp.transform.position;
                    break;
                }
            }
        }
    }
}
