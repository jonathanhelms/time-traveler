using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{

    public float speed = 5f;
    private Transform target;
    public Animator animator;
    public Animator player;

    [SerializeReference] Rigidbody2D rb;

    private float radius;

    public int maxHealth = 100;
    int currentHealth;

    Vector2 movement;

    public Transform attackPoint;
    public LayerMask playerLayer;
    public float attackRange = 0.5f;

    public int attackDamage = 50;
    public float attackRate = 2f;
    float nextAttackTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        radius = 1f;
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // Input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        Vector2 towards = target.position - transform.position;

        if (!player.GetBool("HasCloak") && towards.magnitude > radius)
        {  
            towards.Normalize();
            towards *= speed;

            // Move character
            rb.velocity = towards;

            animator.SetFloat("Horizontal", rb.velocity.x);
            animator.SetFloat("Vertical", rb.velocity.y);
            animator.SetFloat("Magnitude", rb.velocity.magnitude);
        }
        else
        {
            rb.velocity = Vector2.zero;
            animator.SetFloat("Horizontal", rb.velocity.x);
            animator.SetFloat("Vertical", rb.velocity.y);
            animator.SetFloat("Magnitude", rb.velocity.magnitude);
        }

      
        if (Time.time >= nextAttackTime)
        {
            if (towards.magnitude <= radius && animator.GetBool("hasSword") && !player.GetBool("isDead") && !player.GetBool("HasCloak"))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }


    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Enemy hit");
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            rb.velocity = Vector2.zero;
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy died");

        animator.SetBool("isDead", true);


        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;

    }

    void Attack()
    {
        animator.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

        foreach (Collider2D player in hitEnemies)
        {

            player.GetComponent<PlayerMovement>().TakeDamage(attackDamage);
        }
    }

    public void RemoveNPC()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Sword"))
        {
            animator.SetBool("hasSword", true);
        }
    }
}
