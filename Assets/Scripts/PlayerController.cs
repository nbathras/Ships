using UnityEngine;

public class PlayerController : MonoBehaviour {
    // private float speed = 0;

    //this is our target velocity while decelerating
    [SerializeField] private float initialVelocity = 0.0f;

    //this is our target velocity while accelerating
    [SerializeField] private float finalVelocity = 500.0f;

    //this is our current velocity
    [SerializeField] private float currentVelocity = 0.0f;

    //this is the velocity we add each second while accelerating
    [SerializeField] private float accelerationRate = 10.0f;

    //this is the velocity we subtract each second while decelerating
    [SerializeField] private float decelerationRate = 50.0f;

    // Update is called once per frame
    private void Update() {
        /*
        if (Input.GetKeyDown(KeyCode.Alpha0)) {
            speed = 0;
        } else if (Input.GetKeyDown(KeyCode.Alpha1)) {
            speed = 1;
        } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            speed = 2;
        } else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            speed = 3;
        }
        */

        Debug.Log("test1");

        if (Input.GetKey(KeyCode.W)) {
            Debug.Log("test2");
            //add to the current velocity according while accelerating
            currentVelocity = currentVelocity + (accelerationRate * Time.deltaTime);
        } else {
            //subtract from the current velocity while decelerating
            currentVelocity = currentVelocity - (decelerationRate * Time.deltaTime);
        }

        //ensure the velocity never goes out of the initial/final boundaries
        currentVelocity = Mathf.Clamp(currentVelocity, initialVelocity, finalVelocity);

        //propel the object forward
        transform.Translate(0, currentVelocity, 0);
    }
 
}
