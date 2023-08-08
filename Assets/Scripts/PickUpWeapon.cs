using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpWeapon : MonoBehaviour
{
    private bool pickUpAllowed;
    public Animator animator;


    // Update is called once per frame
    private void Update()
    {
        if (pickUpAllowed && Input.GetKeyDown(KeyCode.E))
        {
            PickUp();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            pickUpAllowed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            pickUpAllowed = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            pickUpAllowed = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            pickUpAllowed = false;
        }
    }

    private void PickUp()
    {
        Destroy(gameObject);
        
        if (gameObject.name.Equals("Sword"))
        {
            animator.SetBool("HasSword", true);
            animator.SetBool("HasGun", false);
            animator.SetBool("HasCloak", false);
        } else if (gameObject.name.Equals("Gun"))
        {
            animator.SetBool("HasGun", true);
            animator.SetBool("HasSword", false);
            animator.SetBool("HasCloak", false);
        } else if (gameObject.name.Equals("Invisibility"))
        {
            animator.SetBool("HasCloak", true);
            animator.SetBool("HasSword", false);
            animator.SetBool("HasGun", false);
        }
    }

}
