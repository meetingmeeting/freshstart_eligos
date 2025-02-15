﻿using UnityEngine;
using System.Collections;

public class Furfur_firstBulletCtrl : MonoBehaviour {
	private float speed;
	public float birth;
	private float durationTime;
	public Vector3 targetPos;
	public Transform tr;
	public string firedByName;
	public int damage;
	private TrailRenderer _trail;
	public GameObject damageEffectObject;
	
	// Use this for initialization
	void Awake () {
		tr = GetComponent<Transform> ();
		_trail = GetComponent<TrailRenderer> ();
		//target = null;
		damage = playerStat.skill1_damage;
		speed = 20.0f;
		rigidbody.AddForce (transform.up * 1000.0f);
		birth = Time.time;
		durationTime = 5.0f;
	}
	
	//set target
	public void setTarget(string firedby, Vector3 _pos){
		targetPos = _pos;
		firedByName = firedby;
		birth = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance( targetPos,transform.position)>1.0f) {
			float step = speed* Time.deltaTime;
			tr.position = Vector3.MoveTowards(tr.position, targetPos, step);
		}
		
		if ((Time.time - birth) > durationTime) {
			//birth = Time.time;
			Destroy(this.gameObject);
		}
	}


	void OnTriggerEnter(Collider coll){
		if(ClientState.isMaster){
			if (coll.gameObject.tag == "MINION") {
				string hitParentName = coll.transform.parent.name;
				string firedparentName = GameObject.Find (firedByName).transform.parent.name;
			
				if ((ClientState.team == "red" && coll.name [0] == 'b') ||
			  	  (ClientState.team == "blue" && coll.name [0] == 'r')) {
					damageEffectFunc (this.transform.position);
				if (coll.gameObject.name [0] == 'r'){
						coll.gameObject.GetComponent<minion_state> ().Heated ("skill", gameObject, damage);
					onTriggerEmitter(coll.gameObject.name,1);
				}else if (coll.gameObject.name [0] == 'b'){
						coll.gameObject.GetComponent<blue_minion_state> ().Heated ("skill", gameObject, damage);
						onTriggerEmitter(coll.gameObject.name,2);
				}
					Destroy (this.gameObject);
				}
			} else if (coll.gameObject.tag == "Player" && coll.name != "touchCollider"&&coll.name!=firedByName) {
			
				string hitParentName = coll.transform.parent.name;
				string firedparentName = GameObject.Find (firedByName).transform.parent.name;
			
				if (hitParentName != firedparentName && hitParentName != firedByName) {
					coll.gameObject.GetComponent<PlayerHealthState> ().hitbySkill (firedByName, this.gameObject,damage);
					onTriggerEmitter(coll.gameObject.name,3);
					Destroy (this.gameObject);
				}//if
			} else if (coll.gameObject.tag == "FLOOR") {
				damageEffectFunc (this.transform.position);
				Destroy (this.gameObject);
			}
		}else{
			if (coll.gameObject.tag == "MINION") {
				string hitParentName = coll.transform.parent.name;
				string firedparentName = GameObject.Find (firedByName).transform.parent.name;
				
				if ((ClientState.team == "red" && coll.name [0] == 'b') ||
				    (ClientState.team == "blue" && coll.name [0] == 'r')) {
					damageEffectFunc (this.transform.position);
					Destroy (this.gameObject);
				}
			} else if (coll.gameObject.tag == "Player" && coll.name != "touchCollider"&&coll.name!=firedByName) {
				
				string hitParentName = coll.transform.parent.name;
				string firedparentName = GameObject.Find (firedByName).transform.parent.name;
				
				if (hitParentName != firedparentName && hitParentName != firedByName) {
					Destroy (this.gameObject);
				}//if
			} else if (coll.gameObject.tag == "FLOOR") {
				damageEffectFunc (this.transform.position);
				Destroy (this.gameObject);
			}
		}
	}
	
	void damageEffectFunc(Vector3 _pos){
		GameObject a = (GameObject)Instantiate(damageEffectObject);
		_pos.y += 3.0f;
		a.transform.position = _pos;
	}
	
	private void onTriggerEmitter(string enemy,int order){
		if (ClientState.isMulty) {
						string data = ClientState.id + ":" + ClientState.character + ":" + "first" + ":" + enemy + ":" + order.ToString () + ":" + damage.ToString ();
						SocketStarter.Socket.Emit ("SkillDamageREQ", data);
				}
	}
}