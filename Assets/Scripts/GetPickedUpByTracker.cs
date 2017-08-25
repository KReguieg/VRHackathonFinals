using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPickedUpByTracker : MonoBehaviour {

		Vector3 force;
		Rigidbody boody;	

	void OnTriggerEnter(Collider col) {
		Debug.Log(col.gameObject.name);
		if(col.name == "Tracker1") {
			col.gameObject.AddComponent<SpringJoint>();
			col.gameObject.GetComponent<Rigidbody>().isKinematic = true;
			col.gameObject.GetComponent<SpringJoint>().breakForce = 100.0f;
			col.gameObject.GetComponent<SpringJoint>().connectedBody = boody;
			transform.parent = col.transform;
			transform.localPosition += new Vector3(0, -0.01f, 0);
		}
		//col.transform.parent.GetComponent<BoxCollider>().isTrigger = false;
	}

	void OnCollision(Collision collision) {
		if(collision.collider.name.Contains("Treasurebox") || collision.collider.name.Contains("Sphere") ) {
			boody.AddForce(new Vector3(0, 1, 0));
		}
	}

	void Start() {
		boody = GetComponent<Rigidbody>();
	}

	void Update() {
		force = GetComponent<ConstantForce>().force;
		Debug.Log(force);
		Debug.Log(force.magnitude);
		if(force.magnitude > 100.0f) {
			transform.parent = null;
		}
	}
}
