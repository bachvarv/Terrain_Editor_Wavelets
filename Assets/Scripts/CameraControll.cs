using UnityEngine;
using System.Collections;

public class CameraControll : MonoBehaviour {

    CameraOrbit co;
	
    //Translate Variables
    Vector3 dragOrigin;
    public float dragSpeed = 15.0f;
	bool drag = false;

    //Rotation Variables
    //Vector3 vDown;
    Vector3 localRotation;
    public float cameraDist = -10f;
    float rotSensitivity = 5f;
    float scrollSensitivity = 2f;
    float rotDampening = 10f;
    float scrollDampening = 6f;

    Vector3 rotationAxis;
    float angularVelocity;
    bool rot = false;
    bool dragging = false;
    
    
    
    public Transform tile;
	float x;
	float y;
	float z;
    Vector3 offset;
	// Use yawn this for initialization
	void Start () {
        co = GetComponent<CameraOrbit>();
        if (tile != null)
        {
            //transform.LookAt(tile);
        }
        else
        {
            tile = GameObject.Find("TileMap").transform;
        }

        if (tile != null)
        {
            TileMap comp = tile.GetComponent<TileMap>();
            offset = new Vector3(comp.size_x / 2.0f, 0f, comp.size_x / 2.0f);
            transform.parent.position = offset;
            //transform.LookAt(tile.position + offset);
        }
	}
	
	// Update is called once per frame
	void LateUpdate () {
        if (!Input.GetKey(KeyCode.LeftAlt) && !Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetMouseButtonDown(0))
            {
                dragOrigin = Input.mousePosition;
                drag = true;
                return;
            }
            else if (Input.GetMouseButton(1))
            {
                if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
                {
                    localRotation.x += Input.GetAxis("Mouse X") * rotSensitivity;
                    localRotation.y -= Input.GetAxis("Mouse Y") * rotSensitivity;

                    localRotation.y = Mathf.Clamp(localRotation.y, 0f, 90f);
                    rot = true;
                }
            }

            if (Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                float scrollAmount = Input.GetAxis("Mouse ScrollWheel") * scrollSensitivity;

                scrollAmount *= (cameraDist * 0.3f);
                cameraDist += scrollAmount * -1f;
                cameraDist = Mathf.Clamp(cameraDist, -100, -1.5f);
            }

            Quaternion QT = Quaternion.Euler(localRotation.y, localRotation.x, 0f);
            transform.parent.rotation = Quaternion.Lerp(transform.parent.rotation, QT, Time.deltaTime * rotDampening);

            if (transform.position.z != (cameraDist * -1f))
            {
                transform.localPosition = new Vector3(0f, 0f, Mathf.Lerp(transform.localPosition.z, cameraDist, Time.deltaTime * scrollDampening));
            }

            if (!Input.GetMouseButton(0) && drag)
            {
                drag = false;
                return;
            }

            if (drag)
                OnMouseDragPos();
        }
	}

	void OnMouseDragPos()
	{
		Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
		Vector3 move = new Vector3(-pos.x * dragSpeed, -pos.y * dragSpeed, pos.z * dragSpeed);

		transform.parent.Translate(move, Space.Self);

		dragOrigin = Input.mousePosition;
	}
}
