using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour {
	[SerializeField] private GameObject destinationGameObject;

	void OnTriggerEnter2D(Collider2D other) {
		other.attachedRigidbody.gameObject.transform.position = destinationGameObject.transform.position;
	}
}
