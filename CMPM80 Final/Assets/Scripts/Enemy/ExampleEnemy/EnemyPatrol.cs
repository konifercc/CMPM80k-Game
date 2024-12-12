using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    public Rigidbody2D rb;
    private Animator anim;
    private Transform currentPoint;
    public EnemyFollow enemyFollow;
    public KnockBack knockBack;
    public float speed;
    public bool isKnockedBack;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentPoint = pointB.transform;   
        anim.SetBool("isRunning", true);
        enemyFollow = GetComponent<EnemyFollow>();
        knockBack = GetComponent<KnockBack>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!enemyFollow.followMode && !isKnockedBack)
        {

            Vector2 point = currentPoint.position - transform.position;
            if (currentPoint == pointB.transform && !isKnockedBack)
            {
                rb.linearVelocity = new Vector2(speed, 0);
            }
            else if (!isKnockedBack)
            {
                rb.linearVelocity = new Vector2(-speed, 0);
            }

            if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform)
            {
                Flip();
                currentPoint = pointA.transform;
            }
            if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform)
            {
                Flip();
                currentPoint = pointB.transform;
            }
        }


    }

    private void Flip(){
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;

    }

    private void OnDrawGizmosSelected(){
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position); 
    }
}
