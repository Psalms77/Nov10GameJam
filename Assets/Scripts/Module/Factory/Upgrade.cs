using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : Observer
{

    public float detectRange = 5f;
    Collider2D[] cols;
    GameObject target;
    bool hasFindTarget = false;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        hasFindTarget = false;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasFindTarget)
        {
            cols = Physics2D.OverlapCircleAll(transform.position, detectRange);
            for (int i = 0; i < cols.Length; i++)
            {
                if (cols[i].CompareTag("Player"))
                {
                    target = cols[i].transform.gameObject;
                    hasFindTarget = true;
                    break;
                }
            }
        }

        if (hasFindTarget && target != null)
        {
            transform.DOMove(target.transform.position, 2f).SetEase(Ease.OutCubic);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            EventManager.SendNotification(EventName.PlayerTakeUpgrade, 1f);
            DOTween.Kill(this.transform);
            Destroy(gameObject);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }

#endif
}
