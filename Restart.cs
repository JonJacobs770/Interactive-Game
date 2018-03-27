using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour {

	public void OnMouseDown () {
		//Application.LoadLevel (Application.loadedLevel);
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}
}
