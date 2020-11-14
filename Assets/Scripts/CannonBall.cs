using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CannonBall : MonoBehaviour {
    public static CannonBall CreateCannonBall(Vector3 inPosition, Vector3 inDirNormalized, Vector3 inCurrentVelocity) {
        Transform pfCannonBall = Resources.Load<Transform>("pfCannonBall");
        Transform cannonBallTransform = Instantiate(pfCannonBall, inPosition, Quaternion.identity);

        CannonBall cannonBall = cannonBallTransform.GetComponent<CannonBall>();
        cannonBall.rb.velocity = inDirNormalized * cannonBall.speed + inCurrentVelocity;

        return cannonBall;
    }

    [SerializeField] private float speed;
    [SerializeField] private float despawnTimer;

    private Rigidbody2D rb;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        despawnTimer -= Time.deltaTime;
        if (despawnTimer <= 0) {
            Destroy(gameObject);
        }
    }
}
