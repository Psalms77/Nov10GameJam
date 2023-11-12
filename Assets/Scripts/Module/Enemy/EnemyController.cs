using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyController : Observer
{
    public ObjectPool<GameObject> pool;
    public int hp = 40;
    public int attack = 10;
    public GameObject bulletPrefab;
    public GameObject bulletPosition;
    public float fireRate = 2f;
    private float timer = 0f;
    public int enemyType = 1;  //1 remote 2// close
    public float scaleSpeed = 1.0f;
    public float maxScale = 15.0f;

    private void Awake()
    {
        AddEventListener(EventName.EnemyTakePollution, (object[] arg) =>
        {
            TakePollution((float)arg[0], (float)arg[1]);
        });
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        GameManager.instance.SetFacingOnPlant(this.gameObject);
        timer += Time.deltaTime;
    
        
        if (timer >= 1f / fireRate)
        {
            Shoot();
            timer = 0f;
        }

        EnemyDie();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == ("Player"))
        {
            EventManager.SendNotification(EventName.PlayerTakesDmg, attack, this.gameObject);
        }
    }

    void EnemyDie()
    {
        if (hp <= 0)
        {
            pool.Release(gameObject);
        }

    }

    void Shoot()
    {

        if (enemyType == 1)
        {
            Instantiate(bulletPrefab, bulletPosition.transform.position, bulletPosition.transform.rotation);
        }
    }

    public void TakePollution(float dmg,float dmg2)
    {
        attack += (int)dmg;
        fireRate += dmg2;
        if (transform.localScale.magnitude < 15f)
        { transform.localScale += Vector3.one * scaleSpeed * Time.deltaTime; }


    }

}
