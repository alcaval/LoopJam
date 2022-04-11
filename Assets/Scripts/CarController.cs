using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarController : MonoBehaviour
{
    [SerializeField] private Slider life;
    [SerializeField] private CMScreenshake cmscreenshake;

    [SerializeField] private float accelerationFactor = 30f;
    [SerializeField] private float turnFactor = 3.5f;
    [SerializeField] private float maxSpeed = 20f;

    private float accelerationInput = 0;
    private float steeringInput = 0;
    private float rotationAngle = 0;
    private float velocityUp = 0;
    private bool backwards = false;

    [SerializeField] float driftFactor = 0.95f;

    private Rigidbody2D carRB;

    private float lifeMultiplier = 1f;
    private float healCooldown = 1f;

    private void Awake() 
    {
        carRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();

        if(lifeMultiplier != 1f) StartCoroutine(HealCoroutine());
    }

    private void FixedUpdate() 
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            takeDamage(0.3f);
        }
        ApplyEngineForce();
        KillOrthogonalVelocity();
        ApplySteering();
    }

    private void GetInput()
    {
        steeringInput = Input.GetAxis("Horizontal");
        accelerationInput = Input.GetAxis("Vertical");
        if(accelerationInput > 0)
        {
            backwards = false;
        }
        else{
            backwards = true;
        }
    }

    private void ApplyEngineForce()
    {
        velocityUp = Vector2.Dot(transform.up, carRB.velocity);

        if(velocityUp > maxSpeed && accelerationInput > 0) return;
        if(velocityUp < -maxSpeed && accelerationInput < 0)
        {

            return;  
        } 
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

        if(accelerationInput < 0 && velocityUp > 0 && !backwards)
        {
            isDrifting = true;
            return true;
        }

        if(Mathf.Abs(GetLateralVelocity()) > 2.5f && !backwards)
            return true;
        
        return false;
    }

    public void takeDamage(float value)
    {
        cmscreenshake.ShakeCamera(8, 1f);
        lifeMultiplier -= value;
        life.value = lifeMultiplier;
    }

    IEnumerator HealCoroutine()
    {   
        if(!isCarDrifting(out float latVelocity, out bool isDrifting))
        {
            lifeMultiplier += 0.0005f;
            life.value += 0.0005f;
            yield return new WaitForSeconds(1f);
        }
    }
}
