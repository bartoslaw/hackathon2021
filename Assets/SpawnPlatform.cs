using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlatform : MonoBehaviour
{
    public Transform[] platforms;
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
            yield return new WaitForSeconds(Random.Range(5.0f, 10.0f));
            for (int i = 0; i < platforms.Length; ++i)
            {
                Instantiate(platforms[i], spawner.position, Quaternion.identity);
                gameObject.transform.localPosition = new Vector2(10, 20);
                gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y * Random.Range(2.0f, 2.0f));
            }
        }

    }
}
