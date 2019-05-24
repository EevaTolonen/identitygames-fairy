// @author Eeva Tolonen
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Handles the spawning of enemy tears
public class WeepingWillowTears : MonoBehaviour
{
    public GameObject tearPrefab;
    public Transform tearSpawn;

    float tears = 20;
    float range = 25;

    float timeBetweenTears = 0.2f;
    public bool debugMode = false;


    private void Awake()
    {
        if (debugMode)
        {
            InvokeRepeating("SpawnTears", 1, 5);
        }
    }


    // Randomises tear direction
    private Vector2 RandomiseDirection()
    {
        return new Vector2(Random.Range(-200, 200), (Random.Range(200, 300)));
    }


    // Randomises tear spawn position in certain range
    private Vector3 RandomisePosition()
    {
        float posX = tearSpawn.position.x;
        float posY = tearSpawn.position.y;
        return new Vector3(Random.Range(posX - range, posX + range), posY, tearSpawn.position.z);
    }


    // Spawns tears in random positions and gives them different directions to go
    public void SpawnTears()
    {
        StartCoroutine(Spawn());
    }


    // Spawns tears in random positions and gives them different directions to go
    public IEnumerator Spawn()
    {
        for (int i = 0; i < tears; i++)
        {
            Instantiate(tearPrefab, RandomisePosition(), tearSpawn.rotation).GetComponent<Tear>().Shoot(RandomiseDirection());
            yield return new WaitForSeconds(timeBetweenTears);
        }
    }
}
