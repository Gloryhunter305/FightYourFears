using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Scene Components")]
    public Camera mainCam;
    //public Camera playerCam;

    [Header("Gameobject Components")]
    [SerializeField] private Rigidbody2D RB;
    [SerializeField] private SpriteRenderer spriteRenderer;
    public Transform shootPoint;

    [Header("Player Components")]
    public float moveSpeed = 5f;        //Will experiment with it
    public float jumpForce = 5f;
    public ConstantForce2D CF;
    // public Transform shootPoint;
    // private Vector2 movementDirection;
    public float rotationSpeed = 5f; // Adjust for desired rotation speed


    [Header("Spawning Gameobject")]
    public GameObject bulletPrefab;

    // [Header("Player Camera Variables")]
    // public bool lockXAxis = false;
    // public bool lockYAxis = false;
    // public bool lockZAxis = true; // Lock Z-axis by default

    public PlayerState State = PlayerState.None;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        CF = GetComponent<ConstantForce2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetState(PlayerState.Move);
            RB.gravityScale = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetState(PlayerState.Jump);
            RB.gravityScale = 1;
        }

        if (State == PlayerState.Move)
        {
            RotatePlayer();

            if (Input.GetButton("Fire1"))   //Left Click on Mouse
            {
            PlayerMove(); 
            MovePlayerAway();
            }
        }
        else if (State == PlayerState.Jump)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Jump();
            }
        }
        
        

        // if (Input.GetKey(KeyCode.Space) && mainCam.enabled == true)
        // {
        //     playerCam.enabled = true;
        //     mainCam.enabled = false;
        // }
        // if (Input.GetKey(KeyCode.Space) && playerCam.enabled == true)
        // {
        //     playerCam.enabled = false;
        //     mainCam.enabled = true;
        // }
    }

    // void LateUpdate()
    //     {
    //         if (lockXAxis)
    //         {
    //             transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, transform.eulerAngles.z);
    //         }

    //         if (lockYAxis)
    //         {
    //             transform.rotation = Quaternion.Euler(transform.eulerAngles.x, 0, transform.eulerAngles.z);
    //         }

    //         if (lockZAxis)
    //         {
    //             transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0);
    //         }
    //     }

    public void SetState(PlayerState s)
    {
        State = s;
        if (State == PlayerState.None)
        {

        }
        else if (State == PlayerState.Move)
        {
            spriteRenderer.material.color = Color.blue;
        }
        else if (State == PlayerState.Jump)
        {
            spriteRenderer.material.color = Color.red;
        }
        else if (State == PlayerState.Shoot)
        {
            spriteRenderer.material.color = Color.green;
        }
    }

    private void RotatePlayer()
    {
        Vector3 mousePosition = mainCam.ScreenToWorldPoint(Input.mousePosition);    //Will have to change camera to player camera
        Vector3 direction = mousePosition - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void PlayerMove()
    {
        Vector3 pos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(pos.y - transform.position.y, pos.x - transform.position.x) * Mathf.Rad2Deg;
        Instantiate(bulletPrefab, shootPoint.position, Quaternion.Euler(0, 0, angle));
    }

    private void MovePlayerAway()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerPosition = transform.position;
        Vector2 direction = playerPosition - mousePosition;
        float distance = direction.magnitude;

        if (distance < 1f)
        {
            return;
        }
        direction.Normalize();

        Vector2 velocity = direction * moveSpeed;
        float maxSpeed = 5f;
        if(velocity.magnitude > maxSpeed)
        {
            velocity = velocity.normalized * maxSpeed;
        }
        RB.linearVelocity = velocity;
    }

    void Jump()
    {
        RB.linearVelocity = Vector2.up * jumpForce;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Interacted with object in Gamescene.");
        EnemyScript enemy = collision.gameObject.GetComponent<EnemyScript>();

        Debug.Log(enemy);
        if (enemy != null)
        {
            enemy.GetShot();
        }
    }

    public enum PlayerState 
    {
        None = 0,
        Move = 1,
        Jump = 2,
        Shoot = 3
        
    }
}
