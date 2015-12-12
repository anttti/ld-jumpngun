using UnityEngine;
using System.Collections;

public class EnemyMove : MonoBehaviour {
	private float speed = 5.0f;
	private int direction = 1;
	private int health = 50;

	private Rigidbody2D rigidbody;
	private bool isMoving = false;

	private void Awake() {
		rigidbody = GetComponent<Rigidbody2D> ();
	}

	void Update() {
		if (!isMoving) {
			return;
		}
		var move = new Vector3(1, 0, 0);
		transform.position += move * speed * direction * Time.deltaTime;
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "BounceWall") {
			direction = -direction;
		} else if (coll.gameObject.tag == "Floor") {
			isMoving = true;
		}
	}

	void ApplyDamage(int amount) {
		health -= amount;
		if (health <= 0) {
			Die ();
		}
	}

	private void Die() {
		Destroy (gameObject);
	}
}
