using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCount : MonoBehaviour {

	public int num_grannies;
	public int num_zombies;
	// Use this for initialization
	void Start () {
		num_grannies =  GameObject.FindGameObjectsWithTag("granny").Length;
		num_zombies = GameObject.FindGameObjectsWithTag("zombie").Length;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
