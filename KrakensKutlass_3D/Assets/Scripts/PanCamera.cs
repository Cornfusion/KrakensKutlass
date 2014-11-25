using UnityEngine;
using System.Collections;

public class PanCamera : MonoBehaviour {

	public Transform boundaryTop;
	public Transform boundaryBottom;
	public Transform boundaryLeft;
	public Transform boundaryRight;
	public Transform boundaryZoomIn;
	public Transform boundaryZoomOut;

	public float mouseSensitivity = 1.0f;
	public float scrollSpeed = 1.0f;
	public float zoomSpeed = 1.0f;
	private Vector3 lastPosition;

	private Vector3 originPosition = new Vector3(0, 0, 0);
	private Vector3 tempPos;

	private int screenBorder = 2;

	// Update is called once per frame
	void Update () 
	{

		MouseClickDragPan ();
		ScreenEdgePan ();
		CameraZoom ();
		LimitCameraPan ();

	}

	void CameraZoom()
	{
		transform.Translate(Vector3.forward * Input.GetAxis("Mouse ScrollWheel") * zoomSpeed * Time.deltaTime);
	}

	void LimitCameraPan()
	{
		if(transform.position.x < boundaryLeft.position.x)
		{
			tempPos = transform.position;
			tempPos.x = boundaryLeft.position.x;
			transform.position = tempPos;
		}

		else if(transform.position.x > boundaryRight.position.x)
		{
			tempPos = transform.position;
			tempPos.x = boundaryRight.position.x;
			transform.position = tempPos;
		}

		if(transform.position.z > boundaryTop.position.z)
		{
			tempPos = transform.position;
			tempPos.z = boundaryTop.position.z;
			transform.position = tempPos;
		}
		else if(transform.position.z < boundaryBottom.position.z)
		{
			tempPos = transform.position;
			tempPos.z = boundaryBottom.position.z;
			transform.position = tempPos;
		}

		if(transform.position.y < boundaryZoomIn.position.y)
		{
			tempPos = transform.position;
			tempPos.y = boundaryZoomIn.position.y;
			transform.position = tempPos;
		}
		else if(transform.position.y > boundaryZoomOut.position.y)
		{
			tempPos = transform.position;
			tempPos.y = boundaryZoomOut.position.y;
			transform.position = tempPos;
		}

	}

	void MouseClickDragPan()
	{
		//Right click and drag to pan
		if (Input.GetMouseButtonDown(1))
		{
			lastPosition = Input.mousePosition;
		}
		
		if (Input.GetMouseButton(1))
		{
			Vector3 delta = Input.mousePosition - lastPosition;
			transform.Translate(delta.x * mouseSensitivity * Time.deltaTime, 0, delta.y * mouseSensitivity * Time.deltaTime);
			lastPosition = Input.mousePosition;
		}
	}

	void ScreenEdgePan()
	{
		if(Input.mousePosition.x > Screen.width - screenBorder)
		{
			tempPos = transform.position;
			tempPos.x += scrollSpeed * Time.deltaTime;
			transform.position = tempPos;
		}

		else if(Input.mousePosition.x < 0 + screenBorder)
		{
			tempPos = transform.position;
			tempPos.x -= scrollSpeed * Time.deltaTime;
			transform.position = tempPos;
		}

		if(Input.mousePosition.y < 0 + screenBorder)
		{
			tempPos = transform.position;
			tempPos.z -= scrollSpeed * Time.deltaTime;
			transform.position = tempPos;
		}

		else if(Input.mousePosition.y > Screen.height - screenBorder)
		{
			tempPos = transform.position;
			tempPos.z += scrollSpeed * Time.deltaTime;
			transform.position = tempPos;
		}
	}
}
