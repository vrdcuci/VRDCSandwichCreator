using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrabbing : MonoBehaviour {

    // reference to tracked object
    private SteamVR_TrackedObject trackedObj;
    // reference object to what we have collided with
    private GameObject collidingObject;
    // reference to object currently in hand
    private GameObject objectInHand;
    // reference to Controller for inputs
    private SteamVR_Controller.Device Controller
    {
        get
        {
            return SteamVR_Controller.Input((int)trackedObj.index);
        }
    }

    private void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    private void SetCollidingObject(Collider col)
    {
        if (col.gameObject.tag == "Ingredients" || col.gameObject.tag == "Utility" || col.gameObject.tag == "Respawn")
        {
            //col.transform.localScale *= 1.2f;
            collidingObject = col.gameObject;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        SetCollidingObject(other);
    }

    public void OnTriggerStay(Collider other)
    {
        //SetCollidingObject(other);
    }

    public void OnTriggerExit(Collider other)
    {
        if(collidingObject)
        {
            collidingObject = null;
            //other.transform.localScale = new Vector3(1,1,1);
        }   
    }

    //Spawnable Prefabs
    public GameObject swissPrefab;
    public GameObject cheddarPrefab;
    public GameObject americanPrefab;
    public GameObject hamPrefab;
    public GameObject turkeyPrefab;
    public GameObject salamiPrefab;
    public GameObject pepperoniPrefab;
    public GameObject lettucePrefab;
    public GameObject onionPrefab;
    public GameObject tomatoPrefab;

    private void SpawnGrab()
    {
        SpawnType type = collidingObject.GetComponent<SpawnSettings>().type;
        GameObject spawnedObject;
        switch (type)
        {
            case SpawnType.SWISS:
                spawnedObject = Instantiate(swissPrefab, transform.position, swissPrefab.transform.rotation);
                collidingObject = spawnedObject;
                break;
            case SpawnType.CHEDDAR:
                spawnedObject = Instantiate(cheddarPrefab, transform.position, cheddarPrefab.transform.rotation);
                collidingObject = spawnedObject;
                break;
            case SpawnType.AMERICAN:
                spawnedObject = Instantiate(americanPrefab, transform.position, americanPrefab.transform.rotation);
                collidingObject = spawnedObject;
                break;
            case SpawnType.HAM:
                spawnedObject = Instantiate(hamPrefab, transform.position, hamPrefab.transform.rotation);
                collidingObject = spawnedObject;
                break;
            case SpawnType.TURKEY:
                spawnedObject = Instantiate(turkeyPrefab, transform.position, turkeyPrefab.transform.rotation);
                collidingObject = spawnedObject;
                break;
            case SpawnType.SALAMI:
                spawnedObject = Instantiate(salamiPrefab, transform.position, salamiPrefab.transform.rotation);
                collidingObject = spawnedObject;
                break;
            case SpawnType.PEPPERONI:
                spawnedObject = Instantiate(pepperoniPrefab, transform.position, pepperoniPrefab.transform.rotation);
                collidingObject = spawnedObject;
                break;
            case SpawnType.LETTUCE:
                spawnedObject = Instantiate(lettucePrefab, transform.position, lettucePrefab.transform.rotation);
                collidingObject = spawnedObject;
                break;
            case SpawnType.ONION:
                spawnedObject = Instantiate(onionPrefab, transform.position, onionPrefab.transform.rotation);
                collidingObject = spawnedObject;
                break;
            case SpawnType.TOMATO:
                spawnedObject = Instantiate(tomatoPrefab, transform.position, tomatoPrefab.transform.rotation);
                collidingObject = spawnedObject;
                break;
        }
        GrabObject();
    }

    private void GrabObject()
    {
        objectInHand = collidingObject;
        //collidingObject.transform.localScale *= 1.2f;
        collidingObject = null;

        var joint = AddFixedJoint();
        joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
    }

    private FixedJoint AddFixedJoint()
    {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 1000;
        fx.breakTorque = 1000;
        return fx;
    }


    private void ReleaseObject()
    {
        if(GetComponent<FixedJoint>())
        {
            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());
            objectInHand.GetComponent<Rigidbody>().velocity = Controller.velocity;
            objectInHand.GetComponent<Rigidbody>().angularVelocity = Controller.angularVelocity;
        }
        objectInHand = null;
    }

    // Update is called once per frame
    void Update () {
		if(Controller.GetHairTriggerDown())
        {
            if (collidingObject)
            {
                if (collidingObject.gameObject.tag == "Ingredients" )
                {
                    GrabObject();
                }
                else if (collidingObject.gameObject.tag == "Respawn")
                {
                    SpawnGrab();
                }
            }
        }

        if(Controller.GetHairTriggerUp())
        {
            if(objectInHand)
            {
                ReleaseObject();
            }
        }
	}
}
