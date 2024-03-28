using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    public int explosionRadius = 5;
    public int explosionForce = 500;

    public Rigidbody rb;

    AudioSource audioSource;

    public GameObject explosionParticles;


    // Start is called before the first frame update
    void Start()
    {
        audioSource= GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            Explode();

        }
    }

    public void Explode()
    {
        var surroundingObjects = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (var obj in surroundingObjects)
        {
            var rb = obj.GetComponent<Rigidbody>();
            if (rb == null) continue;

            rb.AddExplosionForce(explosionForce, this.transform.position, 10);


        }

        audioSource.Play();
        Instantiate(explosionParticles, transform.position, Quaternion.identity);
        gameObject.GetComponent<MeshRenderer>().enabled = false;

        Invoke("ObjectDestroy", 1);
    }
    void ObjectDestroy()
    {
        Destroy(gameObject);
    }
}


