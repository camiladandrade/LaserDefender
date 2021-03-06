﻿using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {

	public float health = 150f;
	public GameObject projectile;
	public float projectileSpeed = 10f;
	public float shotsPerSecond = 0.5f;
	public int scoreValue = 150;
	public AudioClip fireSound;
	public AudioClip deathSound;
	
	private ScoreKeeper scoreKeeper;
	
	void Start(){
		scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
	}
	
	void Update(){
		float probability = Time.deltaTime * shotsPerSecond;
		if(Random.value < probability){ 
			Fire ();
		}
	}
	
	void Fire(){
		Vector3 startPosition = transform.position + new Vector3(0, -1, 0);
		GameObject missile = Instantiate(projectile, startPosition, Quaternion.identity) as GameObject;
		missile.rigidbody2D.velocity = new Vector2 (0, -projectileSpeed);
		AudioSource.PlayClipAtPoint(fireSound, transform.position);
	}

	void OnTriggerEnter2D (Collider2D col){
		Projectile missile = col.gameObject.GetComponent<Projectile>();
		if(missile){
			health -= missile.GetDamage();
			missile.Hit();
			if(health <= 0){
				AudioSource.PlayClipAtPoint(deathSound, transform.position);
				Destroy(gameObject);
				scoreKeeper.Score(scoreValue);
			}
		}
	}
}
