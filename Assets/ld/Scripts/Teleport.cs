using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour {
	[SerializeField] private GameObject destinationGameObject;

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log ("Trigger!");
		if (other.attachedRigidbody != null) {
			Debug.Log (other.attachedRigidbody.gameObject.name);
		}
		other.attachedRigidbody.gameObject.transform.position = destinationGameObject.transform.position;
	}
}
