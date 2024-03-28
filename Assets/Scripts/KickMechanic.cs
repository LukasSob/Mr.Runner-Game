using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;
using DG.Tweening;

public class KickMechanic : MonoBehaviour
{
    public float kickForce;
    public float kickInterval;
    public float kickDistance;


    public float kickScreenShake;

    bool readyToKick;

    RaycastHit hit;

    AudioSource kickSound;
    public Animator kickAnim;

    //References
    Rigidbody player;
    public Camera cam;

    private void Start()
    {
        player = GetComponent<Rigidbody>();
        kickSound= GetComponent<AudioSource>();
        readyToKick= true;
    }


    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.E) && readyToKick)
        {
            Kick();
        }
    }

    private void Kick()
    {
        readyToKick = false;
        Vector3 direciton = cam.transform.forward;
        kickAnim.SetTrigger("kick");



        if (Physics.Raycast(cam.transform.position, direciton, out hit, kickDistance))
        {
            if (hit.collider.tag == "ExplosiveBarrel")
            {
                hit.collider.gameObject.GetComponent<ExplosionScript>().Explode();
            }
            if (hit.collider.tag == "Enemy")
            {
                hit.collider.gameObject.GetComponent<EnemyController>().Die();
                player.AddForce(-direciton * kickForce);
            }

            player.AddForce(-direciton * kickForce);
            cam.DOShakePosition(.1f, kickScreenShake);
            kickSound.Play();
        }

        Invoke("ResetKick", kickInterval);
    }

    private void ResetKick()
    {
        readyToKick = true;
    }
}
