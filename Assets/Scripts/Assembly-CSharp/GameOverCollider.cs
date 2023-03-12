using UnityEngine;

public class GameOverCollider : MonoBehaviour
{
	private void OnCollisionEnter(Collision collision)
	{
		GameManager.Instance.EndGameMode();
	}
}
