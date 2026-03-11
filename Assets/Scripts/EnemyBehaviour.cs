using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public float moveSpeed = 3f;

    private Transform player;
    private Rigidbody2D rb;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector2 direction = ((Vector2)player.position - rb.position).normalized;
        rb.velocity = direction * moveSpeed;
    }
}