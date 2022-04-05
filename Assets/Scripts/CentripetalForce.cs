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
        angle = 90.0f;
    }

    // Update is called once per frame
    void FixedUpdate() {
        //CalculateCentripetalForce();
        if (Input.GetKey(KeyCode.A)) {
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

    Vector3 CalculateCentripetalForce() {

            Vector3 offset = transform.position - circleCenter.transform.position;

            float velocityMagnitude = GetComponent<PhysicsEngine>().velocityVector.magnitude;

            //Vector3 centripetalForce = GetComponent<PhysicsEngine>().mass * Mathf.Pow(velocityMagnitude, 2) * offset.normalized / 30;
            //Above formula needs to change to
            //centripetalForce = Static Coefficient of Friction * N (where N = Normal Force = mg, or just m in our case)
            Vector3 centripetalForce = (float) staticFriction * GetComponent<PhysicsEngine>().mass * offset.normalized * 9.81f;

            Debug.Log("CF Magnitude: " + centripetalForce.magnitude);
            return -centripetalForce;
        
    }
}
