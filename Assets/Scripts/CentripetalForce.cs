using System.Collections.Generic;
using UnityEngine;

public class CentripetalForce : MonoBehaviour {

    public GameObject circleCenter; // Reference direction for applying CF around the track
    public List<PhysicMaterial> listOfPhysicMaterials;
    public List<GameObject> listOfCenters;
    public double staticFriction;
    public int index = 0;

    void Start() {
        circleCenter = listOfCenters[index];
    }

    void FixedUpdate() {
        if (Input.GetKey(KeyCode.A)) { // Apply rotation and calculate CF
            transform.Rotate(new Vector3(0, -Time.deltaTime * 60, 0));
            Vector3 centripetalForce = CalculateCentripetalForce();
            GetComponent<PhysicsEngine>().AddForce(centripetalForce);
        }

        if (Input.GetKey(KeyCode.D)) {
            transform.Rotate(new Vector3(0, Time.deltaTime * 60, 0));
            GetComponent<PhysicsEngine>().AddForce(CalculateCentripetalForce());
        }

        staticFriction = listOfPhysicMaterials[index].staticFriction;
    }

    // Calculates Centripetal Force using friction value of surface of track
    Vector3 CalculateCentripetalForce() {

            Vector3 offset = transform.position - circleCenter.transform.position;
            Vector3 centripetalForce = (float) staticFriction * GetComponent<PhysicsEngine>().mass * offset.normalized * 9.81f; // Apply CF towards center of circle
            return -centripetalForce;
        
    }
}
