using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {
	private float speed = 5.0f;
	private int direction = 1;
	private float jumpForce = 400f;
	private float knockbackForce = 10f;

	[SerializeField] private GameObject deadText;

	private bool grounded;
	private Transform groundCheck;
	private Transform ceilingCheck;
	[SerializeField] private LayerMask whatIsGround;
	const float groundedRadius = .2f;

	[SerializeField] private GameObject bullet;
	[SerializeField] private GameObject gunPipe;
	private float fireRate = 0;
	private float timeToFire = 0;

	private Rigidbody2D rigidbody;
	private SpriteRenderer spriteRenderer;

	private void Awake() {
		groundCheck = transform.Find("GroundCheck");
		ceilingCheck = transform.Find("CeilingCheck");
		rigidbody = GetComponent<Rigidbody2D> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();
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

		var yAngle = direction < 0 ? 180 : 0;
		transform.eulerAngles = new Vector3 (0, yAngle, 0);

		if (grounded && Input.GetKey (KeyCode.A)) {
			grounded = false;
			rigidbody.AddForce(new Vector2(0f, jumpForce));
		}

		if (fireRate == 0) {
			if (Input.GetKeyDown (KeyCode.S)) {
				Fire ();
			}
		} else {
			if (Input.GetKey (KeyCode.S) && Time.time > timeToFire) {
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

			// Determine which side the enemy is coming from
			bool isFromRight = coll.collider.attachedRigidbody.transform.position.x >= rigidbody.transform.position.x;
			if (isFromRight) {
				rigidbody.AddForce (new Vector2 (-knockbackForce, 5f), ForceMode2D.Impulse);
			} else {
				rigidbody.AddForce (new Vector2 (knockbackForce, 5f), ForceMode2D.Impulse);
			}

			// Die ();
		}
	}

	private void Fire() {
		Instantiate (bullet, gunPipe.transform.position, gunPipe.transform.rotation);
	}

	private void Die() {
		deadText.SetActive (true);
	}
}
