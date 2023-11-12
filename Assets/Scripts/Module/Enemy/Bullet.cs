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
    private SpriteRenderer rbSprite;
    private Animator bulletAni;
    // Start is called before the first frame update

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rbSprite = rb.GetComponent<SpriteRenderer>();
        bulletAni=GetComponent<Animator>();
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
        if (direction.x > 0) // 玩家面朝右
        {
            rbSprite.flipX = false; // 不翻转
        }
        else if (direction.x < 0) // 玩家面朝左
        {
            rbSprite.flipX = true; // 翻转
        }
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
            EventManager.SendNotification(EventName.PlayerTakesDmg, attack, this.gameObject);
            bulletAni.SetTrigger("Explo");
            if (bulletAni.GetCurrentAnimatorStateInfo(0).normalizedTime>=0.99f)
            { Destroy(gameObject); }
          
            

        }
    }
}
