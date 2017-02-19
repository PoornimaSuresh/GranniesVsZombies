using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granny : MonoBehaviour
{

	GameObject worldManager;
	GameObject[] enemy;
	float speed = 0.5f;
	float damage = 1;
	float health = 3;
	float unit_range = 2;
	float attack_speed = 1f;
	float cooldownTime = 0f;
	Vector3 moveDirection;
	GameObject nearestEnemy;
	UnitCount countManager;
	// Use this for initialization
	void Start()
	{
		worldManager = GameObject.FindGameObjectWithTag("world_manager");
		enemy = GameObject.FindGameObjectsWithTag("zombie");
		countManager = worldManager.GetComponent<UnitCount>();
	}

	// Update is called once per frame
	void Update()
	{

		if (countManager.num_zombies > 0)
		{
			//Every frame seek out enemies
			nearestEnemy = GetNearestEnemy();
			moveDirection = nearestEnemy.transform.position - this.transform.localPosition;
			float distancePerFrame = speed * Time.deltaTime;

			if (moveDirection.magnitude >= distancePerFrame * 200)
			{
				transform.Translate(new Vector3(moveDirection.x, 0, moveDirection.z) * distancePerFrame, Space.World);
				this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(moveDirection), Time.deltaTime);
			}

			cooldownTime -= Time.deltaTime;
			if(cooldownTime <= 0)
			{
				cooldownTime = attack_speed;
				Attack(); //attempt to attack every frame if in range
			}
			
		}


	}
	int count = 0;
	GameObject GetNearestEnemy()
	{
		nearestEnemy = enemy[0];
		while (nearestEnemy == null)
		{
			if (count >= enemy.Length)
				count = 0;
			nearestEnemy = enemy[count];
			count++;
		}
		foreach (GameObject enemy in enemy)
		{

			if (enemy != null && Vector3.Distance(gameObject.transform.position, enemy.transform.position) <= Vector3.Distance(gameObject.transform.position, nearestEnemy.transform.position))
			{
				nearestEnemy = enemy;
			}
		}
		return nearestEnemy;
	}

	void Attack()
	{
		//Debug.Log(moveDirection.magnitude);
		//play animation
		if (moveDirection.magnitude <= unit_range)
		{
			nearestEnemy.GetComponent<zombie>().InflictDamage(damage);
		}

	}
	public void InflictDamage(float damage)
	{
		health -= damage;
		if (health <= 0)
		{
			Kill();
		}
	}

	void Kill()
	{
		countManager.num_grannies--;
		Destroy(gameObject);
	}
}
