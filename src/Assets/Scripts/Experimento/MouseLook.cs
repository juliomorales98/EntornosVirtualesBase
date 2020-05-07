using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
	enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }

	[SerializeField]RotationAxes axes = RotationAxes.MouseXAndY;
	[SerializeField] float sensitivityX = 15;
	[SerializeField] float sensitivityY  = 15;

	[SerializeField] float minimumX = -360;
	[SerializeField] float maximumX = 360;

	[SerializeField] float minimumY = -60;
	[SerializeField] float maximumY = 60;

	[SerializeField] float rotationX = 0;
	[SerializeField] float rotationY  = 0;

	Quaternion originalRotation;



	void Update()
	{
		/*if (!Screen.lockCursor)
		{
			//return;
		}*/

		Quaternion yQuaternion;
		Quaternion xQuaternion;
		if (axes == RotationAxes.MouseXAndY)
		{
			// Read the mouse input axis
			rotationX += Input.GetAxis("Mouse X") * sensitivityX;
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;

			rotationX = Mathf.Clamp(rotationX, minimumX, maximumX);
			rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

			xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
			yQuaternion = Quaternion.AngleAxis(rotationY, Vector3.left);
			transform.localRotation = originalRotation * xQuaternion * yQuaternion;
		}
		else if (axes == RotationAxes.MouseX)
		{
			rotationX += Input.GetAxis("Mouse X") * sensitivityX;
			rotationX = Mathf.Clamp(rotationX, minimumX, maximumX);

			xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
			transform.localRotation = originalRotation * xQuaternion;
		}
		else
		{
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
			rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

			yQuaternion = Quaternion.AngleAxis(rotationY, Vector3.left);
			transform.localRotation = originalRotation * yQuaternion;
		}
	}

	void Start()
	{
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
		// Make the rigid body not change rotation
		if (GetComponent<Rigidbody>())
		{
			GetComponent<Rigidbody>().freezeRotation = true;
		}
		originalRotation = transform.localRotation;
	}
	/*
	static function Mathf.Clamp(angle : float, min : float, max :  float) : float
{
	if (angle< -360)
		angle += 360;
	if (angle > 360)
		angle -= 360;
	return Mathf.Clamp(angle, min, max);
}*/
}
