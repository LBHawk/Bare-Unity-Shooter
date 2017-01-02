using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookieChecker : MonoBehaviour {

    bool isEatable = false;
    public int timeAlive = 250;

    void FixedUpdate()
    {
        if (this.transform.position.y < 0.15f)
        {
            this.transform.position.Set(this.transform.position.x, 0.15f, this.transform.position.z);
        }

        timeAlive--;

        //Debug.Log(timeAlive);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && isEatable)
        {
            Destroy(this.gameObject);
            Debug.Log("yum");
        }
        else if (other.gameObject.tag == "Environment" || timeAlive < 0)
        {
            isEatable = true;
        }
    }
}
