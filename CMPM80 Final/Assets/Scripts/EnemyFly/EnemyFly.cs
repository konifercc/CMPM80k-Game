using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyFly : MonoBehaviour
{
    [SerializeField] public GameObject pointA;
    [SerializeField] public GameObject pointB;
    [SerializeField] public float speed;
    public Rigidbody2D rbf;
    private Animator anim;
    private Transform currentPoint;
    public KnockBackF knockBackF;
    public bool isKnockedBackF;
    public bool isFacingRightF;
    public bool isShooting; //if we want enemy still when shooting
    public bool isPathingToY; //return to y position after knockback
    private float standardY; //y position to return to after knockback
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        standardY = transform.position.y;
        //canMove = true;
        isKnockedBackF = false;
        rbf = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
        //knockBackF = GetComponent<KnockBackF>();
        currentPoint = pointB.transform;
        isFacingRightF = true;
        rbf.gravityScale = 0;
        rbf.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
        //DONT LOCK THE Y LOC OF RB SO KNOCKBACK CAN WORK
        //pointA = GameObject.Find("PointA").GetComponent<Transform>();
        //pointB = GameObject.Find("PointB").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isKnockedBackF)
        {
            normalPathing();
        }
    }

    public void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
        isFacingRightF = !isFacingRightF;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }

    public void setCurrentPointF(GameObject point) //WILL BE USED IN KNOCKBACK SCRIPT
    {
        currentPoint = point.transform;
    }

    public void DisableMovement(float duration)
    {
        //canMove = false;
        rbf.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
        isKnockedBackF = true;
        Invoke("EnableMovement", duration);
    }

    // Method to enable movement after a specified time
    public void EnableMovement()
    {
        //rbf.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        //canMove = true;
        isKnockedBackF = false;
        if (rbf != null)
        {
            rbf.linearVelocity = Vector2.zero; // Stop movement after knockback duration
        }
        FindFurthestPoint();
    }


    /*
    public void returnToY()
    {
        float oldspeed = speed;
        float verticalSpeed = speed;
        if (currentPoint == pointA.transform)
        {
            //Debug.Log("pointB");
            speed = -speed;
        }

        if (transform.position.y < standardY)
        {
            rbf.linearVelocity = new Vector2(speed, verticalSpeed);
        }
        else if (transform.position.y > standardY)
        {
            rbf.linearVelocity = new Vector2(speed, -verticalSpeed);
        }
        else
        {
            rbf.linearVelocity = new Vector2(0, 0);
            isPathingToY = false;
            speed = oldspeed;
            rbf.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
        }
    }
    */


    public void FindFurthestPoint()
    {
        if (Vector2.Distance(transform.position, pointA.transform.position) > Vector2.Distance(transform.position, pointB.transform.position))
        {
            setCurrentPointF(pointA);
            if (isFacingRightF)
            {
                Flip();
            }
        }
        else
        {
            setCurrentPointF(pointB);
            if (!isFacingRightF)
            {
                Flip();
            }
        }
    }

    public void normalPathing()
    {
        //if statement to set the speed towards the current point
        if (!isKnockedBackF)
        {
            //Vector2 point = currentPoint.position - transform.position;
            if (currentPoint == pointB.transform)
            {
                //Debug.Log("pointB");
                rbf.linearVelocity = new Vector2(speed, 0);
            }
            else
            {
                //Debug.Log("pointA");
                rbf.linearVelocity = new Vector2(-speed, 0);
            }
        }

        //check if enemy has reached the point and flip after reaching pt
        if (Vector2.Distance(transform.position, currentPoint.position) < .5f && currentPoint == pointB.transform)
        {
            //Debug.Log("pointB reached");
            Flip();
            currentPoint = pointA.transform;
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < .5f && currentPoint == pointA.transform)
        {
            //Debug.Log("pointA reached");
            Flip();
            currentPoint = pointB.transform;
        }
    }



}
