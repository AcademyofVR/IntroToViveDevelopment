using UnityEngine;

public class VRIO_PullLever : MonoBehaviour
{
	[Header("Lever")]
	public Transform lever;
	public float triggerXPosition;
	public float triggerThreshold = 0.02f;

	protected bool pulled = false;

	public delegate void PullLeverEvent();
	public static event PullLeverEvent OnLeverPull;

	public delegate void ReleaseLeverEvent();
	public static event ReleaseLeverEvent OnLeverRelease;

	public void Update()
	{
		//If lever has not been "pulled" and is in the threshold distance of pulled position.
		if (!pulled && Mathf.Abs(lever.localPosition.x - triggerXPosition) < triggerThreshold)
		{
			//Set pulled to true and fire event
			pulled = true;

			if (OnLeverPull != null)
				OnLeverPull();
		}

		//If lever has been "pulled" and lever leaves threshold distance of pulled position
		if (pulled && Mathf.Abs(lever.localPosition.x - triggerXPosition) > triggerThreshold)
		{
			//Set pulled to false and fire event
			pulled = false;

			if (OnLeverRelease != null)
				OnLeverRelease();
		}
	}
}
