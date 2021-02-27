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
        StartCoroutine(SpawnEnemy());
        camera = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    IEnumerator SpawnEnemy()
    {
        Transform cameraPosition = GameObject.FindGameObjectWithTag("MainCamera").transform;

        while (true)
        {
            spawner = GameObject.FindGameObjectWithTag("Spawner").transform;
            float randomNum = Random.Range(min, max);
            yield return new WaitForSeconds(2.0f);

            spawner.position = new Vector2(spawner.position.x, Random.Range(min, max));

            if (randomNum < 0.5f)
            {
                Instantiate(enemies[0], spawner.position, Quaternion.identity);
            } else
            {
                Instantiate(enemies[1], spawner.position, Quaternion.identity);
            }
        }
        
    }
}
