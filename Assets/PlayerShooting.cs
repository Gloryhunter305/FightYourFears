using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public float kickbackStrength = 5f;

    public void Shoot()
    {
        Vector2 kickbackDirection = Vector2.left; // Or Vector2.right, depending on your game

        GetComponent<Rigidbody2D>().AddForce(kickbackDirection * kickbackStrength, ForceMode2D.Impulse);
    }
}