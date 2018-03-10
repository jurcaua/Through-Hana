using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Transform playerHead;

    public Vector3 GetPosition() {
        return transform.position;
    }
}
