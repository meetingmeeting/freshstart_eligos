﻿using UnityEngine;
using System.Collections;

public class UI_skill_manager : MonoBehaviour {
	public GameObject myplayer;
	public DogSkill_GUI dog_skill_gui;
	public GuciSkill_GUI guci_skill_gui;
	public Barbas_GUI barbas_skill_gui;
	public Tutu_skill_gui tutu_skill;
	public FurfurSkill_GUI furfur_skill;
	public StolaSkill_GUI _stola_skill;
	
	public void setPlayer(){
		myplayer = GameObject.Find (ClientState.id);
		
		switch (ClientState.character) {
		case "dog":
			dog_skill_gui  = GameObject.Find (ClientState.id).GetComponent<DogSkill_GUI>();
			dog_skill_gui.setPlayer();
			break;
		case "guci":
			guci_skill_gui  = GameObject.Find (ClientState.id).GetComponent<GuciSkill_GUI>();
			guci_skill_gui.setPlayer();
			break;
		case "turtle":
			tutu_skill = GameObject.Find (ClientState.id).GetComponent<Tutu_skill_gui>();
			tutu_skill.setPlayer();
			break;
		case "barbas":
			barbas_skill_gui = GameObject.Find (ClientState.id).GetComponent<Barbas_GUI>();
			barbas_skill_gui.setPlayer();
			break;
		case "furfur":
			furfur_skill = GameObject.Find (ClientState.id).GetComponent<FurfurSkill_GUI>();
			furfur_skill.setPlayer();
			break;
		case "stola":
			_stola_skill = GameObject.Find (ClientState.id).GetComponent<StolaSkill_GUI>();
			_stola_skill.setPlayer();
			break;
		}
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	public void firstSkill(){
		switch (ClientState.character) {
		case "dog":
			dog_skill_gui.Skill1_bot();
			break;
		case "turtle":
			tutu_skill.Skill1_bot();
			break;
		case "guci":
			guci_skill_gui.Skill1_bot();
			break;
		case "barbas":
			barbas_skill_gui.Skill1_bot();
			break;
		case "furfur":
			furfur_skill.Skill1_bot();
			break;
		case "stola":
			_stola_skill.Skill1_bot();
			break;
		}
	}
	
	public  void secondSkill(){
		switch (ClientState.character) {
		case "dog":
			dog_skill_gui.Skill2_bot();
			break;
		case "turtle":
			tutu_skill.Skill2_bot();
			break;
		case "guci":
			guci_skill_gui.Skill2_bot();
			break;
		case "barbas":
			barbas_skill_gui.Skill2_bot();
			break;
		case "furfur":
			furfur_skill.Skill2_bot();
			break;
		case "stola":
			_stola_skill.Skill2_bot();
			break;
		}
	}
	
	public void thirdSkill(){
		switch (ClientState.character) {
		case "dog":
			dog_skill_gui.Skill3_bot();			
			break;
		case "turtle":
			tutu_skill.Skill3_bot(); 			
			break;
		case "guci":
			guci_skill_gui.Skill3_bot();
			break;
		case "barbas":
			barbas_skill_gui.Skill3_bot();
			break;
		case "furfur":
			furfur_skill.Skill3_bot();
			break;
		case "stola":
			_stola_skill.Skill3_bot();
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {
	}
	
}

