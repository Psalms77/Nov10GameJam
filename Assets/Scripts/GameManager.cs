using DG.Tweening.Core.Easing;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{

    private GameManagerFSM statemachine;
    public Camera cam;
    public GameObject planet;
    // mouse pointing
    public Vector3 mousePos;
    public Vector2 gravity;
    public CinemachineVirtualCamera vcam;
    public AudioClip bgm;
    public AudioSource bgmSource;
    public float zoomSpeed = 2f;
    public float minZoom = 10f;
    public float maxZoom = 80f;
    public int headcount = 0;
    public GameObject tutorialPanel;
    public GameObject winPanel;
    public GameObject losepanel;

    public Text killnum;
    public Text pollutionnum;
    public int pollutioncount = 0;
    protected override void Awake()
    {
        base.Awake();
        //DontDestroyOnLoad(gameObject);
        
        //DontDestroyOnLoad(GameObject.Find("Camera"));
        Application.targetFrameRate = 120;
        cam = Camera.main;
        planet = GameObject.Find("Planet");

        AddEventListener(EventName.EnemyDies, (object[] arg) =>
        {
            headcount += 1;
            Debug.Log(headcount);
        });
        AddEventListener(EventName.PollutionSpawn, (object[] arg) =>
        {
            //pollutioncount += 1;
        });
        AddEventListener(EventName.EnemyTakePollution, (object[] arg) =>
        {
            //pollutioncount = pollutioncount -1;
        });
    }

    // Start is called before the first frame update
    void Start()
    {
        bgmSource = GetComponent<AudioSource>();
        bgmSource.loop = true;
        bgmSource.Play();
        tutorialPanel.SetActive(true);

        Destroy(tutorialPanel, 30f);
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetKeyDown(KeyCode.G))
        {
            EventManager.SendNotification(EventName.SwitchGameMode);
        }


        pollutioncount = GameObject.FindGameObjectsWithTag("Pollution").Length;

        ZoomMap();
        killnum.text = headcount.ToString();
        pollutionnum.text = pollutioncount.ToString();

        if (headcount > 40)
        {
            winPanel.SetActive(true);
        }

        if (pollutioncount > 40)
        {
            losepanel.SetActive(true);
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


    public void HeadCount()
    {
        headcount += 1;
    }

    public void ZoomMap()
    {

        // Get the mouse scroll wheel input
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");

        // Adjust the orthographic size or field of view based on the scroll input
        if (vcam.m_Lens.Orthographic)
        {
            vcam.m_Lens.OrthographicSize = Mathf.Clamp(vcam.m_Lens.OrthographicSize - scrollWheel * zoomSpeed, minZoom, maxZoom);
        }
        else
        {
            vcam.m_Lens.FieldOfView = Mathf.Clamp(vcam.m_Lens.FieldOfView - scrollWheel * zoomSpeed, minZoom, maxZoom);
        }
    

}

}
