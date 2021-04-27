using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    float moveDuration = 0.20f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveTo(Vector3 position) {
        StartCoroutine(Lerp(position));
    }

    public IEnumerator Lerp(Vector3 position) {
        float timeElapsed = 0;
        Vector3 originalPosition = transform.position;

        while (timeElapsed < moveDuration) {
            transform.position = Vector3.Lerp(originalPosition, position, timeElapsed / moveDuration);
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        transform.position = position;
    }
}
