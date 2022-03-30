using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn2 : MonoBehaviour
{
    [SerializeField]
    private GameObject obj;
    float RandY;
    Vector2 whereToSpawn;
    [SerializeField]
    private float spawnRate = 2f;
    float nextSpawn = 0.0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnRate;
            RandY = Random.Range(-2f, 2f);
            whereToSpawn = new Vector3(RandY, transform.position.x);
            Instantiate(obj, whereToSpawn, Quaternion.identity);
        }
    }
}
