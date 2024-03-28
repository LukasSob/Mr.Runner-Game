using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalObjectScript : MonoBehaviour
{

    Vector3 startPos;
    public bool goalCollected;

    // Start is called before the first frame update
    void Start()
    {
        startPos = this.gameObject.transform.position;
        goalCollected = false;
    }

    // Update is called once per frame
    void Update()
    {

        gameObject.transform.Rotate(startPos, .1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            goalCollected = true;
        }
    }

    public bool Collected()
    {
        return goalCollected;
    }
}
