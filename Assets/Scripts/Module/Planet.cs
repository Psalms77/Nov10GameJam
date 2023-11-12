using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : Observer
{
    public GameObject player;
    public Rigidbody2D playerRb;
    public float gravityScale;
    private bool gravityOn = true;
    public float gravityRadius;

    public Collider2D[] onPlanetStuff;

    private void Awake()
    {
        AddEventListener(EventName.SwitchGameMode, (object[] arg) =>
        {
            
            TurnOffGravity();
        });
    }



    // Start is called before the first frame update
    void Start()
    {
        playerRb = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }   

    private void FixedUpdate()
    {
        if (gravityOn) { PerformGravity(); }
        else if(!gravityOn)
        {
            TurnOffGravity();
        }

    }

    public void PerformGravity()
    {        
        


        onPlanetStuff = Physics2D.OverlapCircleAll(transform.position, gravityRadius);
        for (int i = 0; i < onPlanetStuff.Length; i++)
        {
            if(onPlanetStuff[i].gameObject.GetComponent<Rigidbody2D>() && (onPlanetStuff[i].gameObject.CompareTag("Player") || onPlanetStuff[i].gameObject.CompareTag("Enemy")))
            {
                //Debug.Log("aaaaa");
                Vector3 gravity = -onPlanetStuff[i].transform.position +this.gameObject.transform.position;
                onPlanetStuff[i].transform.gameObject.GetComponent<Rigidbody2D>().AddForce(gravity.normalized * gravityScale);
            }
            
        }

        //Vector3 t = - player.transform.position + this.gameObject.transform.position;
        //playerRb.AddForce(t.normalized * gravityScale);
    }


    void TurnOffGravity()
    {

    }
    

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, gravityRadius);
    }

#endif


}
