using UnityEngine;

public class GrapplingGun : MonoBehaviour {

    private LineRenderer lr;
    private Vector3 grapplePoint;
    public LayerMask whatIsGrappleable;
    public Transform gunTip, camera, player;
    bool grappling = false;
    public GameObject canGrapple;
    public GameObject playerObj;
    Rigidbody playerRB;
    private float maxDistance = 20f;
    float grapplePull = 0;
    private SpringJoint joint;

    void Awake() {
        lr = GetComponent<LineRenderer>();
        playerRB = playerObj.GetComponent<Rigidbody>();
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            grappling = true;
            StartGrapple();
            
        }
        else if (Input.GetMouseButtonUp(0)) {
            grappling = false;
            StopGrapple();
        }


        //Grappling Indicator 
        CanGrappleHit();


    }

    //Called after Update
    void LateUpdate() {
        
        DrawRope();
    }

    /// <summary>
    /// Call whenever we want to start a grapple
    /// </summary>
    void StartGrapple() {
        RaycastHit hit;
        if (Physics.Raycast(camera.position, camera.forward, out hit, maxDistance, whatIsGrappleable)) {
            grapplePoint = hit.point;

            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

            //The distance grapple will try to keep from grapple point. 
            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.05f;

            //Adjust these values to fit your game.
            joint.spring = 7f;
            joint.damper = 7f;
            joint.massScale = 4.5f;

            

            lr.positionCount = 2;
            currentGrapplePosition = gunTip.position;

        }
    }

    void CanGrappleHit()
    {
        RaycastHit hitGrappleable;
        if (Physics.Raycast(camera.position, camera.forward, out hitGrappleable, maxDistance, whatIsGrappleable))
        {
            canGrapple.SetActive(true);
            if (grappling == true && Input.GetMouseButton(1))
            {

                playerRB.AddForce((grapplePoint - playerObj.transform.position));
                Debug.Log("Pulling Grapple");

            }
        }
        else
        {
            canGrapple.SetActive(false);
        }
    }


    /// <summary>
    /// Call whenever we want to stop a grapple
    /// </summary>
    void StopGrapple() {
        lr.positionCount = 0;
        Destroy(joint);
    }

    private Vector3 currentGrapplePosition;
    
    void DrawRope() {
        //If not grappling, don't draw rope
        if (!joint) return;

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * 12f);
        
        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, currentGrapplePosition);
    }

    public bool IsGrappling() {
        return joint != null;
    }

    public Vector3 GetGrapplePoint() {
        return grapplePoint;
    }
}
