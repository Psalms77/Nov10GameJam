using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyController : MonoBehaviour
{
    public ObjectPool<GameObject> pool;
    public int hp = 40;
    public int attack = 10;
    public GameObject bulletPrefab;
    public float fireRate = 2f;
    private float timer = 0f;

    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
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
            EventManager.SendNotification(EventName.PlayerTakesDmg,attack,this.gameObject);
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
  
        Instantiate(bulletPrefab, transform.position, transform.rotation);

    }



}
