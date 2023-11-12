
using System.Collections;
using System.Collections.Generic;
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

        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnIntervals)
        {
            spawnTimer -= spawnIntervals;
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
