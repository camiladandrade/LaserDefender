using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed = 15.0f;
	public float paddig = 0.5f;
	
	//float speedTime = speed * Time.deltaTime;
	float xMin;
	float xMax;

	// Use this for initialization
	void Start () {
		SpaceSize();
	}
	
	// Update is called once per frame
	void Update () {
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
}
