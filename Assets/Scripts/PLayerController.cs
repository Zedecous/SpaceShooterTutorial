using System.Collections;
using UnityEngine;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin,zMax;
}
public class PLayerController : MonoBehaviour {
	public float speed;
	public Rigidbody rb;
	AudioSource shotSound;
	public float tilt;
	public GameObject shot;
	public Transform shotSpawn; 
	public float fireRate;
	private float nextFire;

	public Boundary boundary;
	void Update()
	{
		shotSound = GetComponent<AudioSource> ();
		if (Input.GetButton ("Fire1") && Time.time > nextFire) {
			nextFire = Time.time + fireRate; 
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
			shotSound.Play ();
		}
	}
	void FixedUpdate()
	{
		rb = GetComponent<Rigidbody> ();
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rb.velocity = movement * speed;

		rb.position = new Vector3 (
			Mathf.Clamp(rb.position.x,boundary.xMin,boundary.xMax), 
			0.0f, 
			Mathf.Clamp(rb.position.z,boundary.zMin,boundary.zMax)
		);
		rb.rotation = Quaternion.Euler(0.0f,0.0f, rb.velocity.x * -tilt);
	}
}
