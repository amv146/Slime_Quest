using UnityEngine;
using System.Collections;

public static class GameObjectUtils {
    public static GameObject ObjectAt(Vector3 position) {
        Collider[] colliders;
        if ((colliders = Physics.OverlapSphere(position, 0.25f)).Length > 0) //Presuming the object you are testing also has a collider 0 otherwise
        {
            return colliders[0].gameObject;
        }
        else {
            return null;
        }
    }
}
