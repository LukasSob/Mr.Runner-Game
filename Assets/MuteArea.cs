using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteArea : MonoBehaviour
{
    public AudioSource sourceToMute;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            sourceToMute.Pause();
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.tag == "Player")
        {
            sourceToMute.Play();
        }

    }
}
