﻿using UnityEngine;
using System.Collections;

public class Barbas_secondSkill: MonoBehaviour {
	public GameObject bulleta;
	private float distance;
	private MoveCtrl _moveCtrl;
	
	// Use this for initialization
	void Start () {
		_moveCtrl = GetComponent<MoveCtrl> ();
		distance = 10.0f;
	}
	
	
	public void startSkill(string firedBy,Vector3 _pos){
		StartCoroutine (this.CreateBullet (firedBy,_pos));
		StartCoroutine (this.ShowMuzzleFlash ());
	}
	
	IEnumerator CreateBullet(string firedBy,Vector3 _pos){
		while (true) {
			yield return new WaitForSeconds(1.0f);
			float dist = Vector3.Distance (this.transform.position, _pos);
			if (dist > distance) {
				_moveCtrl.clickendpoint = _pos;
				_moveCtrl.clickendpoint.y = _moveCtrl.terrainHeight;
				//_moveCtrl.isMoveAndAttack = true;
				_moveCtrl.playermoving = true;
				//moveAndAttack ();
			} else {
				GameObject a = (GameObject)Instantiate (bulleta, _pos, this.transform.rotation);
				_moveCtrl.idle();
				break;	
			}		
		}
	}
	
	IEnumerator ShowMuzzleFlash(){
		//_renderera.enabled = true;
		yield return new WaitForSeconds(Random.Range(0.01f,0.2f));
		//	_renderera.enabled = false;
	}
}