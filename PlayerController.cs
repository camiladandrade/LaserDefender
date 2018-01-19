using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed = 15.0f;
	public float paddig = 2f;
	public GameObject projectile;
	public float projectileSpeed = 5f;
	public float firingRate = 0.2f;
	public float health = 250f;
	
	//float speedTime = speed * Time.deltaTime;
	float xMin;
	float xMax;

	// Use this for initialization
	void Start () {
		SpaceSize();
	}
	
	// Update is called once per frame
	void Update () {
		PlayerLaser ();
		SpaceMovement();
	
	}
	
	void SpaceMovement(){
		if (Input.GetKey(KeyCode.RightArrow)){
			transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
		}else if(Input.GetKey(KeyCode.D)){
			transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
		} else if (Input.GetKey(KeyCode.LeftArrow)) {
			transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
		}else if (Input.GetKey(KeyCode.A)){
			transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
		}
		
		//restrict the player to the gamespace
		float newX = Mathf.Clamp(transform.position.x, xMin, xMax);
		transform.position = new Vector3(newX, transform.position.y, transform.position.z);
	}
	
	void SpaceSize(){
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
		Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
		
		xMin = leftMost.x + paddig;
		xMax = rightMost.x - paddig;
		
	}
	void Fire (){
		Vector3 offset = new Vector3(0, 1, 0);
		GameObject beam = Instantiate(projectile, transform.position+offset, Quaternion.identity) as GameObject;
		beam.rigidbody2D.velocity = new Vector3 (0, projectileSpeed, 0);
	}
	
	void PlayerLaser(){
		if(Input.GetKeyDown(KeyCode.Space)){
			InvokeRepeating("Fire", 0.00001f, firingRate);
		}
		
		if(Input.GetKeyUp(KeyCode.Space)){
			CancelInvoke("Fire");
		}
	}
	
	void OnTriggerEnter2D (Collider2D col){
		Projectile missile = col.gameObject.GetComponent<Projectile>();
		if(missile){
			health -= missile.GetDamage();
			missile.Hit();
			if(health <= 0){
				Destroy(gameObject);
			}
		}
	}
}
