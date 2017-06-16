using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab; // Prefab of object we want to spawn
    public float spawnRate = 1f; // spawn an object every spawn rate interval (in seconds)
    [HideInInspector]
    public List<GameObject> objects = new List<GameObject>();

    private float spawnTimer = 0f; // Counts up every frame in seconds

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(transform.position, transform.localScale);
    }

    // Generates a random point within the transform's scale
    Vector3 GenerateRandomPoint()
    {
        // Set halfScale to half of transform's scale
        Vector3 halfScale = transform.localScale * 0.05f;
        // Set randomPoint to Zero
        Vector3 randomPoint = Vector3.zero;
        // Set randomPoint x,y,z to Random range between -halfScale to halfScale (Hint: can do individually)
        randomPoint.x = Random.Range(-halfScale.x, halfScale.x);
        randomPoint.x = Random.Range(-halfScale.y, halfScale.y);
        randomPoint.z = Random.Range(-halfScale.z, halfScale.z);
        
        // Return randomPoint
        return randomPoint;
    }

    // Spawns the prefab at a given position and with rotation
    public void Spawn(Vector3 position, Quaternion rotation)
    {
        // SET clone to new instance of prefab
        GameObject clone = Instantiate(prefab);
        // ADD clone to objects
        objects.Add(clone);
        // SET clone's position to spawner's position + position
        clone.transform.position = transform.position + position;
        // Set clone's rotation to rotation
        clone.transform.rotation = rotation;
    }

    // Update is called once per frame
    void Update()
    {
        // SET spawnTimer to spawnTimer + delta time
        spawnTimer += Time.deltaTime;
        // IF spawnTimer > spawnRate
        if (spawnTimer > spawnRate)
        {
            // SET randomPoint to GenerateRandomPoint()
            Vector3 randomPoint = GenerateRandomPoint();
            // CALL Spawn() and pass randomPoint, Quaternion identity
            Spawn(randomPoint, Quaternion.identity);
            // SET spawnTimer to zero
            spawnTimer = 0f;
        }
    }
}
