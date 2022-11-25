using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour {

	public AudioClip crack;
	public Sprite[] hitSprite;
	public static int breakableCount;
	public GameObject smoke;

	private int maxHits;
	private int timesHit;
	private bool isBreakable;
	private LevelManager levelManager;

	// Use this for initialization
	void Start ()
	{
		isBreakable = (this.tag == "Breakable");
		if (isBreakable) {
			breakableCount++;
			print(breakableCount);
		}
		timesHit = 0;
		levelManager = GameObject.FindObjectOfType<LevelManager>();
		maxHits =hitSprite.Length + 1;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D (Collision2D collision)
	{
		AudioSource.PlayClipAtPoint(crack, transform.position);
		if(isBreakable){
			HandleHits();
		}
	}

	void HandleHits ()
	{
		timesHit++;
		if (timesHit >= maxHits) {
			breakableCount--;
			levelManager.BrickDestroyed();
			PuffSmoke();
			Destroy (gameObject);
		} else {
			LoadSprites();
		}
	}

	void PuffSmoke ()
	{
		GameObject smokepuff= Instantiate(smoke, transform.position, Quaternion.identity) as GameObject;
		smokepuff.GetComponent<ParticleSystem>().startColor = gameObject.GetComponent<SpriteRenderer>().color;
	}

	void LoadSprites ()
	{
		int spriteIndex = timesHit - 1;
		if (hitSprite [spriteIndex]) {
			this.GetComponent<SpriteRenderer> ().sprite = hitSprite [spriteIndex];
		} else {
			print("Sprite missing");
		}

	}

	//TODO remove this finction after we actually win
	void SimulateWin ()
	{
		levelManager.LoadNextLevel();
	}
}
