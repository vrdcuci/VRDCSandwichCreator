using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snap : MonoBehaviour
{
    /// <summary>
    /// To-do:
    /// -trash can
    /// -height stacking
    /// -randomize rotation
    /// </summary>
    /// 


    public GameObject left;
    public GameObject middle;
    public GameObject right;

    public void Start()
    {
    }

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
        if (col.tag == "Ingredients" || col.tag == "TopBread")
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
    /// <summary>
    /// Snaps the target gameobject to the position of *this* gameObject.
    /// </summary>
    /// <param name="source"></param>
    private void SnapObject(GameObject gameObj)
    {
        if (((Mathf.Approximately(left.GetComponent<Snap>().snapHeight, middle.GetComponent<Snap>().snapHeight)) && (Mathf.Approximately(middle.GetComponent<Snap>().snapHeight, right.GetComponent<Snap>().snapHeight))) && gameObj.name == "whitebreadtop")
        {
            gameObj.transform.position = middle.transform.position;
            //gameObj.transform.rotation = middle.transform.rotation;

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

        if (canSnap && !gameObj.GetComponent<IngredientSettings>().isAttached && gameObj.name != "whitebreadtop")
        {
            gameObj.GetComponent<IngredientSettings>().isAttached = true;
            gameObj.transform.SetParent(transform.parent);
            Rigidbody rb = gameObj.GetComponent<Rigidbody>();
            rb.isKinematic = true;
            rb.detectCollisions = false;

            gameObj.transform.localPosition = transform.localPosition;
            snapHeight += gameObj.GetComponent<IngredientSettings>().thickness;
            transform.Translate(gameObj.GetComponent<IngredientSettings>().thickness * Vector3.forward);

            if(gameObj.name != "whitebreadtop")
                gameObj.transform.localRotation = transform.localRotation * Quaternion.Euler(0,0, 8 - 16 * UnityEngine.Random.value);
        }

    }

    private void Update()
    {
        if(collidingObject)
        {
            SnapObject(collidingObject);
        }
    }
}
