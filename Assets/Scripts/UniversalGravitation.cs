using UnityEngine;

public class UniversalGravitation : MonoBehaviour {

	private PhysicsEngine[] physicsEngineArray;

	/* -------------------- PART 2 ------------------------------------*/
	//Define the private constant for the G (gravitational coeeficient):
	private readonly float gravCoeeficient = 6.674f * Mathf.Pow(10, -11);
	

	// Use this for initialization
	void Start() {
		physicsEngineArray = GameObject.FindObjectsOfType<PhysicsEngine>();
	}

	void FixedUpdate() {
		CalculateGravity();
	}

	/* -------------------- PART 2 ------------------------------------*/
	void CalculateGravity()
	{
		// for each object in physicsEngineArray:
		foreach (PhysicsEngine ObjectA in physicsEngineArray) {
			// for each object in the physicsEngineArray:
			foreach (PhysicsEngine ObjectB in physicsEngineArray)	{

				// Now we have two objects A and B; we can calulcate the Fg.
				//Eliminate duplicatation: if ObjectA is not Object B
				//Eliminate gravity on itself: If objectA=!this
				if(ObjectA != ObjectB && ObjectA != this) {

					// Find the (r) distance between Object A and Object B:
					float distance = Vector3.Distance(ObjectA.transform.position, ObjectB.transform.position);

					// Find (r^2) distance to the power of two; use Mathf.Pow: 
					float distanceSquared = Mathf.Pow(distance, 2);

					// Find (Fg) magnitude of the gravity force; 
					float gravityMagnitude = (gravCoeeficient * ObjectA.GetComponent<PhysicsEngine>().mass
									* ObjectB.GetComponent<PhysicsEngine>().mass) / distanceSquared;
					Debug.Log("Gravity Magnitude: " + gravityMagnitude);
					// Normalizing the gravity; Just uncomment the line below:
					Vector3 offset = ObjectA.transform.position - ObjectB.transform.position;
					Vector3 gravityFeltVector = gravityMagnitude * offset.normalized;

					// Add the force to the list of the forces on physicsEngine for object A; 
					// Note that you need to take care of the negative sign (downward acceleration) manually:
					ObjectA.GetComponent<PhysicsEngine>().AddForce(-gravityFeltVector);

				}
			}
		}
	}
}