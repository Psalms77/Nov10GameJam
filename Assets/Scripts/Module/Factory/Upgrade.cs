using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : Observer
{

    public float detectRange = 7f;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }



    private void OnTriggerEnter2D(Collider2D collision)
    {

    }



#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }

#endif
}
