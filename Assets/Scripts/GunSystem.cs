using UnityEngine;
using TMPro;
using System.Runtime.CompilerServices;
using EZCameraShake;
using DG.Tweening;

public class GunSystem : MonoBehaviour
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
    bool shooting, readyToShoot;

    //Reference
    public Camera fpsCam;
    public Transform attackPoint;
    public GunSway gunSway;
    public GameManager gameManager;

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
        player.GetComponent<PlayerManager>().holdingWeapon= true;

    }
    private void Update()
    {
        if (gameManager.shootingAllowed) MyInput();
    }
    private void MyInput()
    {
         shooting = Input.GetMouseButtonDown(0);

        //Shoot
        if (readyToShoot && shooting)
        {
            for (int i = 0; i < bulletsPerTap; i++)
            {
                Shoot();
            }
            PlayerForce();
            var muzzleFlashInstatieated = Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);
            gunSway.ShootAnim();
            gunShot.Play();
            fpsCam.DOShakePosition(camShakeDuration, camShakeMagnitude);
            fpsCam.DOShakeRotation(camShakeDuration, camShakeMagnitude);
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
    private void PlayerForce()
    {
        playerRB.AddForce(-fpsCam.transform.forward * shotForce, ForceMode.Impulse);
    }

    private void ResetShot()
    {
        readyToShoot = true;
        Destroy(muzzleFlashInstatieated);
    }
}

