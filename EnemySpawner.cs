using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemyPrefab;
	public float width = 10f;
	public float height = 5f;
	public float speed = 5f;
	
	private bool movingRight = true;
	private float xMax;
	private float xMin;	

	// Use this for initialization
	void Start () {
		CameraSpace();
		CreateEnemy();
	}
	
	// Update is called once per frame
	void Update () {
		FormationMovement();
	}
	
	void CreateEnemy(){
		foreach (Transform child in transform){
			GameObject enemy = Instantiate(enemyPrefab, child.transform.position , Quaternion.identity) as GameObject;
			enemy.transform.parent = child;
		}
	}
	
	void CameraSpace(){
		float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanceToCamera));
		Vector3 rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distanceToCamera));
		xMax = rightEdge.x;
		xMin = leftEdge.x;
	}
	
	public void OnDrawGizmos(){
		Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0));
	}
	
	void FormationMovement (){
		if(movingRight){
			transform.position += new Vector3(speed*Time.deltaTime, 0, 0);
		}else{
			transform.position += new Vector3(-speed*Time.deltaTime, 0, 0);
		}
		
		float rightEdgeOfFormation = transform.position.x + (0.5f*width);
		float leftEdgeOfFormation = transform.position.x - (0.5f*width);
		if(leftEdgeOfFormation < xMin){
			movingRight = true;
		} else if(rightEdgeOfFormation > xMax){
			movingRight = false;
		}
	}
}
