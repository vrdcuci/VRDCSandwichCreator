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

    private GameObject middle;

    public void Start()
    {
        middle = GameObject.Find("whitebreadtop");
    }

    private GameObject collidingObject;

    public float snapHeight = 0.0f;
    public bool canSnap = true;

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
        if (gameObj.tag == "TopBread")
        {
            gameObj.transform.SetParent(transform);
            Rigidbody rb2 = gameObj.GetComponent<Rigidbody>();
            rb2.isKinematic = true;
            rb2.detectCollisions = false;
            gameObj.transform.position = middle.transform.position;
            gameObj.transform.rotation = middle.transform.rotation;
            Debug.Log("Top Bread detected");

            return;
        }

        if (canSnap && !gameObj.GetComponent<IngredientSettings>().isAttached)
        {
            gameObj.GetComponent<IngredientSettings>().isAttached = true;
            gameObj.transform.SetParent(transform.parent);
            Rigidbody rb = gameObj.GetComponent<Rigidbody>();
            rb.isKinematic = true;
            rb.detectCollisions = true;

            gameObj.transform.localPosition = transform.localPosition;
            snapHeight += gameObj.GetComponent<IngredientSettings>().thickness;
            transform.Translate(gameObj.GetComponent<IngredientSettings>().thickness * Vector3.forward);

            if(gameObj.GetComponent<BreadController>())
            {
                canSnap = false;
                gameObj.transform.localRotation = Quaternion.Euler(0, 0, 0);
            } else
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
