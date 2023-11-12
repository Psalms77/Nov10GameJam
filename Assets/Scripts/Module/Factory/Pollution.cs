using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pollution : Observer
{



    public float detectRange = 7f;
    Collider2D[] cols;
    GameObject target;
    bool hasFindTarget = false;
    
    // Start is called before the first frame update
    void Start()
    {
        hasFindTarget = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasFindTarget) {        



            cols = Physics2D.OverlapCircleAll(transform.position, detectRange);
            for (int i = 0; i < cols.Length; i++)
            {
                if (cols[i].CompareTag("Enemy"))
                {
                    target = cols[i].transform.gameObject;
                    hasFindTarget = true;
                    break;
                }
            } 
        }

        if (hasFindTarget && target != null) {
            transform.DOMove(target.transform.position, 2f).SetEase(Ease.OutCubic);
        }




    }

    private void AntiGravity()
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
