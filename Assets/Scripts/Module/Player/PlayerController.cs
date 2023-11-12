using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine.UI;

public class PlayerController : Observer
{
    public RaycastHit2D[] groundcheck;
    public float absVelo;
    public float speedLimit = 5.5f;
    public float maxHp = 100f;
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

    private float damageCoolDown = 0.5f;
    private float damageCooldownTimer;
    private bool canDealDamage = true;

    public bool isgrounded = false;

    public AudioSource sfxSource;
    public AudioClip jetpack;
    public AudioClip upgradesfx;
    public AudioClip[] hitEnemy;
    public AudioClip[] shoot;

    public Slider hpbar;
    public GameObject losepanel;

    private void Awake()
    {
        stateMachine = new PlayerFSM(this);
        AddEventListener(EventName.PlayerTakesDmg, (object[] arg) =>
        {
            TakeDamage((float)arg[0], (GameObject)arg[1]);
        });
        AddEventListener(EventName.PlayerTakeUpgrade, (object[] arg) =>
        {
            TakeUpgrade((float)arg[0]);
        });

    }

    // Start is called before the first frame update
    void Start()
    {
        //init
        rb = GetComponent<Rigidbody2D>();
        laserLineRenderer = GetComponent<LineRenderer>();
        _anim = GetComponent<Animator>();
        sfxSource = GetComponent<AudioSource>();
        _sr = GetComponent<SpriteRenderer>();
        _ps = transform.GetChild(0).GetComponent<ParticleSystem>();
        shootingPoint = GameObject.Find("shootingPoint").transform;
        hpbar.maxValue = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        absVelo = rb.velocity.magnitude;

        gravity = -planet.transform.position + this.gameObject.transform.position;
        transform.up = gravity;
        GroucndCheckRaycast();
        AnimatorControl();
        DamageCoolDown();
        hpbar.value = maxHp - hp;
        if (hp < 0)
        {
            LosePanel();
        }
        stateMachine.currentState.HandleUpdate();
    }

    public void DamageCoolDown()
    {

        if (damageCooldownTimer<damageCoolDown)
        {
            damageCooldownTimer += Time.deltaTime;
            canDealDamage = false;
        }
        else
        {
            damageCooldownTimer = 0;
            canDealDamage = true;
        }


    }


    public void MoveOnPlanet()
    {

        if (Input.GetKey(KeyCode.D)) {
            _sr.flipX = false;
            if (absVelo >= speedLimit)
            {
                Vector2 temp = -Vector2.Perpendicular(gravity).normalized;
                rb.velocity += temp * moveSpeed * Time.deltaTime;
                rb.AddForce(-temp);
            }
            else
            {
                Vector2 temp = -Vector2.Perpendicular(gravity).normalized;
                rb.velocity += temp * moveSpeed * Time.deltaTime;
            }


        }
        if (Input.GetKey(KeyCode.A))
        {
            _sr.flipX = true;
            if (absVelo >= speedLimit) {
                Vector2 temp = Vector2.Perpendicular(gravity).normalized;
                rb.velocity += temp * moveSpeed * Time.deltaTime;
                rb.AddForce(-temp);
            }
            else
            {
                Vector2 temp = Vector2.Perpendicular(gravity).normalized;
                rb.velocity += temp * moveSpeed * Time.deltaTime;
            }
        }
    }

    public void JumpOnPlanet()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
 
            sfxSource.PlayOneShot(jetpack);
        }


        if (Input.GetKey(KeyCode.Space))
        {
            //Debug.Log("jumped");
            _ps.Play();
            rb.velocity += gravity.normalized * jumpParam * Time.deltaTime;
        }
        if (!Input.GetKey(KeyCode.Space))
        {
            _ps.Pause();
        }



    }


    public void ShootingLaser()
    {
        if (Input.GetMouseButtonDown(0)) {
            //Debug.Log(shoot[Random.Range(0, 3)]);
            sfxSource.PlayOneShot(shoot[Random.Range(0,3)]); }

        if (Input.GetMouseButton(0))
        {

            hits = Physics2D.RaycastAll(shootingPoint.position, (GameManager.instance.mousePos - shootingPoint.position), (GameManager.instance.mousePos - shootingPoint.position).magnitude);
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].transform.CompareTag("Enemy"))
                {
                    Debug.Log("hit");
                    if (canDealDamage)
                    {
                        sfxSource.PlayOneShot(hitEnemy[Random.Range(0, 3)]);
                        EventManager.SendNotification(EventName.EnemyTakesDmg, dmg, hits[i].transform.gameObject);
                    }
                }
            }

            laserLineRenderer.SetColors(new Color(255, 255, 0, 1f), end: new Color(255, 255, 0, 1f));
            laserLineRenderer.SetWidth(0.3f, 0.3f);
            DrawLaser(shootingPoint.position, (GameManager.instance.mousePos));



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
        Vector3 flybackDir = -damageSource.transform.position + this.transform.position;
        this.transform.DOMove(this.transform.position + flybackDir * flybackMultiplier, flybackTime).SetEase(Ease.OutCubic).SetId("flyback");

    }
    public void TakeUpgrade(float upgrade)
    {
        sfxSource.PlayOneShot(upgradesfx);
        dmg += upgrade;
        hp += upgrade;

    }



    void GroucndCheckRaycast()
    {
        groundcheck = Physics2D.RaycastAll(this.gameObject.transform.position, -gravity.normalized, 2.5f);
        for (int i = 0; i < groundcheck.Length; i++)
        {
            if (groundcheck[i].transform.CompareTag("Planet"))
            {
                isgrounded = true; break;
            }
            else
            {
                isgrounded = false;
            }
        }
    }


    void AnimatorControl()
    {
        _anim.SetFloat("absVelo", absVelo);
        if (isgrounded) { _anim.SetBool("isGrounded", true);_anim.SetBool("isInAir", false); }
        if (!isgrounded) { _anim.SetBool("isGrounded", false); _anim.SetBool("isInAir", true); }

    }


    public void LosePanel()
    {
        losepanel.SetActive(true);
    }



#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        //Gizmos.DrawLine(this.gameObject.transform.position, );
    }

#endif

}
