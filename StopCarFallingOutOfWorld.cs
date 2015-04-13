using UnityEngine;
using System.Collections;

public class StopCarFallingOutOfWorld : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		if (transform.position.y <= -10) {
			Debug.Log ("Fell out of world!");
			transform.position = new Vector3(10, 3, 10);
		}
	}
}
