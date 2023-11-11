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
    public float jumpParam = 1f;
    private Vector2 gravity;
    private Vector2 t;
    private void Awake()
    {
        stateMachine = new PlayerFSM(this);
        
    }



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        laserLineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        gravity = -planet.transform.position + this.gameObject.transform.position;
        //Debug.Log(GameManager.instance.mousePos);
        //ShootingLaser();
        transform.up = gravity;
        stateMachine.currentState.HandleUpdate();

    }


    public void MoveOnPlanet()
    {
        
        if (Input.GetKey(KeyCode.D)) {
            rb.velocity += gravity.Perpendicular1().normalized * moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity += gravity.Perpendicular2().normalized * moveSpeed * Time.deltaTime;
        }
    }

    public void JumpOnPlanet()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("jumped");
            rb.velocity += gravity.normalized * jumpParam * Time.deltaTime;
        }


    }


    public void ShootingLaser()
    {
        if (Input.GetMouseButton(0))
        {

            laserLineRenderer.SetColors(new Color(255, 255, 0, 0.5f), end: new Color(255, 255, 0, 0.5f));
            laserLineRenderer.SetWidth(0.2f, 0.2f);
            DrawLaser(this.gameObject.transform.position, (GameManager.instance.mousePos));

        }

        if (!Input.GetMouseButton(0))
        {
            laserLineRenderer.SetPosition(0, Vector3.zero);
            laserLineRenderer.SetPosition(1, Vector3.zero);
        }


    }

    public void DrawLaser(Vector2 startPos, Vector2 endPos)
    {
        laserLineRenderer.SetPosition(0, startPos);
        laserLineRenderer.SetPosition(1, endPos);
    }

}
