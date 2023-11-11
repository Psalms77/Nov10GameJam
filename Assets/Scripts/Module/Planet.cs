using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public GameObject player;
    public Rigidbody2D playerRb;
    public float gravityScale;

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
        Vector3 t = - player.transform.position + this.gameObject.transform.position;
        playerRb.AddForce(t.normalized * gravityScale);
    }
}
