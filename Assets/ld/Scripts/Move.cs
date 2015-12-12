using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {
	[SerializeField] private GameObject startingPosition;

	public float speed = 10.0f;
	public int direction = 1;
	public float jumpForce = 400f;

	[SerializeField] private GameObject deadText;

	private bool grounded;
	private Transform groundCheck;
	private Transform ceilingCheck;
	[SerializeField] private LayerMask whatIsGround;
	const float groundedRadius = .2f;

	[SerializeField] private GameObject bullet;
	[SerializeField] private GameObject gunPipe;
	private float fireRate = 0;
	float timeToFire = 0;

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

		if (fireRate == 0) {
			if (Input.GetButtonDown ("Fire1")) {
				Fire ();
			}
		} else {
			if (Input.GetButton ("Fire1") && Time.time > timeToFire) {
				timeToFire = Time.time + 1 / fireRate;
				Fire ();
			}
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

	private void Fire() {
		Instantiate (bullet, gunPipe.transform.position, Quaternion.identity);
	}

	private void Die() {
		deadText.SetActive (true);
	}
}
