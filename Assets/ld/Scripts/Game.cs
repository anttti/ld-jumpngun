using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

	[SerializeField] private GameObject playerSpawnPosition;
	[SerializeField] private GameObject enemySpawnPosition1;
	[SerializeField] private GameObject enemySpawnPosition2;
	[SerializeField] private GameObject playerGameObject;
	[SerializeField] private GameObject enemyGameObject;

	private int enemySpawnInterval = 3;
	private int nextEnemySpawnTime = 0;

	void Start () {
	
	}

	void Update () {
		if (Time.time > nextEnemySpawnTime) {
			nextEnemySpawnTime += enemySpawnInterval;

			Vector3 position;
			if (Random.value > 0.5f) {
				position = enemySpawnPosition1.transform.position;
			} else {
				position = enemySpawnPosition2.transform.position;
			}

			Instantiate (enemyGameObject, position, Quaternion.identity);
		}
	}
}
