﻿using UnityEngine;
using System.Collections;

public class BulletCtrl_BlueCannon : MonoBehaviour {
	public int damage;
	private float speed;
	public float birth;
	private float durationTime;
	public GameObject target;
	private Transform tr;
	private TrailRenderer _trail;
	
	// Use this for initialization
	void Start () {
		tr = GetComponent<Transform> ();
		_trail = GetComponent<TrailRenderer> ();
		//target = null;
		damage = 40;
		speed = 20.0f;
		//rigidbody.AddForce (transform.forward * speed);
		birth = Time.time;
		durationTime = 5.0f;
	}
	
	public void setTarget(string _name){
		target = GameObject.Find(_name);
	}
	
	// Update is called once per frame
	void Update () {
		if (target != null) {
			Vector3 targetPosition=target.transform.position;
			targetPosition.y = 51.0f;
			if (targetPosition != tr.position) {
				float step = speed* Time.deltaTime;
				tr.position = Vector3.MoveTowards(tr.position, targetPosition, step);
			}
		}
		if ((Time.time - birth) > durationTime) {
			StartCoroutine (PushObjectPool ());	
			birth = Time.time;
		}
	}
	
	void OnTriggerEnter(Collider coll){
		if (target != null) {
			if(target.name==coll.name){
				if(target.tag=="MINION"){
					if(target.name[0]=='r')
						target.GetComponent<minion_state>().Heated("minion", gameObject,damage);
					else
						target.GetComponent<blue_minion_state>().Heated("minion",gameObject,damage);
					StartCoroutine (PushObjectPool ());	
				}else if(target.tag=="Player"){					
					target.GetComponent<PlayerHealthState>().Heated("minion", gameObject,damage);
					StartCoroutine (PushObjectPool ());			
				}//hit player
			}
		}
	}
	
	IEnumerator PushObjectPool()
	{
		yield return new WaitForSeconds(0.1f);
		_trail.enabled = false;
		gameObject.SetActive (false);
	}
}
