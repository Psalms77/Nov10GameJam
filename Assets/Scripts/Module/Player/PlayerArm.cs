using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArm : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("a");
        Vector2 mouse = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        if (mouse.x <= 960f) {
            //left

        }
        else if (mouse.x > 960f)
        {
            //right



        }
        //Debug.Log(mouse);
        PointingAim();


    }



    void PointingAim()
    {
        this.gameObject.transform.up = - this.gameObject.transform.position + GameManager.instance.mousePos;

    }




}
