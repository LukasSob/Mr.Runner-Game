using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;

    public bool holdingWeapon;
    public int pickupDistance;
    public GameObject shotGunWeapon;
    public GameObject shotGunPickup;
    public LayerMask weaponPickup;

    public float maxWeaponThrowForce;
    public float throwForceSpeed;
    float weaponThrowForce;

    public Camera cam;
    Vector3 direction;
    RaycastHit hit;

    bool isDead;


    // Start is called before the first frame update
    void Start()
    {
        shotGunPickup.transform.position = shotGunWeapon.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        direction = cam.transform.forward;

        if (Input.GetKeyDown(KeyCode.F))
        {
            
            if (Physics.Raycast(cam.transform.position, direction, out hit, pickupDistance))
            {
                if (hit.collider.tag == "WeaponPickup") 
                {
                    Debug.Log("Weapon Picked Up");
                    PickUpItem();
                    Destroy(hit.transform.gameObject);
                }
            }

        }
        if (Input.GetKey(KeyCode.Q) && holdingWeapon)
        {
            if (weaponThrowForce < maxWeaponThrowForce) 
            {
                weaponThrowForce = weaponThrowForce + throwForceSpeed;
            }
            else
            {
                weaponThrowForce= maxWeaponThrowForce;
            }
            Debug.Log("Throw Force: " + weaponThrowForce);

        }
        if (Input.GetKeyUp(KeyCode.Q) && holdingWeapon)
        {
            DropItem();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerKill")
        {
            isDead = true;
            Debug.Log(isDead);
        }

        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            isDead = true;
            Debug.Log(isDead);
        }
    }

    private void DropItem()
    {
        shotGunPickup.transform.position = shotGunWeapon.transform.position;

        holdingWeapon = false;
        shotGunWeapon.gameObject.SetActive(false);
        GameObject spawnedWeapon = Instantiate(shotGunPickup);
        spawnedWeapon.GetComponent<Rigidbody>().AddForce(direction * weaponThrowForce);
        weaponThrowForce= 0; 
    }

    private void PickUpItem()
    {
        
        holdingWeapon = true;
        shotGunWeapon.gameObject.SetActive(true);
    }

    public bool isPlayerDead()
    {
        if (isDead == true)
        {
            return true;
        }
        return false;
    }

}
