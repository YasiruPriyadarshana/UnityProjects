using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public float speed = 10.0f;
	public GameObject projectile;
	public float projectileSpeed;
	public float firingRate = 0.2f;
	public float health = 250;

	public AudioClip fireSound;
	public AudioClip sheldSound;

	private float xmin;
	private float xmax;
	private float padding= 0.5f;

	void Start ()
	{
		float distance= transform.position.z - Camera.main.transform.position.z;
		Vector3 leftmost= Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
		Vector3 rightmost= Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
		xmin = leftmost.x + padding;
		xmax = rightmost.x - padding;
	}

	void Fire ()
	{
		GameObject beam= Instantiate(projectile, transform.position , Quaternion.identity) as GameObject;
		beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0 , projectileSpeed, 0);
		AudioSource.PlayClipAtPoint(fireSound, transform.position);
	}

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Space)) {
			InvokeRepeating("Fire",0.000001f,firingRate);
		}
		if (Input.GetKeyUp(KeyCode.Space)) {
			CancelInvoke("Fire");
		}

		if (Input.GetKey(KeyCode.LeftArrow)) {
			transform.position += Vector3.left * speed * Time.deltaTime;
		}else if (Input.GetKey(KeyCode.RightArrow)) {
			transform.position += Vector3.right * speed * Time.deltaTime;
		}

		//restrict player to game space
		float newX = Mathf.Clamp(transform.position.x, xmin, xmax) ;
		transform.position = new Vector3(newX, transform.position.y, transform.position.z);
	}

	void OnTriggerEnter2D (Collider2D collider)
	{
		Projectile missile = collider.gameObject.GetComponent<Projectile> ();
		if (missile) {
			health -= missile.GetDamage();
			missile.Hit();
			AudioSource.PlayClipAtPoint(sheldSound, transform.position);
			if(health <= 0){
				Die();
			}
		}
	}

	void Die ()
	{
		LevelManager man = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		man.LoadLevel("Win Screen");
		Destroy(gameObject);
	}
}
