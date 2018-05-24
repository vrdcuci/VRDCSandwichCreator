using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDestruction : MonoBehaviour {

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Ingredients" || other.gameObject.tag == "Utility")
        Destroy(other.gameObject);
    }
}
