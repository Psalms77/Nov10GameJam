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
    public Vector2 mousePos;
    public Vector2 gravity;
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
        gravity = -planet.transform.position + objectPosition;
        return gravity;
    }
    // call every frame
    public void SetFacingOnPlant(GameObject go)
    {
        go.transform.up = gravity;
    }

    public void ZoomMap()
    {


    }

}
