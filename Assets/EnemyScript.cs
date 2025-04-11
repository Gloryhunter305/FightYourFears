using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    [SerializeField] private PlayerMovement player;
    private Rigidbody2D RB;
    [SerializeField] private Transform target;
    public float speed = 20f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindAnyObjectByType<PlayerMovement>();
        RB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.State == PlayerMovement.PlayerState.Jump)
        {
            Vector2 currentPosition = RB.position;
            Vector2 targetPosition = target.position;
            Vector2 newPosition = Vector2.MoveTowards(currentPosition, targetPosition, speed * Time.deltaTime);
            RB.MovePosition(newPosition);
        }
    }

    public void GetShot()
    {
        Debug.Log("Randomize position");
        float randomX = Random.Range(-8.75f, 8.75f);
        float randomY = Random.Range(5f, -5f);

        transform.position = new Vector2(randomX, randomY);
    }
}
