using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UIElements;

public class celestialData : MonoBehaviour
{
    public double Mass;
    public bool passed = true;

    bool didSpin = false;
    


    void Update () {
        if (didSpin)
            return;
        if (!passed && gameObject.transform.position.z > 0) {
            passed = true;
            print($"{gameObject.name} has done a spin in {Time.timeAsDouble}.");
            didSpin = true;
        }
        if (gameObject.transform.position.x < 0 && gameObject.transform.position.z < 0 )
            passed = false;
    }
}
