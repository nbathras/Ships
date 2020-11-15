using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {

    public float acceleration;
    public float steering;
    private Rigidbody2D rb;

    public float maximumSpeed;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        // Cannon Firing
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            ShootVolley(-transform.right);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            ShootVolley(transform.right);
        }
    }

    private void ShootVolley(Vector3 direction) {
        int[] cannonBallPositions = new int[] { -2, -1, 0, 1 };
        float cannonBallSeperationDistance = 1.5f;

        for (int i = 0; i < cannonBallPositions.Length; i++) {
            Vector3 verticalOffset = transform.up * cannonBallSeperationDistance * cannonBallPositions[i];
            Vector3 horizontalOffset = transform.right * Random.Range(0, 0.5f);

            CannonBall.CreateCannonBall((transform.position + verticalOffset + horizontalOffset), direction, rb.velocity);

        }
    }

    private void FixedUpdate() {
        float h = -Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector2 speed = transform.up * (v * acceleration);

        rb.AddForce(speed);

        float direction = Vector2.Dot(rb.velocity, rb.GetRelativeVector(Vector2.up));
        if (direction >= 0.0f) {
            rb.rotation += h * steering * (rb.velocity.magnitude / 5.0f);
            //rb.AddTorque((h * steering) * (rb.velocity.magnitude / 10.0f));
        } else {
            rb.rotation -= h * steering * (rb.velocity.magnitude / 5.0f);
            //rb.AddTorque((-h * steering) * (rb.velocity.magnitude / 10.0f));
        }

        Vector2 forward = new Vector2(0.0f, 0.5f);
        float steeringRightAngle;
        if (rb.angularVelocity > 0) {
            steeringRightAngle = -90;
        } else {
            steeringRightAngle = 90;
        }

        Vector2 rightAngleFromForward = Quaternion.AngleAxis(steeringRightAngle, Vector3.forward) * forward;
        // Debug.DrawLine((Vector3)rb.position, (Vector3)rb.GetRelativePoint(rightAngleFromForward), Color.green);

        float driftForce = Vector2.Dot(rb.velocity, rb.GetRelativeVector(rightAngleFromForward.normalized));

        Vector2 relativeForce = (rightAngleFromForward.normalized * -1.0f) * (driftForce * 10.0f);

        // Debug.DrawLine((Vector3)rb.position, (Vector3)rb.GetRelativePoint(relativeForce), Color.red);

        rb.AddForce(rb.GetRelativeVector(relativeForce));

        float currentSpeed = Vector3.Magnitude(rb.velocity);  // test current object speed

        if (currentSpeed > maximumSpeed) {
            float brakeSpeed = currentSpeed - maximumSpeed;  // calculate the speed decrease

            Vector3 normalisedVelocity = rb.velocity.normalized;
            Vector3 brakeVelocity = normalisedVelocity * brakeSpeed;  // make the brake Vector3 value

            rb.AddForce(-brakeVelocity);  // apply opposing brake force
        }
    }
}
