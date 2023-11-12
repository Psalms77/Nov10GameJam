using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyController : Observer
{
    public ObjectPool<GameObject> pool;
    public float hp = 40;
    public float attack = 10;
    public GameObject bulletPrefab;
    public GameObject bulletPosition;
    public float fireRate = 2f;
    private float timer = 0f;
    public int enemyType = 1;  //1 remote 2// close
    public float scaleSpeed = 20.0f;
    public float maxScale = 15.0f;
    public int count = 0;
    public float attackRange = 5f;
    public GameObject checkPlayer;
    public float frozenTime = 2f;
    Rigidbody2D rb;
    private void Awake()
    {
        AddEventListener(EventName.EnemyTakePollution, (object[] arg) =>
        {
        TakePollution((float)arg[0], (float)arg[1],(GameObject)arg[2]);
        });
        AddEventListener(EventName.EnemyTakesDmg, (object[] arg) =>
        {
            TakeDamage((float)arg[0]);
        });
        count = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {   
        GameManager.instance.SetFacingOnPlant(this.gameObject);
        timer += Time.deltaTime;
        Collider2D target = Physics2D.OverlapCircle(checkPlayer.transform.position, attackRange);                     
        if (target != null && target.CompareTag("Player"))
        {
        if (timer >= 1f / fireRate)
        {
            Shoot();
            timer = 0f;
        }
        }
        if (timer >= frozenTime)
        {

            rb.constraints = RigidbodyConstraints2D.FreezePosition;
            
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

    public void TakePollution(float dmg,float dmg2,GameObject gameObject)
    {

        if (count <= 5)
        {

            if (this.gameObject == gameObject)
            {
                attack += dmg;
                fireRate += dmg2;
                if (transform.localScale.magnitude < 15f)
                { transform.localScale += Vector3.one * scaleSpeed * Time.deltaTime; }
                count++;
            }
        }
        
    }
    public void TakeDamage(float damage)
    {
        
        hp-=damage;
    }

    void OnDrawGizmosSelected()
    { 
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(checkPlayer.transform.position, attackRange);
    }
}
