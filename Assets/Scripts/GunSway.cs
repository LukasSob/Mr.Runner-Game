using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GunSway : MonoBehaviour
{


    [Header("Sway Settings")]
    [SerializeField] private float smooth;
    [SerializeField] private float multiplier;

    [SerializeField] private float gunXMoveMultiplier;
    [SerializeField] private float gunZMoveMultiplier;

    [SerializeField] private float recoil;

    Vector3 gunStartPos;

    Vector3 gunMoveLeft;
    Vector3 gunMoveRight;

    Vector3 gunMoveUp;
    Vector3 gunMoveDown;

    Vector3 gunMoveRecoil;

    private void Start()
    {
        gunStartPos = transform.localPosition;

        gunMoveLeft = new Vector3(transform.localPosition.x + gunXMoveMultiplier, transform.localPosition.y, transform.localPosition.z);
        gunMoveRight = new Vector3(transform.localPosition.x - gunXMoveMultiplier, transform.localPosition.y, transform.localPosition.z);

        gunMoveUp = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z - gunZMoveMultiplier);
        gunMoveDown = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + gunZMoveMultiplier);
        gunMoveRecoil = new Vector3(transform.localPosition.x + recoil, transform.localPosition.y, transform.localPosition.z); ;

    }

    private void Update()
    {
        // get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * multiplier;
        float mouseY = Input.GetAxisRaw("Mouse Y") * multiplier;

        // calculate target rotation
        Quaternion rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);

        Quaternion targetRotation = rotationX * rotationY;

        

        // rotate 
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);


            if (Input.GetKey(KeyCode.A) == true)
        {
            transform.localPosition = Vector3.Slerp(transform.localPosition, gunMoveLeft, smooth * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D) == true)
        {
            transform.localPosition = Vector3.Slerp(transform.localPosition, gunMoveRight, smooth * Time.deltaTime);
        }
        else
        {
            transform.localPosition = Vector3.Slerp(transform.localPosition, gunStartPos, smooth * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.W) == true)
        {
            transform.localPosition = Vector3.Slerp(transform.localPosition, gunMoveUp, smooth * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.S) == true)
        {
            transform.localPosition = Vector3.Slerp(transform.localPosition, gunMoveDown, smooth * Time.deltaTime);
        }
        else
        {
            transform.localPosition = Vector3.Slerp(transform.localPosition, gunStartPos, smooth * Time.deltaTime);
        }

    }


    public void ShootAnim()
    {
        Quaternion spin = Quaternion.FromToRotation(transform.position, new Vector3(transform.position.x, transform.position.y + 100, transform.position.z));

        transform.localRotation = Quaternion.Slerp(transform.localRotation, spin, smooth * Time.deltaTime);
        transform.localPosition = Vector3.Slerp(transform.localPosition, gunMoveRecoil, smooth * Time.deltaTime);
    }
}
