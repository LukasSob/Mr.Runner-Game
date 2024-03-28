using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class EnemyController : MonoBehaviour
{
    // References
    public Transform player;
    EnemyGunSystem enemyGun;
    public float rotationSpeed = 10f;
    public float playerShootDistance;
    bool dead = false;

    AudioSource enemyHitSound;

    // Start is called before the first frame update
    void Start()
    {
        // player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyGun = GetComponentInChildren<EnemyGunSystem>();
        SetRigidBodyState(true);
        SetColliderState(false);

        enemyHitSound= GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            StartRotating();

            // Check if the player is in the line of sight
            if (IsPlayerInLineOfSight())
            {
                // Player is in line of sight, initiate shooting logic
                enemyGun.ShootController();
            }
        }
    }

    private Coroutine LookCoroutine;

    public void StartRotating()
    {
        if (LookCoroutine != null)
        {
            StopCoroutine(LookCoroutine);
        }

        LookCoroutine = StartCoroutine(LookAt());
    }

    private IEnumerator LookAt()
    {
        Vector3 playerPosition = new Vector3(player.position.x, transform.position.y, player.position.z);
        Quaternion lookRotation = Quaternion.LookRotation(playerPosition - transform.position);

        // Restrict rotation to only the y-axis (yaw)
        lookRotation = Quaternion.Euler(0f, lookRotation.eulerAngles.y, 0f);

        while (transform.rotation != lookRotation)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
            yield return null;
        }
    }

    public void Die()
    {
        GetComponent<RigBuilder>().enabled = false;
        GetComponent<Animator>().enabled = false;
        SetRigidBodyState(false);
        SetColliderState(true);
        dead = true;

        enemyHitSound.Play();
    }

    void SetRigidBodyState(bool state)
    {
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = state;
            rigidbody.AddForce(transform.up * 500);
        }

        GetComponent<Rigidbody>().isKinematic = !state;
    }

    void SetColliderState(bool state)
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();

        foreach (Collider collider in colliders)
        {
            collider.enabled = state;
        }

        GetComponent<Collider>().enabled = !state;
    }

    bool IsPlayerInLineOfSight()
    {
        RaycastHit hit;
        Vector3 direction = player.position - transform.position;

        if (Physics.Raycast(transform.position, direction, out hit, playerShootDistance))
        {
            // Assuming you have a tag "Player" for your player object
            if (hit.collider.CompareTag("Player"))
            {
                return true;
            }
        }

        return false;
    }
}
