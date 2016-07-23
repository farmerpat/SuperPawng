using UnityEngine;
using System.Collections;

public class BackgroundController : MonoBehaviour {

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {

    }

    void OnTriggeredEnter2D (Collider2D col) {
        Debug.Log ("bg detected a collision");

    }
}
