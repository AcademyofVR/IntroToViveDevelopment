using UnityEngine;
using System.Collections;

public class TargetController : MonoBehaviour
{
	public Transform targetParentTransform;
	public GameObject floatingScorePrefab;
	public float targetMoveSpeed = 1;
	public float[] targetXPositions;

	protected float currentTargetX;
	protected bool moveForward;

	public void Awake()
	{
		VRIO_SlideLever.OnLeverSnap += MoveTargetToPosition;
		VRIO_PullLever.OnLeverPull += TurnOnMoveForward;
		VRIO_PullLever.OnLeverRelease += TurnOffMoveForward;
		currentTargetX = targetParentTransform.position.x;
	}

	public void Update()
	{
		if (targetParentTransform.position.x != currentTargetX)
		{
			float moveX = Mathf.MoveTowards(targetParentTransform.position.x, currentTargetX, targetMoveSpeed * Time.deltaTime);
			targetParentTransform.position = new Vector3(moveX, targetParentTransform.position.y, targetParentTransform.position.z);
		}
	}

	public void OnCollisionEnter(Collision collision)
	{
		ContactPoint contact = collision.contacts[0];
		GameObject score = Instantiate(floatingScorePrefab) as GameObject;
		score.transform.position = contact.point;

		float points;
		float distance = Vector3.Distance(transform.position, contact.point);
		if (distance < 0.15f)
			points = 50;
		else if (distance < 0.3f)
			points = 20;
		else if (distance < 0.45f)
			points = 5;
		else
			points = 0;

		TextMesh text = score.GetComponent<TextMesh>();
		text.text = points.ToString();

		StartCoroutine(ScoreFlyAway(text));
	}

	protected IEnumerator ScoreFlyAway(TextMesh text)
	{
		float time = 0;
		while (time < 1)
		{
			Vector3 position = text.gameObject.transform.position;
			position.y += Time.deltaTime;
			text.gameObject.transform.position = position;

			Color color = text.color;
			color.a = 1 - time;
			text.color = color;

			time += Time.deltaTime;
			yield return null;
		}
		Destroy(text.gameObject);
	}

	public void MoveTargetToPosition(int index)
	{
		if (index < targetXPositions.Length)
		{
			currentTargetX = targetXPositions[index];
		}
	}

	public void TurnOnMoveForward()
	{
		currentTargetX = targetXPositions[targetXPositions.Length - 1];
	}

	public void TurnOffMoveForward()
	{
		currentTargetX = targetParentTransform.position.x;
	}
}
