using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryController : Observer
{
    public GameObject upgradePrefab;
    public GameObject pollutionPrefab;

    public Transform pollutionPoint;
    public Transform upgradePoint;
    private float spawnUpgradeTimer;
    public float spawnUpgradeTime;
    private float spawnPollutionTimer;
    public float spawnPollutionTime;

    public AudioSource pollutionSfx;
    public AudioClip[] pollution;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameManager.instance.SetFacingOnPlant(this.gameObject);
        SpawnStuff();


    }


    private void SpawnStuff()
    {
        if (spawnUpgradeTimer < spawnUpgradeTime)
        {
            spawnUpgradeTimer += Time.deltaTime;
        }
        else if (spawnUpgradeTimer >= spawnUpgradeTime)
        {
            Instantiate(upgradePrefab, upgradePoint.position, Quaternion.identity);
            pollutionSfx.PlayOneShot(pollution[Random.Range(0, 3)]);
            EventManager.SendNotification(EventName.PollutionSpawn);
            Instantiate(pollutionPrefab, pollutionPoint.position, Quaternion.identity);

            spawnUpgradeTimer = 0;
        }

        if (spawnPollutionTimer < spawnPollutionTime)
        {
            spawnPollutionTimer += Time.deltaTime;
        }
        else if (spawnPollutionTimer >= spawnPollutionTime)
        {
            //Instantiate(upgradePrefab);
            Instantiate(pollutionPrefab);

            spawnPollutionTimer = 0;
        }
    }




}
