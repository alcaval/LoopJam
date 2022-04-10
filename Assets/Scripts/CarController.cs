using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private float accelerationFactor = 30f;
    [SerializeField] private float turnFactor = 3.5f;
    [SerializeField] private float maxSpeed = 20f;

    private float accelerationInput = 0;
    private float steeringInput = 0;
    private float rotationAngle = 0;
    private float velocityUp = 0;
    [SerializeField] float driftFactor = 0.95f;

    private Rigidbody2D carRB;

    private void Awake() 
    {
        carRB = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }

    private void FixedUpdate() 
    {
        ApplyEngineForce();
        KillOrthogonalVelocity();
        ApplySteering();
    }

    private void GetInput()
    {
        steeringInput = Input.GetAxis("Horizontal");
        accelerationInput = Input.GetAxis("Vertical");
    }

    private void ApplyEngineForce()
    {
        velocityUp = Vector2.Dot(transform.up, carRB.velocity);

        if(velocityUp > maxSpeed && accelerationInput > 0) return;
        if(velocityUp < -maxSpeed && accelerationInput < 0) return;
        if(carRB.velocity.sqrMagnitude > maxSpeed * maxSpeed && accelerationInput > 0) return;

        if(accelerationInput == 0)
        {
            carRB.drag = Mathf.Lerp(carRB.drag, 3f, Time.fixedDeltaTime * 3);
        }
        else
        {
            carRB.drag = 0;
        }

        Vector2 engineForce = transform.up * accelerationInput * accelerationFactor;

        carRB.AddForce(engineForce, ForceMode2D.Force);
    }

    private void ApplySteering()
    {
        float minSpeed = (carRB.velocity.magnitude / 8);
        minSpeed = Mathf.Clamp01(minSpeed);

        rotationAngle -= steeringInput * turnFactor * minSpeed;

        carRB.MoveRotation(rotationAngle);
    }

    private void KillOrthogonalVelocity()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(carRB.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(carRB.velocity, transform.right);

        carRB.velocity = forwardVelocity + rightVelocity * driftFactor;
    }

    private float GetLateralVelocity() { return Vector2.Dot(transform.right, carRB.velocity); }

    public bool isCarDrifting(out float latVelocity, out bool isDrifting)
    {
        latVelocity = GetLateralVelocity();
        isDrifting = false;

        if(accelerationInput < 0 && velocityUp > 0)
        {
            isDrifting = true;
            return true;
        }

        if(Mathf.Abs(GetLateralVelocity()) > 3f)
            return true;
        
        return false;
    }
}
