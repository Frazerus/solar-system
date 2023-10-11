using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class outputDist : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        print(Vector3.Distance(this.transform.position, Vector3.zero));
    }
}
