using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class PlayerController : Observer
{

    public PlayerFSM stateMachine;
    public GameObject planet;
    public LineRenderer laserLineRenderer;
    public Rigidbody2D rb;
    public float moveSpeed = 1f;

    private void Awake()
    {
        stateMachine = new PlayerFSM(this);
    }



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 t = -planet.transform.position + this.gameObject.transform.position;
        
        if (Input.GetKey(KeyCode.D)) {
            rb.velocity = t.Perpendicular1().normalized * moveSpeed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = t.Perpendicular2().normalized * moveSpeed;
        }



    }
}
