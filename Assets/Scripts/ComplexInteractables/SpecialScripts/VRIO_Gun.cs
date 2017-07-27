using UnityEngine;
using System.Collections;
using Valve.VR;

public class VRIO_Gun : VRInteractableObject
{
	public enum GunTypes { SingleShot, Automatic };

	[Header("Simple Gun Stuff")]
	public EVRButtonId fireButton = EVRButtonId.k_EButton_SteamVR_Trigger;
	public Transform projectileExitPoint;
	public GameObject bulletPrefab;
	public float bulletSpeed = 400;
	public float multishotDelay = 0.2f;

	[Header("Extra Gun Stuff")]
	public GunTypes defaultGunType = GunTypes.SingleShot;
	public Material[] gunTypeMaterials;
	public MeshRenderer[] gunMeshRenderers;
	public AudioSource gunAudioSource;

	protected bool autoFire = false;
	protected float restTimer = 0;

	public void Awake()
	{
		VRIO_Button.OnButtonPress += ToggleGunType;
	}

	public void Update()
	{
		//If gun is set to autofire, and trigger is down
		if (autoFire)
		{
			//If it is time to shoot
			if (restTimer > multishotDelay)
			{
				//Reset timer, and shoot
				restTimer = 0;
				ShootBullet();
			}
			else
			{
				//Add time to the reset timer
				//Delta time is how much time has passed between this and the last frame.
				restTimer += Time.deltaTime;
			}
		}
	}

	public override void ButtonPressDown(EVRButtonId button, VRControllerInput controller)
	{
		//As it is, there is no check for whether the shooter is actually in your hand before you fire.
		//You would have to write in the ability to check if the current object is being held.

		//If button is desired "fire" button
		if (button == fireButton)
		{
			//Shoot
			ShootBullet();

			//Haptic pulse
			controller.device.TriggerHapticPulse(2000);

			//Trigger audio
			gunAudioSource.Play();

			//Turn on autofire, if gun is set to automatic
			if (defaultGunType == GunTypes.Automatic)
				autoFire = true;
		}
	}

	public override void ButtonPressUp(EVRButtonId button, VRControllerInput controller)
	{
		//If button is desired "fire" button
		if (button == fireButton)
		{
			//Set autofire to false
			autoFire = false;

			//Reset autofire timer
			restTimer = 0;
		}
	}

	protected void ShootBullet()
	{
		//Create bullet and set it to muzzle's position and rotation
		GameObject bullet = Instantiate(bulletPrefab);
		bullet.transform.position = projectileExitPoint.position;
		bullet.transform.rotation = projectileExitPoint.rotation;

		//Add force to bullet
		Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
		bulletRigidbody.AddForce(transform.forward * bulletSpeed);
	}

	public void ToggleGunType()
	{
		//Swap gun type and set to matching material
		//Yeah I know. This code is pretty quick and dirty.
		//But this isn't the important part!

		if (defaultGunType == GunTypes.Automatic)
			defaultGunType = GunTypes.SingleShot;
		else
			defaultGunType = GunTypes.Automatic;

		if ((int)defaultGunType < gunTypeMaterials.Length)
		{
			for (int i = 0; i < gunMeshRenderers.Length; i++)
				gunMeshRenderers[i].material = gunTypeMaterials[(int)defaultGunType];
		}
	}
}
