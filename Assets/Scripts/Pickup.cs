using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Pickup : MonoBehaviour {

    // private components
    private Collider pickupCollider;

    // Private check variables
    private bool isCollidingWithPlayer = false;
    
	void Start () {
        // Get the collider and check if trigger
        pickupCollider = GetComponent<Collider>();
        if (!pickupCollider.isTrigger) {
            Debug.LogWarning(gameObject.name + " was not set to trigger! Setting to trigger...");
            pickupCollider.isTrigger = true;
        }
	}

    void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Player") && !isCollidingWithPlayer) {
            isCollidingWithPlayer = true;
            Debug.Log(gameObject.name + " collided with " + collider.name + "!");

            HanaController hanaController = collider.GetComponentInParent<HanaController>();
            if (hanaController != null) {
                hanaController.GrowHana();
            } else {
                Debug.LogWarning(collider.name + " is on player layer but does not have \"HanaController\" component.");
            }

            Destroy(gameObject);
        }
    }

    void OnTriggerExit(Collider collider) {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Player") && isCollidingWithPlayer) {
            isCollidingWithPlayer = false;
        }
    }
}
