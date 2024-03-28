using UnityEngine;
using TMPro;
using System.Runtime.CompilerServices;
using EZCameraShake;
using DG.Tweening;

public class EnemyGunSystem : MonoBehaviour
{
    //Gun stats
    public int damage;
    public float timeBetweenShooting, spread, range;
    public int bulletsPerTap;
    public int bulletSpeed;

    Rigidbody playerRB;
    public float shotForce;
    public GameObject player;

    //bools 
    bool shooting = true;
    bool readyToShoot;

    //Reference
    public Camera fpsCam;
    public Transform attackPoint;
    public GunSway gunSway;

    //Graphics
    public GameObject muzzleFlash, bulletGO;
    private GameObject muzzleFlashInstatieated;
    public float camShakeMagnitude, camShakeDuration;
    public TextMeshProUGUI text;

    //Audio
    public AudioSource gunShot;

    private void Awake()
    {
        gunSway = this.GetComponent<GunSway>();
        playerRB = player.GetComponent<Rigidbody>();
        readyToShoot = true;

    }

    public void ShootController()
    {

        //Shoot
        if (readyToShoot && shooting)
        {
            for (int i = 0; i < bulletsPerTap; i++)
            {
                Shoot();
            }
            muzzleFlashInstatieated = Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);
        }
    }
    private void Shoot()
    {
        readyToShoot = false;

        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);
        Vector3 Direction = attackPoint.forward + new Vector3(x, y, 0);

        var bullet = Instantiate(bulletGO, attackPoint.position, Quaternion.identity);

        bullet.GetComponent<Rigidbody>().velocity = Direction * bulletSpeed;
        Invoke("ResetShot", timeBetweenShooting);
    }

    private void ResetShot()
    {
        readyToShoot = true;
        Destroy(muzzleFlashInstatieated);
    }
}

