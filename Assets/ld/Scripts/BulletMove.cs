using UnityEngine;
using System.Collections;

public class BulletMove : MonoBehaviour {
	private float speed = 1f;

	void Start () {
	}

	void Update () {
		transform.position = new Vector2 (transform.position.x + speed, transform.position.y);
	}

	void OnCollisionEnter2D (Collision2D coll) {
		if (coll.gameObject.tag == "Enemy") {
			coll.gameObject.SendMessage ("ApplyDamage", 10);
		}
		if (coll.gameObject.tag != "Player") {
			Destroy (gameObject);
		}
	}
}
