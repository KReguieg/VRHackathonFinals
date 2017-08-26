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
			sphere.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f) * Random.Range(1.0f, 2.5f);
			sphere.transform.position = Vector3.zero + Random.insideUnitSphere * 0.5f + new Vector3(0, 1, 0);
			sphere.GetComponent<SphereCollider>().enabled = false;
			sphere.AddComponent<Rigidbody>().useGravity = true;
			sphere.GetComponent<SphereCollider>().enabled = true;
		}

		// Spawn Special Price
		GameObject playerAsPrice = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		playerAsPrice.name = "PLAYER";
		playerAsPrice.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
		playerAsPrice.transform.position = Vector3.zero + Random.insideUnitSphere * 0.5f + new Vector3(0, 0.5f, 0);
		playerAsPrice.AddComponent<Rigidbody>().useGravity = true;
		playerAsPrice.AddComponent<GetPickedUpByTracker>();
		playerAsPrice.AddComponent<ConstantForce>();
		playerAsPrice.GetComponent<MeshRenderer>().material.color = Color.cyan;
	}
}
