using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveTextManager : MonoBehaviour {
	public GameManager gm;
	public static int wave;
	Text text;

	// Use this for initialization
	void Awake () {
		text = GetComponent<Text>();
		wave = 0;
	}
	
	// Update is called once per frame
	void Update () {
		wave = gm.waveNumber;
		text.text = "Wave " + wave;
	}
}
