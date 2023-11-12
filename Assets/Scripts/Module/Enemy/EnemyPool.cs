
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyPool : MonoBehaviour
{
    public GameObject[] plant;
    public float spawnIntervals;
    private float spawnTimer;
    public List<Vector3> spawnPositions = new List<Vector3>();
    private ObjectPool<GameObject> pool;

    public int countAll;
    public int countActive;
    public int countInactive;
    private int randomIndex;
    private int randomPos;
    float CreatTime = 5f;
    float systemTime = 0f;


    private void Awake()
    {
        pool = new ObjectPool<GameObject>(createFunc, actionOnGet, actionOnRelease, actionOnDestroy, true, 10, 1000);
        int randomIndex = Random.Range(0, plant.Length);
    }

    GameObject createFunc()
    {
        var obj = Instantiate(plant[randomIndex], transform);
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
        randomIndex = Random.Range(0, plant.Length);
        countAll = pool.CountAll;
        countActive = pool.CountActive;
        countInactive = pool.CountInactive;

        //spawnTimer += Time.deltaTime;

        //if (spawnTimer >= spawnIntervals)
        //{
        //    spawnTimer -= spawnIntervals;
        //    Spawn();
        //}
        CreatTime -= Time.deltaTime;
        systemTime += Time.deltaTime;
        if (CreatTime <= 0 && systemTime<=30)    
        {
            CreatTime = Random.Range(8, 10);
            Spawn();
        }
        if (CreatTime <= 0 && systemTime <= 60&& systemTime>30)
        {
            CreatTime = Random.Range(4, 9);
            Spawn();
        }
        if (CreatTime <= 0 && systemTime <= 120 && systemTime > 60)
        {
            CreatTime = Random.Range(3, 7);
            Spawn();
        }
        if (CreatTime <= 0 && systemTime > 120)
        {
            CreatTime = Random.Range(1, 4);
            Spawn();
        }



    }

    private void Spawn()
    {
        randomPos= UnityEngine.Random.Range(0, spawnPositions.Count);
        Vector3 randomPosition = spawnPositions[randomPos];
        GameObject temp = pool.Get();
        temp.transform.position = spawnPositions[randomPos];
    }
}
