using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {
	int currentScore;
	public Text scoreDisplay;
	int pointsForBestTime=1000;
	int enemiesDestroyed=0;


	public void AddPoints(int amnt,bool enemy=false){
		currentScore+=amnt;
		if(enemy){
			enemiesDestroyed++;
		}
	}
/// <summary>
/// 
/// </summary>
/// <param name="bestTime">In Seconds</param>
/// <returns></returns>
	public int CalculateFinalScore(int bestTime){
		int t = currentScore+enemiesDestroyed*10;
		int levelTime = Mathf.RoundToInt(Time.timeSinceLevelLoad);
		if(levelTime<bestTime){
			levelTime=bestTime;
		}
		t += pointsForBestTime*bestTime/levelTime;
		return t;
	}
	

}
