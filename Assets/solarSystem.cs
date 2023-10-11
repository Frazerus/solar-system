using System.Linq;
using UnityEngine;

public class solarSystem : MonoBehaviour
{
    public GameObject[] allBodies;
    public int G;

    public int startingSpeed = 1; 

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = startingSpeed;
        allBodies = GameObject.FindGameObjectsWithTag("Celestial");
        var sun = GameObject.Find("Sun");
        foreach (var body in allBodies) {
            if (body == sun)
                continue;
            
            body.GetComponent<Rigidbody>().velocity = Mathf.Sqrt(G * sun.GetComponent<Rigidbody>().mass / Vector3.Distance(sun.transform.position, body.transform.position)) * Vector3.forward;
        }

        var moon = allBodies.First(o => o.name == "Moon");
        var earth = allBodies.First(o => o.name == "Earth");
        moon.GetComponent<Rigidbody>().velocity -= Mathf.Sqrt(G * earth.GetComponent<Rigidbody>().mass/Vector3.Distance(earth.transform.position, moon.transform.position)) * Vector3.forward;
    }

    void Update ()
    {
        float ScrollWheelChange = Input.GetAxis("Mouse ScrollWheel");
        if (ScrollWheelChange != 0){                                 
            float Y = ScrollWheelChange * -1500;                     
            float CamX = Camera.main.transform.position.x;
            float CamY = Camera.main.transform.position.y;
            float CamZ = Camera.main.transform.position.z;
            Camera.main.transform.position = new Vector3(CamX, CamY + Y, CamZ);
        }

        if (Input.GetKeyDown(KeyCode.K)) {
            Time.timeScale += 1;
        }

        if (Input.GetKeyDown(KeyCode.J) && Time.timeScale >= 1) {
            Time.timeScale -= 1;
        }

        if (Input.GetKeyDown(KeyCode.Q) && G >= 5){
            G -= 5;
        }

        if (Input.GetKeyDown(KeyCode.E)){
            G += 5;
        }
    }

    void FixedUpdate()
    {
        foreach (var body in allBodies ){
            var rb = body.GetComponent<Rigidbody>();
            foreach (var otherBody in allBodies) {
                if (body == otherBody)
                    continue;
                
                var dir =  otherBody.transform.position - body.transform.position;
                var sqrDistance = dir.sqrMagnitude;
                var F = G * (body.GetComponent<Rigidbody>().mass * otherBody.GetComponent<Rigidbody>().mass) / sqrDistance;

                rb.AddForce((float)F * dir.normalized);
            }
        }
    }
}
