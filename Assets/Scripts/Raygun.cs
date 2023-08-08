using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raygun : MonoBehaviour
{
    public Transform firePointLeft;
    public Transform firePointRight;
    public GameObject bulletPrefab;
    public Animator animator;
    float direction;

    int ammo = 10;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {

        direction = Input.GetAxis("Horizontal");
        // shooting logic
        if(ammo > 0)
        {
            if (animator.GetBool("HasGun") && direction == 1)
            {
                Instantiate(bulletPrefab, firePointRight.position, firePointRight.rotation);

            }
            else if (animator.GetBool("HasGun"))
            {
                Instantiate(bulletPrefab, firePointLeft.position, firePointLeft.rotation);
            }
            ammo--;
        }
        

    }
}
