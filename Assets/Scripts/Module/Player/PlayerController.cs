using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class PlayerController : Observer
{
    public float hp = 100f;
    public float dmg = 10f;
    public float flybackTime = 0.5f;
    public float flybackMultiplier = 1f;
    public PlayerFSM stateMachine;
    public GameObject planet;
    public LineRenderer laserLineRenderer;
    public Rigidbody2D rb;
    public float moveSpeed = 1f;
    public float jumpParam = 1f;
    private Vector2 gravity;
    private SpriteRenderer _sr;
    private ParticleSystem _ps;
    private Animator _anim;

    private RaycastHit2D[] hits;
    private Transform shootingPoint;

    private void Awake()
    {
        stateMachine = new PlayerFSM(this);
        AddEventListener(EventName.PlayerTakesDmg, (object[] arg) =>
        {
            TakeDamage((float)arg[0], (GameObject)arg[1]);
        });

    }

    // Start is called before the first frame update
    void Start()
    {
        //init
        rb = GetComponent<Rigidbody2D>();
        laserLineRenderer = GetComponent<LineRenderer>();
        _anim = GetComponent<Animator>();
        _sr = GetComponent<SpriteRenderer>();
        _ps = transform.GetChild(0).GetComponent<ParticleSystem>();
        shootingPoint = GameObject.Find("shootingPoint").transform;
    }

    // Update is called once per frame
    void Update()
    {
        gravity = -planet.transform.position + this.gameObject.transform.position;
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

            hits = Physics2D.RaycastAll(shootingPoint.position, (GameManager.instance.mousePos - shootingPoint.position));
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].transform.CompareTag("Enemy"))
                {
                    EventManager.SendNotification(EventName.EnemyTakesDmg, dmg);
                }
            }

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


    public void TakeDamage(float dmg, GameObject damageSource)
    {
        DOTween.Kill("flyback");
        hp = hp - dmg;
        Vector3 flybackDir = - damageSource.transform.position + this.transform.position;
        this.transform.DOMove(this.transform.position + flybackDir *flybackMultiplier, flybackTime).SetEase(Ease.OutCubic).SetId("flyback");




    }

}
