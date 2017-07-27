using UnityEngine;

public class BallButton : VRIO_Button
{
	public GameObject ballPrefab;

	public delegate void BallButtonPress(GameObject ballPrefab);
	public static event BallButtonPress OnBallButtonPress;

	protected override void TriggerButtonPress()
	{
		if (OnBallButtonPress != null)
			OnBallButtonPress(ballPrefab);
	}
}
