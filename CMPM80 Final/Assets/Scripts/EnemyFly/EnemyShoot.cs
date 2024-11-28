using System.Runtime.InteropServices.WindowsRuntime;
//using UnityEditor.Tilemaps;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public float detectionRange = 10f;
    public float fireRate = 1f;
    private float nextFireTime = 0f;
    private bool flipOnShoot = false;

    public EnemyFly enemyFly;
    public Animator anim;
    [SerializeField] private Transform poisonSpawnPointL;
    [SerializeField] private Transform poisonSpawnPointR;
    [SerializeField] private GameObject poisonPrefab;

    private void Start()
    {
        anim = GetComponent<Animator>();
        enemyFly = GetComponent<EnemyFly>();
        anim.Play("FlyingEnemyMove");
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        DetectAndShootPlayer();
    }

    private void DetectAndShootPlayer()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectionRange);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("PlayerHitBox") && Time.time > nextFireTime)
            {
                anim.Play("FlyingShot");
                Invoke("returnToMoveAnim", 0.3f);
                Vector3 direction = (hit.transform.position - transform.position).normalized;
                FacePlayer(direction);
                Shoot(direction);
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
        if (flipOnShoot)
        {
            Invoke("Flip", 0.5f);
            flipOnShoot = false;
        }
    }

    private void FacePlayer(Vector3 direction)
    {
        if (direction.x > 0 && !enemyFly.isFacingRightF || direction.x < 0 && enemyFly.isFacingRightF)
        {
            Flip();
            flipOnShoot = true;
        }
    }

    private void Shoot(Vector3 direction)
    {
        //Debug.Log("Shoot");
        GameObject poison;
        if (enemyFly.isFacingRightF)
        {
            poison = Instantiate(poisonPrefab, poisonSpawnPointR.position, poisonSpawnPointR.rotation);
        }
        else
        {
            poison = Instantiate(poisonPrefab, poisonSpawnPointL.position, poisonSpawnPointL.rotation);
            Vector3 localScale = poison.transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
        
        //set speed and direction of the poison, BUT ADD IT TO THE ENEMY CURRENT SPEED
        float currentEnemySpeed = enemyFly.rbf.linearVelocity.x;
        Rigidbody2D rb = poison.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = direction * (poison.GetComponent<WitchPotion>().speed + Mathf.Abs(currentEnemySpeed));
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }

    public void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
        enemyFly.isFacingRightF = !enemyFly.isFacingRightF;
    }

    private void returnToMoveAnim()
    {
        anim.Play("FlyingEnemyMove");
    }

}
