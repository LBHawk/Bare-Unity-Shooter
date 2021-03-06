﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public EnemyManager[] enemyManagers;
	//public bool[] spawningDone;
	//int coroutinesDone;
	public int waveNumber;
	public int timeBetweenWaves;
	bool waveComplete;
	bool waiting;
	public Text waveText;
	public float textFlashSpeed;

	// Init
	void Awake () {
		// Set wave number in editor (for easy testing)
		//waveNumber = 0;
		waveComplete = true;
		waiting = false;
		//waveText = GameObject.Find("WaveText").GetComponent<Text>();
	}
	
	// Check if wave is done, then start a new wave if it is
	void Update () {
		// Don't update wave state if we're between waves, otherwise bad things happen
		if(!waiting)
		{
			WaveCheck();
		}
		
		if(waveComplete)
		{
			//StartWave();
			waveComplete = false;
			waiting = true;
			//StartCoroutine(FlashText());
			StartCoroutine(WaveReady());
		}
	}

	// If every enemyManager has returned, and no enemies are alive, the wave is complete
	// I.e. return if an em is spawning or an enemy is alive
	void WaveCheck(){
		Debug.Log("Checkin wave");
		for(int i = 0; i < enemyManagers.Length; i++)
		{
			if(!enemyManagers[i].spawningDone)
			{
				Debug.Log("Spawning not done");
				return;
			}
		}

		if(GameObject.FindGameObjectWithTag("Enemy") != null)
		{
			Debug.Log("Found enemy");
			return;
		}

		// Do this so we can wait a few seconds before new wave
		StartCoroutine(FlashText());
		waveComplete = true;
	}

	// Start up enememanagers to spawn enemies
	void StartWave(){
		for (int i = 0; i < enemyManagers.Length; i++){
			Debug.Log("Starting coroutine");
			StartCoroutine(enemyManagers[i].Spawner(waveNumber));
		}
	}

	// Pause between waves
	IEnumerator WaveReady(){
		yield return new WaitForSeconds(timeBetweenWaves / 2);
		waveNumber++;
		yield return new WaitForSeconds(timeBetweenWaves / 2);
		//waveComplete = true;
		StartWave();

		// Safety mech to ensure wave isnt skipped
		yield return new WaitForSeconds(3);
		waiting = false;
		yield break;
	}

	IEnumerator FlashText(){
		float r = waveText.color.r;
		float g = waveText.color.g;
		float b = waveText.color.b;

		float startTime = Time.time;
		for(int i = 0; i < 310; i++)
		{
			waveText.color = new Color(r,g,b,(System.Single)(Mathf.Sin((Time.time - startTime + 2.0f) * textFlashSpeed) + 1.0)/2.0f);
			waveText.material.color = Color.white;
			yield return null;
		}

		waveText.color = new Color(r,g,b,1.0f);

		yield break;
	}
}
