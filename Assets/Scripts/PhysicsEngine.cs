using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhysicsEngine : MonoBehaviour {
	public float mass;              // [kg]
	public Vector3 velocityVector;  // [m s^-1]
	public Vector3 netForceVector;  // N [kg m s^-2]
	private Vector3 forwardForceVector;
	public double vAverage;
	public GameObject Direction;
	public Text velocityText;

	public List<Vector3> forceVectorList = new List<Vector3>();

	// Use this for initialization
	void Start() {
		SetupThrustTrails();
		forwardForceVector = new Vector3(0, 0, 0);

	}

	void FixedUpdate() {
		RenderTrails();
		UpdatePosition();
	}

	public void AddForce(Vector3 forceVector) {
		forceVectorList.Add(forceVector);
	}

	void UpdatePosition() {

        //Direction
        Vector3 offset = Direction.transform.position - transform.position;
        forwardForceVector = offset.normalized * 2000;
        forceVectorList.Add(forwardForceVector);
		Debug.Log("Forward Velocity Magn: " + forwardForceVector.magnitude);

		// Sum the forces and clear the list
		netForceVector = Vector3.zero;
		foreach (Vector3 forceVector in forceVectorList) {
			netForceVector = netForceVector + forceVector;
		}
		forceVectorList = new List<Vector3>();

		// Calculate position change due to net force
		Vector3 accelerationVector = netForceVector / mass;
		velocityVector += accelerationVector * Time.deltaTime;
		transform.position += velocityVector * Time.deltaTime;
	}

	/// Code for drawing thrust tails
	public bool showTrails = true;

	private LineRenderer lineRenderer;
	private int numberOfForces;

	// Use this for initialization
	void SetupThrustTrails() {
		lineRenderer = gameObject.AddComponent<LineRenderer>();
		lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
		lineRenderer.SetColors(Color.yellow, Color.yellow);
		lineRenderer.SetWidth(0.2F, 0.2F);
		lineRenderer.useWorldSpace = true;
	}

	// Update is called once per frame
	void RenderTrails() {
		if (showTrails) {
			lineRenderer.enabled = true;
			numberOfForces = forceVectorList.Count;
			lineRenderer.SetVertexCount(numberOfForces * 2);
			int i = 0;
			foreach (Vector3 forceVector in forceVectorList) {
				lineRenderer.SetPosition(i, GetComponent<Transform>().position);
				lineRenderer.SetPosition(i + 1, forceVector);
				i = i + 2;
			}
		}
		else {
			lineRenderer.enabled = false;
		}
	}

	void calcAverageV() {
		vAverage = Math.Pow(velocityVector.x, 2.0) + Math.Pow(velocityVector.z, 2.0);
		vAverage = Math.Sqrt(vAverage);

		velocityText.text = "Velocity: " + vAverage.ToString("0.00");
	}

	private void Update() {
		calcAverageV();
	}

}