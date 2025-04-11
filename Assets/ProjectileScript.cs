using System.Collections;
using UnityEditor;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    [Header("Bullet Components")]
    public Rigidbody2D RB;
    public float bulletSpeed = 5f;
    public float lifeTime = 6f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(DestroyAfterTime(lifeTime));
    }

    private IEnumerator DestroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        RB.linearVelocity = transform.right * bulletSpeed;
    }

    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     EnemyScript enemy = other.gameObject.GetComponent<EnemyScript>();
    //     if (enemy != null)
    //     {
    //         enemy.GetShot();
    //     }

    //     //Bullet hit other gameobjects in the scene that isn't the enemy
    //     if (!other.gameObject.CompareTag("Player")) 
    //     {
    //         Destroy(gameObject);
    //     }
    // }
}