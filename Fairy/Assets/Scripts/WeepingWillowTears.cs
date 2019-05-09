using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeepingWillowTears : MonoBehaviour
{
    public GameObject tearPrefab;
    public Transform tearSpawn;
    float tears = 10;
    float range = 25;

    float timeBetweenTears = 0.2f;


    private Vector2 RandomiseDirection()
    {
        return new Vector2(Random.Range(-200, 200), (Random.Range(200, 300)));
    }

    private Vector3 RandomisePosition()
    {
        float posX = tearSpawn.position.x;
        float posY = tearSpawn.position.y;
        return new Vector3(Random.Range(posX - range, posX + range), (Random.Range(posY - range, posY + range)), tearSpawn.position.z);
    }



    public void SpawnTears()
    {
        StartCoroutine(Spawn());
    }



    public IEnumerator Spawn()
    {
        for (int i = 0; i < tears; i++)
        {
            Instantiate(tearPrefab, RandomisePosition(), tearSpawn.rotation).GetComponent<Tear>().Shoot(RandomiseDirection());
            yield return new WaitForSeconds(timeBetweenTears);
        }
    }

    /*    for (int i = 0; i < tears; i++)
    {

    }*/
}
