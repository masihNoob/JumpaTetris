using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

	[SerializeField]
	private int score = 0, scoreOneLine=40, scoreTwoLine=100, scoreThreeLine=300, scoreFourLine=1200;
	public int lengthDestroy=0;

	// Use this for initialization
	public int ShowPoint () {
		return score;
	}
	
	// Update is called once per frame
	public void UpdateScore () {
		switch (lengthDestroy)
		{
			case 1:
				score += scoreOneLine;
			break;
			case 2:
				score += scoreTwoLine;
			break;
			case 3:
				score += scoreThreeLine;
			break;
			case 4:
				score += scoreFourLine;
			break;
		}
		lengthDestroy =0;
	}
}
