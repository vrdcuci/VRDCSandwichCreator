using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snap : MonoBehaviour
{
    public GameObject left;
    public GameObject middle;
    public GameObject right;

    private GameObject collidingObject;

    public float snapHeight = 0.0f;
    private bool canSnap = true;

    private void OnTriggerEnter(Collider col)
    {
        SetCollidingObject(col);
    }

    private void OnTriggerStay(Collider col)
    {
        //SetCollidingObject(col);
    }

    private void SetCollidingObject(Collider col)
    {
        if (col.tag == "Ingredients")
        {
            collidingObject = col.gameObject;
        }
    }

    public void OnTriggerExit(Collider col)
    {
        if (collidingObject)
        {
            collidingObject = null;
        }
    }

    private void SnapObject(GameObject gameObj)
    {
        if(((Mathf.Approximately(left.GetComponent<Snap>().snapHeight, middle.GetComponent<Snap>().snapHeight)) && (Mathf.Approximately(middle.GetComponent<Snap>().snapHeight, right.GetComponent<Snap>().snapHeight))) && gameObj.name == "breadtop(Clone)")
        {
            gameObj.transform.position = middle.transform.position;
            gameObj.GetComponent<IngredientSettings>().isAttached = true;
            gameObj.transform.SetParent(transform.parent);
            Rigidbody rb = gameObj.GetComponent<Rigidbody>();

            rb.isKinematic = true;
            rb.detectCollisions = false;

            gameObj.transform.localRotation = Quaternion.Euler(0, 0, 0);
            left.GetComponent<Snap>().canSnap = false;
            middle.GetComponent<Snap>().canSnap = false;
            right.GetComponent<Snap>().canSnap = false;
        }
        else if(canSnap && !gameObj.GetComponent<IngredientSettings>().isAttached && gameObj.name != "breadtop(Clone)")
        {
            gameObj.GetComponent<IngredientSettings>().isAttached = true;
            gameObj.transform.SetParent(transform.parent);
            Rigidbody rb = gameObj.GetComponent<Rigidbody>();

            rb.isKinematic = true;
            rb.detectCollisions = false;

            gameObj.transform.localPosition = transform.localPosition;
            snapHeight += gameObj.GetComponent<IngredientSettings>().thickness;
            transform.Translate(gameObj.GetComponent<IngredientSettings>().thickness * Vector3.forward);
            gameObj.transform.localRotation = transform.localRotation * Quaternion.Euler(0,0, 8 - 16 * UnityEngine.Random.value);
        }
        collidingObject = null;
    }

    private void Update()
    {
        if(collidingObject)
        {
            SnapObject(collidingObject);
        }
    }
}
