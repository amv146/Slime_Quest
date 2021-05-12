using System.Collections;
using System.Collections.Generic;
using Unity;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour {
    float moveDuration = 0.3f;
    public int movementAmount = 3;
    public bool readyToMove = true;

    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void MoveTo(Vector3 position) {
        StartCoroutine(Lerp(position));
    }

    public IEnumerator Lerp(Vector3 position) {
        readyToMove = false;
        float timeElapsed = 0;
        Vector3 originalPosition = transform.position;
        while (timeElapsed < moveDuration) {
            transform.position = Vector3.Lerp(originalPosition, position, timeElapsed / moveDuration);
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        transform.position = position;
        readyToMove = true;
    }
}