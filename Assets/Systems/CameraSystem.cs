﻿using UnityEngine;
using FYFY;
using FYFY_plugins.PointerManager;

public class CameraSystem : FSystem {
	private Family controllableGO = FamilyManager.getFamily(new AllOfComponents(typeof(CameraComponent)));

	private Family UIGO = FamilyManager.getFamily(new AnyOfComponents(typeof(UIActionType), typeof(UITypeContainer), typeof(ElementToDrag)),
													new AllOfComponents(typeof(PointerOver)));

	public CameraSystem()
	{
		foreach( GameObject go in controllableGO)
		{
			onGOEnter(go);
	    }
	    controllableGO.addEntryCallback(onGOEnter);
	}

	private void onGOEnter(GameObject go)
	{
        go.GetComponent<CameraComponent>().orbitH = go.GetComponent<CameraComponent>().transform.eulerAngles.y;
        go.GetComponent<CameraComponent>().orbitV = go.GetComponent<CameraComponent>().transform.eulerAngles.x;
        //go.GetComponent<CameraComponent>().movementRotation = new Vector3 (Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));

	}

	// Use this to update member variables when system pause. 
	// Advice: avoid to update your families inside this function.
	protected override void onPause(int currentFrame) {
	}

	// Use this to update member variables when system resume.
	// Advice: avoid to update your families inside this function.
	protected override void onResume(int currentFrame){
		
	}

	// Use to process your families.
	protected override void onProcess(int familiesUpdateCount) {
		foreach( GameObject go in controllableGO ){
			
			/*
			Debug.Log(Input.GetAxis("Horizontal"));
			if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
			{
				if (go.GetComponent<CameraComponent>().limiteCount >= go.GetComponent<CameraComponent>().limiteMin && go.GetComponent<CameraComponent>().limiteCount < go.GetComponent<CameraComponent>().limiteMax)
				{
					go.transform.position += new Vector3(go.transform.forward.x + go.transform.up.x, 0, go.transform.forward.z + go.transform.up.z ) * Input.GetAxis("Vertical") * go.GetComponent<CameraComponent>().cameraSpeed * Time.deltaTime;
					go.transform.position += new Vector3(go.transform.right.x, 0, go.transform.right.z ) * Input.GetAxis("Horizontal") * go.GetComponent<CameraComponent>().cameraSpeed * Time.deltaTime;
		        	go.GetComponent<CameraComponent>().limiteCount++;
		        }
		    }
		    */

		    // gauche droite
		    go.transform.position += new Vector3(go.transform.forward.x + go.transform.up.x, 0, go.transform.forward.z + go.transform.up.z ) * Input.GetAxis("Vertical") * go.GetComponent<CameraComponent>().cameraSpeed * Time.deltaTime;
			// avance recule
			go.transform.position += new Vector3(go.transform.right.x, 0, go.transform.right.z ) * Input.GetAxis("Horizontal") * go.GetComponent<CameraComponent>().cameraSpeed * Time.deltaTime;
			Debug.Log("x = " + go.transform.position.x);
			Debug.Log("y = " + go.transform.position.y);
			Debug.Log("z = " + go.transform.position.z);
			
			go.transform.position = new Vector3(
			   Mathf.Clamp(Camera.main.transform.position.x, go.GetComponent<CameraComponent>().MIN_X, go.GetComponent<CameraComponent>().MAX_X),
			   Mathf.Clamp(Camera.main.transform.position.y, go.GetComponent<CameraComponent>().MIN_Y, go.GetComponent<CameraComponent>().MAX_Y),
			   Mathf.Clamp(Camera.main.transform.position.z, go.GetComponent<CameraComponent>().MIN_Z, go.GetComponent<CameraComponent>().MAX_Z));
			
			/*   
			go.transform.position = new Vector3(
			   Mathf.Clamp(go.transform.position.x, go.GetComponent<CameraComponent>().farLeft.position.x, go.GetComponent<CameraComponent>().farRight.position.x),
			   Mathf.Clamp(go.transform.position.y, go.GetComponent<CameraComponent>().farLeft.position.y, go.GetComponent<CameraComponent>().farRight.position.y),
			   Mathf.Clamp(go.transform.position.z, go.GetComponent<CameraComponent>().farLeft.position.z, go.GetComponent<CameraComponent>().farRight.position.z));
			*/
			//------------------------------------------------------------------------------------

			// Déplacement avec la molette comme dans l'éditeur
	        if (Input.GetMouseButton(2))
            {
            	Cursor.lockState = CursorLockMode.Locked;
	        	Cursor.visible = false;
                go.transform.Translate(-Input.GetAxisRaw("Mouse X") * Time.deltaTime * go.GetComponent<CameraComponent>().dragSpeed, -Input.GetAxisRaw("Mouse Y") * Time.deltaTime * go.GetComponent<CameraComponent>().dragSpeed, 0);
            }

			// Zoom avec la molette
			else if(Input.GetAxis("Mouse ScrollWheel") < 0)
	        {
	            if(go.GetComponent<CameraComponent>().ScrollCount >= go.GetComponent<CameraComponent>().ScrollWheelminPush && go.GetComponent<CameraComponent>().ScrollCount < go.GetComponent<CameraComponent>().ScrollWheelLimit)
	            {
	                go.transform.position += new Vector3(0, go.GetComponent<CameraComponent>().zoomSpeed, 0);
	                go.GetComponent<CameraComponent>().ScrollCount++;
	            }
	        }
	        else if(Input.GetAxis("Mouse ScrollWheel") > 0)
	        {
	            if(go.GetComponent<CameraComponent>().ScrollCount > go.GetComponent<CameraComponent>().ScrollWheelminPush && go.GetComponent<CameraComponent>().ScrollCount <= go.GetComponent<CameraComponent>().ScrollWheelLimit)
	            {
	                go.transform.position -= new Vector3(0, go.GetComponent<CameraComponent>().zoomSpeed, 0);
	                go.GetComponent<CameraComponent>().ScrollCount--;
	            }
	        }
	        
            // Déplacement de type orbite
            else if (Input.GetMouseButton(1))
            {
            	go.GetComponent<CameraComponent>().orbitH += go.GetComponent<CameraComponent>().lookSpeedH * Input.GetAxis("Mouse X");
                go.GetComponent<CameraComponent>().orbitV -= go.GetComponent<CameraComponent>().lookSpeedV * Input.GetAxis("Mouse Y");
                go.GetComponent<CameraComponent>().transform.eulerAngles = new Vector3(go.GetComponent<CameraComponent>().orbitV, go.GetComponent<CameraComponent>().orbitH, 0f);
            	//go.transform.position = go.transform.position + new Vector3(0,go.GetComponent<CameraComponent>().orbitH,0);
            }
	        else
	        {
	        	Cursor.lockState = CursorLockMode.None;
	        	Cursor.visible = true;
	    	}
			//------------------------------------------------------------------------------------
		}		
	}
}