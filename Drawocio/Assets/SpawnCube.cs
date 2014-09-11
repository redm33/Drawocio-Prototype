using UnityEngine;
using System.Collections;

public class SpawnCube : MonoBehaviour {
		private float mouseposX;
		private Vector3 rayHitWorldPosition;
		public GameObject yourObject;
		public Material drawingMaterial;
		
		void Start ()
		{
			Time.timeScale = 2.0f;
		}
		
		// Update is called once per frame
		void Update ()
		{
			/**GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		cube.transform.position = new Vector3(0f, 0.5F, 0f);
		cube.transform.localScale = new Vector3 (1.25f, 1.5f, 1f);
		Rigidbody gameObjectsRigidBody = cube.AddComponent<Rigidbody>(); // Add the rigidbody.
		gameObjectsRigidBody.mass = 5; // Set the GO's mass to 5 via the Rigidbody.**/
			if (Input.GetKey (KeyCode.Mouse0)) {
				// raycast
				RaycastHit rayHit;
				if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out rayHit)) {
					// where did the raycast hit in the world - position of rayhit
					rayHitWorldPosition = rayHit.point;
					print ("rayHit.point : " + rayHit.point + " (rayHitWorldPosition)");
					mouseposX = rayHit.point.x;
				}
				GameObject cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
				cube.transform.position = new Vector3 (rayHit.point.x, rayHit.point.y, .1f);
				cube.transform.localScale = new Vector3 (.2f, .2f, .1f);
				cube.renderer.material = drawingMaterial;
				//yourObject.transform.position = new Vector3 (mouseposX, 0f, 0f);
			}
		}
	}

