using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {

	public bool autoPaly=false;
	private Ball ball;
	private float minX=1.5f,maxX=14.5f;

	// Use this for initialization
	void Start () {
		ball = GameObject.FindObjectOfType<Ball>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!autoPaly) {
			MoveWithMouse ();
		} else {
			AutoPlay();
		}
	}

	void MoveWithMouse ()
	{
		Vector3 paddlePos=new Vector3(0.5f,this.transform.position.y,0f);
		float mousePosInBlocks = Input.mousePosition.x / Screen.width * 16;
		paddlePos.x=Mathf.Clamp(mousePosInBlocks,minX , maxX);

		this.transform.position = paddlePos;
	}

	void AutoPlay(){
		Vector3 paddlePos=new Vector3(0.5f,this.transform.position.y,0f);
		Vector3 ballPoss = ball.transform.position;
		paddlePos.x=Mathf.Clamp(ballPoss.x, minX, maxX);
		this.transform.position = paddlePos;
	}
}
