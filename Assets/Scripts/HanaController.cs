using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HanaController : MonoBehaviour {

    public Transform flowers;
    public float startScale = 2f;
    public float endScale = 4f;
    public Vector2 growRange = new Vector2(1f, 2f);

    private List<Transform> hanas;
    private int currentHana = 0;

    private Vector3 startPosition;
    private Vector3 startScaleVector;
    private Vector3 endScaleVector;

    [Header("Debug")]
    public bool debug = false;

    // testing
    enum BendAxis { X, Y, Z };

    void Start() {
        startPosition = flowers.localPosition;

        startScaleVector = new Vector3(startScale, startScale, startScale);
        endScaleVector = new Vector3(endScale, endScale, endScale);

        hanas = new List<Transform>();
        for (int i = 0; i < flowers.childCount; i++) {
            hanas.Add(flowers.GetChild(i));
            hanas[i].localScale = startScaleVector;
        }
        currentHana = 0;
    }

    public void GrowHana() {
        StartCoroutine(Bloom());
    }

    public void BendHana() {
        StartCoroutine(Flow());
    }

    private IEnumerator Bloom() {
        if (hanas.Count > currentHana) {
            if (debug) {
                Debug.Log("Hana " + currentHana + " is growing!");
            }

            Transform flower = hanas[currentHana];
            int thisFlowerIndex = currentHana;
            currentHana++;

            flower.rotation = GetFlowerRandomRotation();
            float growOut = Random.Range(growRange.x, growRange.y);

            bool growing = true;
            float t = 0;
            while (growing) {
                if (t >= 1) {
                    growing = false;
                }
                flower.localScale = Vector3.Lerp(startScaleVector, endScaleVector, t);
                flower.localPosition = Vector3.Lerp(startPosition, startPosition + flower.up * growOut, t);
                t += 0.02f;
                yield return new WaitForSeconds(Time.deltaTime);
            }
            BendHana();

            if (debug) {
                Debug.Log("Hana " + thisFlowerIndex + " is done growing. :)");
            }
        } else {
            if (debug) {
                Debug.Log("No more flowers!");
            }
        }
    }

    void Update() {
        
    }

    private IEnumerator Flow() {
        if (currentHana > 0) { // If at least one flower has been spawned
            // should be a for loop but this is just a test of some code
            int j = 0;

        }
        yield return null;
    }

    Quaternion GetFlowerRandomRotation() {
        return Quaternion.Euler(Random.Range(-100, 100), Random.Range(0, 360), Random.Range(-100, 100));
    }
}
