using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour {
    public GameObject cookie;
    public float speed;

    Rigidbody rb;
    int floorMask;
    float camRayLength = 100f;
    GameObject barrelEnd;
    Transform barrelEndTransform;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        floorMask = LayerMask.GetMask("Floor");
        barrelEnd = this.transform.Find("Barrel End").gameObject;
        barrelEndTransform = barrelEnd.transform;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        Turning();

        if (Input.GetButton("Fire2") && GameObject.FindGameObjectWithTag("Cookie") == null)
        {
            ShootCookie();
        }
    }

    public void ShootCookie()
    {
        Vector3 spawnPos = barrelEndTransform.position;
        spawnPos.y += .2f;
        GameObject newCookie = Instantiate(cookie, barrelEndTransform.position, barrelEndTransform.rotation) as GameObject;

        Debug.Log(barrelEndTransform.position);

        Vector3 vel = AngleFromMouse().normalized * speed;
        vel.Set(vel.x, 4, vel.z);
        newCookie.GetComponent<Rigidbody>().velocity = vel;
        
    }



    void Turning()
    {
        Vector3 playerToMouse = AngleFromMouse();

        playerToMouse.y = 0f;

        // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
        Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

        rb.MoveRotation(newRotation);
    }

    Vector3 AngleFromMouse()
    {
        // Create a ray from the mouse cursor on screen in the direction of the camera.
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Create a RaycastHit variable to store information about what was hit by the ray.
        RaycastHit floorHit;

        Vector3 playerToMouse = new Vector3();

        // Perform the raycast and if it hits something on the floor layer...
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            // Create a vector from the player to the point on the floor the raycast from the mouse hit.
            playerToMouse = floorHit.point - transform.position; 
            
        }

        return playerToMouse;
    }
}
