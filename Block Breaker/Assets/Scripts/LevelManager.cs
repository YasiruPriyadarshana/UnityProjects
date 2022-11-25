using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	public void LoadLevel(string name)
	{
		Debug.Log("Level load requested for name : "+name);
		Brick.breakableCount = 0;
		Application.LoadLevel(name);
	}

	public void QuitRequest()
	{
		Debug.Log("I Want To Quit!!");
		Application.Quit();
	}

	public void LoadNextLevel ()
	{
		Brick.breakableCount = 0;
		Application.LoadLevel(Application.loadedLevel + 1);
	}

	public void BrickDestroyed ()
	{
		if(Brick.breakableCount <= 0){
			LoadNextLevel();
		}
	}
}
