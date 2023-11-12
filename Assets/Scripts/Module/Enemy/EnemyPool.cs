
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyPool : MonoBehaviour
{
    public GameObject plant;
    public float spawnIntervals;
    private float spawnTimer;

    private ObjectPool<GameObject> pool;

    public int countAll;
    public int countActive;
    public int countInactive;
    private void Awake()
    {
        pool = new ObjectPool<GameObject>(createFunc, actionOnGet, actionOnRelease, actionOnDestroy, true, 10, 1000);
    }

    GameObject createFunc()
    {
        var obj = Instantiate(plant, transform);
        obj.GetComponent<EnemyController>().pool = pool;
        return obj;

    }

    void actionOnGet(GameObject obj)
    {
        obj.gameObject.SetActive(true);

    }

    void actionOnRelease(GameObject obj)
    { 
        obj.gameObject.SetActive(false);
    }

    void actionOnDestroy(GameObject obj)
    {
        Destroy(obj);   

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        countAll = pool.CountAll;
        countActive=pool.CountActive;
        countInactive=pool.CountInactive;

        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnIntervals)
        {
            spawnTimer -= spawnIntervals;
            Spawn();


        }


    }

    private void Spawn()
    {
       GameObject temp = pool.Get();


        temp.transform.position = new Vector3(UnityEngine.Random.Range(20f, 22f), UnityEngine.Random.Range(20f,22f));
    }
}
