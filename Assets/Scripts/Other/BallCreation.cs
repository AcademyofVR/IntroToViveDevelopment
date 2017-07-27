using UnityEngine;

public class BallCreation : MonoBehaviour
{
	protected AudioSource audioSource;

	void Awake()
	{
		BallButton.OnBallButtonPress += SpawnBall;
		audioSource = GetComponent<AudioSource>();
	}

	protected void SpawnBall(GameObject ballPrefab)
	{
		Instantiate(ballPrefab, transform.position, Quaternion.identity);
		audioSource.Play();
	}
}
