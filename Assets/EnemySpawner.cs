using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] enemies;
    public Transform spawner;
    public Transform camera;

    private float min = 0.0f;
    private float max = 1.0f;


    void Start()
    {
        spawner = GameObject.FindGameObjectWithTag("Spawner").transform;
        StartCoroutine(SpawnEnemy());
        camera = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            float randomNum = Random.Range(min, max);
            yield return new WaitForSeconds(2.5f);

            if (randomNum < (max / 0.5f))
            {
                spawner.position = new Vector2(spawner.position.x, Random.Range(min, max));
                Instantiate(enemies[0], spawner.position, Quaternion.identity);
            } else
            {
                spawner.position = new Vector2(spawner.position.x, Random.Range(min, max));
                Instantiate(enemies[1], spawner.position, Quaternion.identity);
            }
        }
        
    }
}
