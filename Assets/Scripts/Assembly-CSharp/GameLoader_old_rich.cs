using System.Collections;
using UnityEngine;

public class GameLoader_old_rich : MonoBehaviour
{
	public void Start()
	{
		StartCoroutine(LoadGame());
	}

	public IEnumerator LoadGame()
	{
		yield return Application.LoadLevelAsync("Game");
	}
}
