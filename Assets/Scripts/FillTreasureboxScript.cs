using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillTreasureboxScript : MonoBehaviour {

	[SerializeField]
    private int numberOfSpheres = 10;

    // Use this for initialization
    void Start () {
		for (int i = 0; i < numberOfSpheres; i++)
		{
			GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			sphere.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
			sphere.transform.position = Vector3.zero + Random.insideUnitSphere * 0.5f + new Vector3(0, 1, 0);
			sphere.AddComponent<Rigidbody>().useGravity = true;
		}

		// Spawn Special Price
		GameObject playerAsPrice = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		playerAsPrice.name = "PLAYER";
		playerAsPrice.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		playerAsPrice.transform.position = Vector3.zero + Random.insideUnitSphere * 0.5f + new Vector3(0, 0.5f, 0);
		playerAsPrice.AddComponent<SphereCollider>();
		playerAsPrice.AddComponent<Rigidbody>().useGravity = true;
		playerAsPrice.AddComponent<GetPickedUpByTracker>();
		playerAsPrice.AddComponent<ConstantForce>();
		playerAsPrice.GetComponent<MeshRenderer>().material.color = Color.cyan;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
