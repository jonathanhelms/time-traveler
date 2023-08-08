using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    public Animator animator;
    public float speed = 12f;
    Vector2 movement;

    public GameObject[] weapons;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    public int attackDamage = 40;
    public float attackRate = 2f;
    float nextAttackTime = 0f;

    public int maxHealth = 100;
    int currentHealth;

    int capacitors;

    private void Start()
    {
        currentHealth = maxHealth;
        capacitors = 0;
    }


    // Update is called once per frame
    void Update()
    {
        // Input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Magnitude", movement.magnitude);

        if(Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Space) && animator.GetBool("HasSword"))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }

        
    }

    void FixedUpdate()
    {
        // Movement
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    void Attack()
    {
        animator.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
         
            enemy.GetComponent<EnemyChase>().TakeDamage(attackDamage);
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
        LevelManager.instance.GameOver();
        Debug.Log("Player died");

        animator.SetBool("isDead", true);


        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        
        //gameObject.SetActive(false);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name.Contains("Flux"))
        {
            capacitors += 1;
            Destroy(collision.gameObject);
        }

        if (capacitors >= 5)
        {
            Win();
        }
    }

    void Win()
    {
        LevelManager.instance.WinGame();
    }

    void OnDrawGizmosSelected()
    {

        if(attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    
}


