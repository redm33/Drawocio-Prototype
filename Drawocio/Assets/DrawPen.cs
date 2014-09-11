using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrawPen : MonoBehaviour {

	private LineRenderer line;
	private bool isMousePressed;
	private List<Vector3> pointsList;
	private Vector3 mousePos;
	public GameObject prefab;
	public GameObject ink;
	private float lastX;
	private float lastY;
	private float firstX;
	private float firstY;
	private bool firstPos;
	private List<GameObject> subChunks;
	
	// Structure for line points
	struct myLine
	{
		public Vector3 StartPoint;
		public Vector3 EndPoint;
	};
	//    -----------------------------------    
	void Awake()
	{
		// Create line renderer component and set its property
		line = gameObject.AddComponent<LineRenderer>();
		line.material =  new Material(Shader.Find("Particles/Additive"));
		line.SetVertexCount(0);
		line.SetWidth(.1f,.1f);
		line.SetColors(Color.green, Color.green);
		line.useWorldSpace = true;    
		isMousePressed = false;
		pointsList = new List<Vector3>();
		subChunks = new List<GameObject> ();
		firstPos = true;
		lastY = -1000000f;
		lastX = 1000000f;
	}
	//    -----------------------------------    
	void Update () 
	{
		// If mouse button down, remove old line and set its color to green
		Pen ();
		Pencil ();

	}
	void Pencil()
	{
		if(Input.GetKey(KeyCode.L))
		{
			if(Input.GetMouseButtonDown(0))
			{
				isMousePressed = true;
			}
			else if(Input.GetMouseButtonUp(0))
			{
				isMousePressed = false;
				firstPos = true;
			}
			// Drawing line when mouse is moving(presses)
			if(isMousePressed)
			{
				RaycastHit hit;
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				if (gameObject.collider.Raycast (ray, out hit, Mathf.Infinity)) {
					mousePos = hit.point;
					mousePos.z = mousePos.z + 1f;
					if(firstPos)
					{
						firstX = mousePos.x;
						firstY = mousePos.y;
					}
					if(firstPos || Mathf.Abs(lastY - mousePos.y) >= .15f || Mathf.Abs(lastX - mousePos.x) >= .15f){
						GameObject newCube = Instantiate(prefab, mousePos, Quaternion.identity) as GameObject;
						subChunks.Add(newCube);
						lastY = mousePos.y;
						lastX = mousePos.z;
					}
				}
				firstPos = false;
			}
			
			if (Input.GetKey (KeyCode.Space)) {
				for(int i = 0; i < subChunks.ToArray().Length-1; i++)
				{
					ConfigurableJoint joint = subChunks[i].AddComponent<ConfigurableJoint>();
					joint.xMotion = ConfigurableJointMotion.Locked;
					joint.yMotion = ConfigurableJointMotion.Locked;
					joint.zMotion = ConfigurableJointMotion.Locked;
					joint.angularXMotion = ConfigurableJointMotion.Locked;
					joint.angularYMotion = ConfigurableJointMotion.Locked;
					joint.angularZMotion = ConfigurableJointMotion.Locked;
					joint.targetPosition = new Vector3(0, 0, 0);
					joint.connectedBody = subChunks[i+1].rigidbody;
					subChunks[i].rigidbody.useGravity = true;
					subChunks[i].rigidbody.isKinematic = false;
				}
				if(Mathf.Abs(firstX-lastX) <= 3 && Mathf.Abs(firstY-lastY) <= 3)
				{
					ConfigurableJoint joint = subChunks[subChunks.ToArray().Length - 1].AddComponent<ConfigurableJoint>();
					joint.xMotion = ConfigurableJointMotion.Locked;
					joint.yMotion = ConfigurableJointMotion.Locked;
					joint.zMotion = ConfigurableJointMotion.Locked;
					joint.angularXMotion = ConfigurableJointMotion.Locked;
					joint.angularYMotion = ConfigurableJointMotion.Locked;
					joint.angularZMotion = ConfigurableJointMotion.Locked;
					joint.targetPosition = new Vector3(0, 0, 0);
					joint.connectedBody = subChunks[0].rigidbody;
				}
				subChunks[subChunks.ToArray().Length-1].rigidbody.useGravity = true;
				subChunks[subChunks.ToArray().Length-1].rigidbody.isKinematic = false;
				subChunks = new List<GameObject>();
				
				
			}
		}

	}
	void Pen()
	{
		if(Input.GetKey(KeyCode.I))
		{
			if(Input.GetMouseButtonDown(0))
			{
				isMousePressed = true;
			}
			else if(Input.GetMouseButtonUp(0))
			{
				isMousePressed = false;
				firstPos = true;
			}
			// Drawing line when mouse is moving(presses)
			if(isMousePressed)
			{
				RaycastHit hit;
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				if (gameObject.collider.Raycast (ray, out hit, Mathf.Infinity)) {
					mousePos = hit.point;
					mousePos.z = mousePos.z + 1f;
					if(firstPos)
					{
						firstX = mousePos.x;
						firstY = mousePos.y;
					}
					if(firstPos || Mathf.Abs(lastY - mousePos.y) >= 1f || Mathf.Abs(lastX - mousePos.x) >= 1f){
						GameObject newCube = Instantiate(ink, mousePos, Quaternion.identity) as GameObject;
						subChunks.Add(newCube);
						lastY = mousePos.y;
						lastX = mousePos.z;
					}
				}
				firstPos = false;
			}
			
			if (Input.GetKey (KeyCode.Space)) {
				for(int i = 0; i < subChunks.ToArray().Length-1; i++)
				{
					subChunks[i+1].transform.parent = subChunks[i].transform;
					subChunks[i].rigidbody.useGravity = true;
					subChunks[i].rigidbody.isKinematic = false;
				}


				subChunks[subChunks.ToArray().Length-1].rigidbody.useGravity = true;
				subChunks[subChunks.ToArray().Length-1].rigidbody.isKinematic = false;
				subChunks = new List<GameObject>();


			}
		}
	}

}
