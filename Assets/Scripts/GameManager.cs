using DG.Tweening.Core.Easing;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    private GameManagerFSM statemachine;
    public Camera cam;
    // mouse pointing
    public Vector2 mousePos;
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        //DontDestroyOnLoad(GameObject.Find("Canvas"));
        //DontDestroyOnLoad(GameObject.Find("Camera"));
        Application.targetFrameRate = 120;
        cam = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log("aaa");
    }

    public GameObject GetPlayer()
    {
        return GameObject.FindGameObjectWithTag("Player");
    }





}
