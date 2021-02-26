using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] enemies;
    public Transform spawner;

    void Start()
    {
        spawner = GameObject.FindGameObjectWithTag("Spawner").transform;
        StartCoroutine(SpawnEnemy());
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(3.0f, 12.0f));
            for (int i = 0; i < enemies.Length; ++i)
                Instantiate(enemies[i], spawner.position, Quaternion.identity);
        }
        
    }
}
