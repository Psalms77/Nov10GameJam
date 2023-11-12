using DG.Tweening.Core.Easing;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    private GameManagerFSM statemachine;
    public Camera cam;
    public GameObject planet;
    // mouse pointing
    public Vector3 mousePos;
    public Vector2 gravity;

    public AudioClip bgm;
    public AudioSource bgmSource;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        
        //DontDestroyOnLoad(GameObject.Find("Camera"));
        Application.targetFrameRate = 120;
        cam = Camera.main;
        planet = GameObject.Find("Planet");
    }

    // Start is called before the first frame update
    void Start()
    {
        bgmSource = GetComponent<AudioSource>();
        bgmSource.loop = true;
        bgmSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetKeyDown(KeyCode.G))
        {
            EventManager.SendNotification(EventName.SwitchGameMode);
        }

        if (Input.GetKey(KeyCode.M))
        {

        }

    }

    public GameObject GetPlayer()
    {
        return GameObject.FindGameObjectWithTag("Player");
    }

    // call every frame
    public Vector2 GetGravity(Vector3 objectPosition)
    {

        Vector3 grav = -planet.transform.position + objectPosition;
        return grav;
    }
    // call every frame
    public void SetFacingOnPlant(GameObject go)
    {
        go.transform.up = GetGravity(go.transform.position);
    }

    public void ZoomMap()
    {




    }

}
