using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackDistance = 0.8f;
    [SerializeField] private Vector2 attackBoxSize = new Vector2(1f, 0.5f);
    [SerializeField] private int attackDamage = 1;
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private float attackCooldown = 0.25f;

    [SerializeField] private Transform swordTransform;
    [SerializeField] private SpriteRenderer swordSpriteRenderer;
    [SerializeField] private float swingDuration = 0.12f;
    [SerializeField] private float swingAngle = 90f;

    private float lastAttackTime;
    private PlayerMovement playerMovement;
    private bool isAttacking = false;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();

        if (swordSpriteRenderer != null)
        {
            swordSpriteRenderer.enabled = false;
        }
    }
    void Update()
    {
        if (attackPoint == null || playerMovement == null)
            return;

        // move attack to be in player direction
        Vector2 facing = playerMovement.FacingDirection.normalized;
        attackPoint.localPosition = facing * attackDistance;

        UpdateSwordIdlePosition(facing);
        Debug.Log("Facing: " + playerMovement.FacingDirection);

        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= lastAttackTime + attackCooldown)
        {
            Attack();
            lastAttackTime = Time.time;
            StartCoroutine(PlaySwordSwing());
        }
    }

    void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(attackPoint.position, attackBoxSize, 0f, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(attackDamage);
            }
        }
    }

    void UpdateSwordIdlePosition(Vector2 facing)
    {
        if (swordTransform == null || isAttacking) return;

        swordTransform.localPosition = facing * attackDistance;
        float angle = Mathf.Atan2(facing.y, facing.x) * Mathf.Rad2Deg;
        swordTransform.localRotation = Quaternion.Euler(0f, 0f, angle);
    }

    IEnumerator PlaySwordSwing()
    {
        if (swordTransform != null || swordSpriteRenderer == null)
            yield break;

        isAttacking = true;
        swordSpriteRenderer.enabled = true;

        Vector2 facing = playerMovement.FacingDirection.normalized;
        float baseAngle = Mathf.Atan2(facing.y, facing.x) * Mathf.Rad2Deg;

        float startAngle = baseAngle - swingAngle * 0.5f;
        float endAngle = baseAngle + swingAngle * 0.5f;

        float timer = 0f;

        while (timer < swingDuration)
        {
            float t = timer / swingDuration;
            float currentAngle = Mathf.Lerp(startAngle, endAngle, t);
            swordTransform.localPosition = facing * attackDistance;
            swordTransform.localRotation = Quaternion.Euler(0f, 0f, currentAngle);

            timer += Time.deltaTime;
            yield return null;
        }

        swordTransform.localRotation = Quaternion.Euler(0f, 0f, endAngle);
        yield return new WaitForSeconds(0.02f);
        swordSpriteRenderer.enabled = false;
        isAttacking = false;
    }
    
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPoint.position, attackBoxSize);
    }
}
