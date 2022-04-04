using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentripetalForce : MonoBehaviour {

    public GameObject circleCenter;
    private float angle;
    public List<PhysicMaterial> listOfPhysicMaterials;
    public List<GameObject> listOfCenters;
    public double staticFriction;
    public int index = 0;

    // Start is called before the first frame update
    void Start() {
        circleCenter = listOfCenters[index];
        angle = 60.0f;
    }

    // Update is called once per frame
    void FixedUpdate() { 
        CalculateCentripetalForce();
        staticFriction = listOfPhysicMaterials[index].staticFriction;
    }

    void CalculateCentripetalForce() {
    if (circleCenter) {
            Debug.Log("FOUND CENTER OF CIRCLE");

            // Find the (r) distance between Object A and Object B:
            //float distance = Vector3.Distance(transform.position, ObjectB.transform.position);

            // Find (r^2) distance to the power of two; use Mathf.Pow: 
            //float distanceSquared = Mathf.Pow(distance, 2);

            // Find (Fg) magnitude of the gravity force; 
            //float gravityMagnitude = (gravCoeeficient * ObjectA.GetComponent<PhysicsEngine>().mass * ObjectB.GetComponent<PhysicsEngine>().mass) / distanceSquared;
            //Debug.Log("Gravity Magnitude: " + gravityMagnitude);
            // Normalizing the gravity; Just uncomment the line below:
            Vector3 offset = transform.position - circleCenter.transform.position;

            float velocityMagnitude = GetComponent<PhysicsEngine>().velocityVector.magnitude;
            //Vector3 velocityNormalized = GetComponent<PhysicsEngine>().velocityVector.normalized;

            //Vector3 velocitySquared = Mathf.Pow(velocityMagnitude, 2) * velocityNormalized;

            // Vector3 centripetalForce = GetComponent<PhysicsEngine>().mass * Mathf.Pow(velocityMagnitude, 2) * offset.normalized / 30;
            //Above formula needs to change to
            //centripetalForce = Static Coefficient of Friction * N (where N = Normal Force = mg, or just m in our case)
            Vector3 centripetalForce = (float) staticFriction * GetComponent<PhysicsEngine>().mass * offset.normalized / 30;

            //Debug.Log(centripetalForce);
            // Add the force to the list of the forces on physicsEngine for object A; 
            // Note that you need to take care of the negative sign (downward acceleration) manually:

            // Rotate the cube by converting the angles into a quaternion.
            float tiltAroundZX = Input.GetAxis("Horizontal") * angle + -60;
            Debug.Log(tiltAroundZX);
            //angle += 4 * 0.1f;
            Quaternion target = Quaternion.Euler(0, tiltAroundZX, 0);
            transform.rotation = Quaternion.EulerRotation(0, tiltAroundZX, 0);
            //transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 5.0f);
            GetComponent<PhysicsEngine>().AddForce(-centripetalForce);
        }
}
}
