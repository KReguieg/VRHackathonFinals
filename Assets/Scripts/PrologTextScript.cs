using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrologTextScript : MonoBehaviour {

	float time;
	int counter;

	string[] prologText;

	// Use this for initialization
	void Start () {
		counter = 0;
		prologText = new string[]{"Whhhaaat..., Whaaat happened?!?!", "There was a crash in time-space continuum...\nThe whole universe is upside down...,"
								+ "\nSome stars exploded some were born.\nSome people got shrunk, others got enlarged..." + "\nNothing really matters anymore...",
								"EXCEPT", "FACTS, \nStraight facts!\nlike numbers and Math\nand such deep shit... Not that crap they are telling you on TV",
								"Team up with a giant friend and help each other\nsolving riddles as well as physical challenges.", "Maybe you can bring the universe to a normal state again.",
								"..."};
								
		GetComponent<Text>().text = prologText[counter];
		counter += 1;
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		if(counter == prologText.Length) {
			transform.parent.parent.gameObject.SetActive(false);
		}
		if(time >= 10.0f) {
			time = 0.0f;
			GetComponent<Text>().text = prologText[counter++];
		}
	}
}
