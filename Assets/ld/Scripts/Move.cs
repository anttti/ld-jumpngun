using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {
	[SerializeField] private GameObject startingPosition;

	public float speed = 10.0f;
	public int direction = 1;
	public float jumpForce = 400f;

	const float groundedRadius = .2f;

	[SerializeField] private GameObject deadText;

	private bool grounded;
	private Transform groundCheck;
	private Transform ceilingCheck;
	[SerializeField] private LayerMask whatIsGround;

	private Rigidbody2D rigidbody;

	private void Awake() {
		groundCheck = transform.Find("GroundCheck");
		ceilingCheck = transform.Find("CeilingCheck");
		rigidbody = GetComponent<Rigidbody2D> ();
	}

	private void FixedUpdate() {
		grounded = false;
		Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, whatIsGround);
		for (int i = 0; i < colliders.Length; i++) {
			if (colliders [i].gameObject != gameObject) {
				grounded = true;
			}
		}
	}

	void Update() {
		var move = new Vector3(1, 0, 0);
		transform.position += move * speed * direction * Time.deltaTime;

		if (grounded && Input.GetKey (KeyCode.Space)) {
			grounded = false;
			rigidbody.AddForce(new Vector2(0f, jumpForce));
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "BounceWall") {
			direction = -direction;
		} else if (coll.gameObject.tag == "Enemy") {
			Debug.Log ("You just kicked the bucket");
			Die ();
		}
	}

	private void Die() {
		deadText.SetActive (true);
	}
}
