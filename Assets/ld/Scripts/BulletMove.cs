using UnityEngine;
using System.Collections;

public class BulletMove : MonoBehaviour {
	private float speed = 1f;
	private float knockbackForce = 4f;

	void Start () {
	}

	void Update () {
		transform.position += transform.forward * speed;
	}

	void OnCollisionEnter2D (Collision2D coll) {
		if (coll.gameObject.tag == "Enemy") {
			coll.gameObject.SendMessage ("ApplyDamage", 10);

			bool isFromRight = coll.collider.attachedRigidbody.transform.position.x >= transform.position.x;
			if (isFromRight) {
				coll.collider.attachedRigidbody.AddForce (new Vector2 (-knockbackForce, 0), ForceMode2D.Impulse);
			} else {
				coll.collider.attachedRigidbody.AddForce (new Vector2 (knockbackForce, 0), ForceMode2D.Impulse);
			}
		}
		if (coll.gameObject.tag != "Player") {
			Destroy (gameObject);
		}
	}
}
