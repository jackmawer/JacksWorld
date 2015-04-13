using UnityEngine;
using System.Collections;

public class StopFallingOutOfWorld : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		if (transform.position.y <= -10) {
			Debug.Log ("Fell out of world!");
			transform.position = new Vector3(1, 1, 1);
		}
	}
}
