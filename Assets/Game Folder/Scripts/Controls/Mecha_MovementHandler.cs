using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mecha_MovementHandler : MonoBehaviour {

    // This object's rigid body component
    private Rigidbody rb;

    // Is the mech currently in the middle of a jump.
    private bool bMidJump = false;

    // Starting position of our current jump.
    private Vector3 jumpStartPosition;

    // The current percentage of the way through the jump.
    private float currDist = 0.0f;

    // The name of this weapon.
    [Tooltip("The maximum height of the mecha's jump.")]
    [SerializeField]
    private float altitudeLock = 10.0f;

    // The name of this weapon.
    [Tooltip("The max speed of the mecha's jump.")]
    [SerializeField]
    private float maxTravelSpeed = 5.0f;

    // The name of this weapon.
    [Tooltip("The The acceleration of the mecha's jump.")]
    [SerializeField]
    private float travelAccel = 0.8f;

    private float travelSpeed = 0.0f;

    // The distance from the target that we stop boosting.
    [Tooltip("The distance from the target that we stop boosting.")]
    [SerializeField]
    public float StopThreshold = 2.5f;

    // Reference to the jump-smonke particles.
    [Tooltip("Reference to the jump-smonke particles.")]
    [SerializeField]
    private ParticleSystem jumpParticles;

    // Reference to the jump-smonke particles.
    [Tooltip("Reference to the prefab containing launch particles.")]
    [SerializeField]
    private GameObject launchParticlePrefab;

    // Reference to the jump-smonke particles.
    [Tooltip("Reference to the prefab containing launch particles.")]
    [SerializeField]
    private float mechaGravity = 0.2f;

    // Reference to the jump-smonke particles.
    [Tooltip("Reference to the prefab containing launch particles.")]
    [SerializeField]
    private float mechaGroundClearance = 3.0f;

    public LayerMask floorMask;

    // Use this for initialization
    void Start () {
        // Bind Events
        EventManager.instance.OnMechaJumpStart.AddListener(() =>
        {
            StartJump();
        });

        rb = this.GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        JumpUpdate();
    }

    // Called when the rocket jump begins
    private void StartJump()
    {
        bMidJump = true;
        currDist = 0.0f;
        travelSpeed = 0.0f;
        jumpStartPosition = this.transform.position;

        jumpParticles.Play(true);
        Instantiate(launchParticlePrefab, this.transform);
        //this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }

    // Called during Fixed Update
    private void JumpUpdate()
    {
        if(bMidJump)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
			Vector3 Target = GameObject.FindGameObjectWithTag("JumpReticule").transform.position;

            float MaxDist = Vector3.Distance(new Vector3(jumpStartPosition.x, 0, jumpStartPosition.z), new Vector3(Target.x, 0, Target.z));
            if (MaxDist - currDist < StopThreshold)
            {
                bMidJump = false;
                EventManager.instance.OnMechaJumpEnd.Invoke();
            }
            else
            {
                travelSpeed = Mathf.Lerp(travelSpeed, maxTravelSpeed, travelAccel * Time.deltaTime);

                currDist += travelSpeed * Time.deltaTime;
                Vector3 NewPos = Vector3.Lerp(jumpStartPosition, Target, currDist / MaxDist);

                float Height = Mathf.Lerp(jumpStartPosition.y, jumpStartPosition.y + altitudeLock, Mathf.Sin((currDist / MaxDist) * Mathf.PI));
                NewPos.y = Height;

                this.transform.position = NewPos;
            }
        }
        else
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, -transform.up, out hit))
            {
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - (mechaGravity * Time.deltaTime), this.transform.position.z);
            }

            rb.constraints = RigidbodyConstraints.FreezeAll;
            jumpParticles.Stop(true);
        }
    }

    public bool isJumping()
    {
        return bMidJump;
    }
}
