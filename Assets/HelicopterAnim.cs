using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterAnim : MonoBehaviour
{
    public bool spinX;
    public bool spinY;
    public bool spinZ;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (spinX)
        {
            this.gameObject.transform.Rotate(1 * 10, 0, 0);
        }
        else if (spinY)
        {
            this.gameObject.transform.Rotate(0, 1 * 10, 0);
        }
        else if (spinZ)
        {
            this.gameObject.transform.Rotate(0, 0, 1 * 10);
        }
        
    }
}
