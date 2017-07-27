using UnityEngine;

public class VRIO_RaycastExample : VRInteractableObject
{
	public Color headRayColor;
	public Color controllerRayColor;
	public Renderer objectRenderer;

	protected Color originalColor;
	protected Material objectMaterial;

	void Awake()
	{
		//Get object's material from the renderer, and the original color
		objectMaterial = objectRenderer.material;
		originalColor = objectMaterial.color;
	}

	public override void RayEnter(RaycastHit hit, VRControllerInput controller = null)
	{
		//Get desired color and apply to the object's material
		Color c = controller == null ? headRayColor : controllerRayColor;
		objectMaterial.color = c;
	}

	public override void RayExit(VRControllerInput controller = null)
	{
		//Apply object's original color
		objectMaterial.color = originalColor;
	}
}
