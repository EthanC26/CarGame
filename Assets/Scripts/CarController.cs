using UnityEngine;

public class CarController : MonoBehaviour
{
    public float moterForce = 100f;
    public float brakeForce = 1000f;
    public float maxSteerAngle = 30f;
    public float steerSpeed = 5f;

    public WheelCollider frontLeftWheel, frontRightWheel;
    public WheelCollider rearLeftWheel, rearRightWheel;

    public Transform frontLeftTransform, frontRightTransform;
    public Transform rearLeftTransform, rearRightTransform;

    private float horizontalInput;
    private float verticalInput;
    private float currentSteerAngle;
    private float currentBrakeForce;
    private float maxSpeed = 50f;

    private bool isBraking;

    private Rigidbody rb;
    private BoxCollider bc;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.centerOfMass = new Vector3(0f, -0.5f, 0f);
    }

    private void Update()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        isBraking = Input.GetKey(KeyCode.Space);
    }

    private void HandleMotor()
    {
        frontLeftWheel.motorTorque = verticalInput * moterForce;
        frontRightWheel.motorTorque = verticalInput * moterForce;

        currentBrakeForce = isBraking ? brakeForce : 0f;
        ApplyBraking();
    }
    
    private void ApplyBraking()
    {
        frontLeftWheel.brakeTorque = currentBrakeForce;
        frontRightWheel.brakeTorque = currentBrakeForce;
        rearLeftWheel.brakeTorque = currentBrakeForce;
        rearRightWheel.brakeTorque = currentBrakeForce;
    }

    private void HandleSteering()
    {
        float speed = rb.linearVelocity.magnitude * 3.6f; // Convert to km/h

        //reduce steering angle at higher speeds
        float speedFactor = Mathf.Clamp01(speed / maxSpeed);
        float adjustedMaxSteerAngle = Mathf.Lerp(maxSteerAngle, maxSteerAngle * 0.3f, speedFactor);

        float targetSteerAngle = horizontalInput * adjustedMaxSteerAngle;
        currentSteerAngle = Mathf.Lerp(currentSteerAngle, targetSteerAngle, steerSpeed * Time.deltaTime);

        frontLeftWheel.steerAngle = currentSteerAngle;
        frontRightWheel.steerAngle = currentSteerAngle;
    }

    private  void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.position = pos;
        wheelTransform.rotation = rot;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheel, frontLeftTransform);
        UpdateSingleWheel(frontRightWheel, frontRightTransform);
        UpdateSingleWheel(rearLeftWheel, rearLeftTransform);
        UpdateSingleWheel(rearRightWheel, rearRightTransform);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FinshLine"))
        {
            GameManager.Instance.AddScore(1);
        }
    }
}
