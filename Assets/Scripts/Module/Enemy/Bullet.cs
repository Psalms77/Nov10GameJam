using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float paodanSpeed = 10f;
    public float bulletSpeed = 600f;
    public float lifeTime = 2f;
    private Vector3 direction;
    private Rigidbody2D rb;
    public bool isPao;
    public float attack =10;
    
    // Start is called before the first frame update

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        direction = (GameManager.instance.GetPlayer().transform.position - transform.position).normalized;
        if (isPao)
        {
            rb.velocity = direction * paodanSpeed;
        }
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per framez
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (!isPao)
        {
            rb.velocity = direction * bulletSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == ("Player"))
        {
            EventManager.SendNotification(EventName.PlayerTakesDmg,attack,this.gameObject);
            Destroy(gameObject);

        }
    }
}
