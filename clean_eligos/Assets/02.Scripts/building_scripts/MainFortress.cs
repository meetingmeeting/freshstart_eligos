﻿using UnityEngine;
using System.Collections;

public class MainFortress : MonoBehaviour {public GameObject bloodEffect;
	public GameObject bloodDecal;
	
	public int hp = 400;
	public bool buildingDead;	
	public Texture2D victory, defeat ;
	
	// Use this for initialization
	void Start () {		
		buildingDead = false;	


	}
	

	
	void OnGUI(){		


		if (this.gameObject.name == "blue_building" && buildingDead ==true ) {			
			if(ClientState.team =="red"){
				GUI.DrawTexture(new Rect (300, 10, 550, 400), victory);
				
			}else{
				GUI.DrawTexture(new Rect (300, 10, 550, 400), defeat);
			}
			
			if (GUI.Button (new Rect (450, 370, 150, 100), "ok")) {
				Application.LoadLevel ("scEndGame");
			}
		}else if(this.gameObject.name == "red_building" && buildingDead==true){			
			if(ClientState.team =="blue"){
				GUI.DrawTexture(new Rect (300, 10, 550, 400), victory);
				
			}else{
				GUI.DrawTexture(new Rect (300, 10, 550, 400), defeat);
			}
			
			if (GUI.Button (new Rect (450, 370, 150, 100), "ok")) {				
				Application.LoadLevel ("scEndGame");				
			}
		}
	}
	
	public void Heated(string firedby, GameObject obj,int damage){
		if (ClientState.isMaster) {	
			hp -= damage;

			if (ClientState.isMulty) {
			string data = this.name + ":" + hp.ToString () + "";
			SocketStarter.Socket.Emit ("attackBuilding", data);
			}
			
			if (hp <= 0) {
				hp = 0;
				buildingDie ();
				
				string data2 = ClientState.id + ":" + this.name;
				//SocketStarter.Socket.Emit ("minionDieREQ", data2);
			}
		}
		
		Collider coll = obj.collider;	
		StartCoroutine (this.CreateBloodEffect(coll.transform.position));		
	}
	
	public void HeatedSync(int _hp){
		hp = _hp;
		
		if (hp <= 0) {
			hp = 0;
			buildingDie ();
		}
	}
	
	IEnumerator CreateBloodEffect(Vector3 pos)
	{
		GameObject _blood1 = (GameObject)Instantiate (bloodEffect, pos, Quaternion.identity);
		Destroy (_blood1, 2.0f);
		
		Vector3 decalPos = this.transform.position+(Vector3.right*5.01f);
		Quaternion decalRot = Quaternion.Euler(0,Random.Range(0,360),0);
		
		yield return null;
	}
	
	public void buildingDie(){		
		buildingDead = true;
		Debug.Log ("Clientstate.team = "+ClientState.team);
	}
}