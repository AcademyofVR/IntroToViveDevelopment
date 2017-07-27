using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour
{
	protected float lifetime = 0;
	protected float maxLifetime = 3;

	void Update()
	{
		lifetime += Time.deltaTime;
		if (lifetime > maxLifetime)
			Destroy(gameObject);
	}
}
