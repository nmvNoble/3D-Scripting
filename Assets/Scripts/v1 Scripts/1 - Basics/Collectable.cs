using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Collected by Player.");
            Destroy(this.gameObject);
        }
        else if (other.tag == "Objects")
        {
            Debug.Log("Collected by Object.");
            Destroy(this.gameObject);
        }
    }
}
