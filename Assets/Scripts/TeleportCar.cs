using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeleportCar : MonoBehaviour
{
    public GameObject car;
    public int index;
    public List<int> listOfTeleports;

    public Text material;
    public List<string> listOfMaterials;

    public Text friction;
    public List<double> listOfFrictionCoefficients;
    public void teleport() {
        if (index >= 5) {
            index = 0;
            car.GetComponent<CentripetalForce>().index = 0;
        } else {
            index++;
            car.GetComponent<CentripetalForce>().index++;
        }

        car.GetComponent<CentripetalForce>().circleCenter = car.GetComponent<CentripetalForce>().listOfCenters[index];

        car.transform.position = new Vector3(-10, 1.5f, listOfTeleports[index]);
        material.text = "Material: " + listOfMaterials[index];
        friction.text = "Friction Coefficient: " + listOfFrictionCoefficients[index];
        
    }
}
