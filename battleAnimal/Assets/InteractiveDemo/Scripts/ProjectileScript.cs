﻿using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour {

    public GameObject impactParticle;
    public GameObject projectileParticle;
    public GameObject[] trailParticles;
    [HideInInspector]
    public Vector3 impactNormal; //Used to rotate impactparticle.

	// Use this for initialization
	void Start () {
        projectileParticle = Instantiate(projectileParticle, transform.position, transform.rotation) as GameObject;
        projectileParticle.transform.parent = transform;
	}
	
	// Update is called once per frame
	void OnCollisionEnter (Collision hit) {

        //transform.DetachChildren();
        impactParticle = Instantiate(impactParticle, transform.position, Quaternion.FromToRotation(Vector3.up, impactNormal)) as GameObject;
        //Debug.DrawRay(hit.contacts[0].point, hit.contacts[0].normal * 1, Color.yellow);

        if (hit.gameObject.tag == "Destructible") // Projectile will destroy objects tagged as Destructible
        {
            Destroy(hit.gameObject);
        }


        //yield WaitForSeconds (0.05);
        foreach (GameObject trail in trailParticles)
	    {
            GameObject curTrail = transform.Find(projectileParticle.name + "/" + trail.name).gameObject;
            curTrail.transform.parent = null;
            Destroy(curTrail, 3f); 
	    }

		Destroy(impactParticle, 5f);

        Destroy(projectileParticle, 3f);

        Destroy(gameObject);
        //projectileParticle.Stop();

	}
}
