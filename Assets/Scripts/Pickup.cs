using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {
    GameObject player;
    public float InactiveTime = 20f;

	// Use this for initialization
	void Awake () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            other.gameObject.GetComponent<PlayerHealth>().Pickup();
            StartCoroutine(Reactivate());
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
        }
    }

	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }

    IEnumerator Reactivate()
    {
        Debug.Log("Reactivate started");
        yield return new WaitForSeconds(InactiveTime);
        GetComponent<BoxCollider>().enabled = true;
        GetComponent<MeshRenderer>().enabled = true;
        Debug.Log("Reactivate Ended");
    }
}
