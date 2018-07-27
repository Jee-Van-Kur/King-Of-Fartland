using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpwaner : MonoBehaviour {

    public static EnemySpwaner instance;
    public GameObject enemy;
    float randX;
    Vector2 spwanHere;
    public float SpwanRate = 0.5f;
    float nextSpwan = 0.0f;
	// Use this for initialization
	void Start () {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time > nextSpwan)
        {
            nextSpwan = Time.time + SpwanRate;
            randX = Random.Range(-4f, 4f);
            spwanHere = new Vector2(randX, transform.position.y);
            Instantiate(enemy, spwanHere, Quaternion.identity);
        }
	}
}
