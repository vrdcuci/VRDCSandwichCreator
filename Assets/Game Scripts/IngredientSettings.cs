using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientSettings : MonoBehaviour
{
    public bool isAttached = false;
    public float thickness;

    private void OnCollisionEnter(Collision collision)
    {
        IngredientSettings ingredientSettings = collision.gameObject.GetComponent<IngredientSettings>();
        //ingredientSettings != null
        if (collision.gameObject.tag == "Ingredients" && isAttached && ingredientSettings.isAttached)
        {
            Physics.IgnoreCollision(GetComponent<Collider>(), collision.gameObject.GetComponent<Collider>());
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        IngredientSettings ingredientSettings = collision.gameObject.GetComponent<IngredientSettings>();
        if (collision.gameObject.tag == "Ingredients" && isAttached && ingredientSettings.isAttached)
        {
            Physics.IgnoreCollision(GetComponent<Collider>(), collision.gameObject.GetComponent<Collider>());
        }
    }
}
