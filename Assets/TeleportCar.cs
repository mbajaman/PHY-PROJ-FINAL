using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportCar : MonoBehaviour
{
    public GameObject car;
    public int index;
    public List<int> listOfTeleports;
    public void teleport() {
        if (index >= 5) {
            index = 0;
        } else {
            index++;
        }

        car.transform.position = new Vector3(10, 1.5f, listOfTeleports[index]);

    }
}
