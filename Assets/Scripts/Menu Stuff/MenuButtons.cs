using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
	[SerializeField]
	string sceneSelected;

	public void OnButtonPress()
	{
		StartCoroutine(AsyncSceneLoad());
	}

	IEnumerator AsyncSceneLoad()
	{
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneSelected);

		// Wait until the scene fully loads
		while (!asyncLoad.isDone)
			yield return null;
	}
}
