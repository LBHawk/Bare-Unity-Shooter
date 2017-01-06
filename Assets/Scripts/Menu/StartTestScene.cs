using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartTestScene : MonoBehaviour {

	public void TestScene(string levelName){
		SceneManager.LoadScene(levelName);
		return;
	}
}