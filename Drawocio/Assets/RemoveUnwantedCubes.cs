using UnityEngine;
using System.Collections;

public class RemoveUnwantedCubes : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision col)
	{
		Debug.Log ("hi!");
		Debug.Log (col.gameObject.name);
		if (col.gameObject.name == "TestCube") {
			Destroy (col.gameObject);
			Debug.Log("Destroyed");
		}
	}
}
