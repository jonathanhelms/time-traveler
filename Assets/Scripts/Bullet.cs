using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed = 20f;
    public Rigidbody2D rb;
    public int attackDamage = 40;
    public float direction;
    public float timer;

    // Start is called before the first frame update
    void Start()
    {
        direction = Input.GetAxis("Horizontal");
        if (direction == 1)
        {
            rb.velocity = direction * (transform.right * speed);
        } else
        {
            rb.velocity = -1 * (transform.right * speed);
        }
        
        
    }

    void Update()
    {
        timer += 1f * Time.deltaTime;

        if(timer >= 4)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        //Debug.Log(hitInfo.name);
        Destroy(gameObject);

        if (hitInfo.name.Contains("Friar"))
        {
            hitInfo.GetComponent<EnemyChase>().TakeDamage(attackDamage);
        }
    
        
    }

}
