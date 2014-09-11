using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrawPen : MonoBehaviour {

	private LineRenderer line;
	private bool isMousePressed;
	private List<Vector3> pointsList;
	private Vector3 mousePos;
	public GameObject prefab;
	public GameObject parentPrefab;
	private GameObject parent;
	private float lastZ;
	private float lastY;
	private bool firstPos;
	
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
		firstPos = true;
		lastY = -1000000f;
		lastZ = 1000000f;
	}
	//    -----------------------------------    
	void Update () 
	{
		// If mouse button down, remove old line and set its color to green
		if(Input.GetMouseButtonDown(0))
		{
			isMousePressed = true;
			line.SetVertexCount(0);
			pointsList.RemoveRange(0,pointsList.Count);
			line.SetColors(Color.green, Color.green);
			parent = Instantiate(parentPrefab, new Vector3(0,0,0),Quaternion.identity) as GameObject;


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
				mousePos.x = mousePos.x + 1f;
				if(firstPos || Mathf.Abs(lastY - mousePos.y) >= .15f || Mathf.Abs(lastZ - mousePos.z) >= .15f){
					GameObject newCube = Instantiate(prefab, mousePos, Quaternion.identity) as GameObject;
					newCube.transform.parent = parent.transform;
					lastY = mousePos.y;
					lastZ = mousePos.z;
				}
			}
			Debug.Log (mousePos);
			firstPos = false;
		}

		if (Input.GetKey (KeyCode.Space)) {
			parent.rigidbody.AddForce(new Vector3(0,-5,0));
			parent.rigidbody.isKinematic = false;
			Debug.Log ("In");
		}

	}

}
