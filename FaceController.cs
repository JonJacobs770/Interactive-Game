using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;
using System.Threading;
using System.IO;

public class FaceController : MonoBehaviour {

	public Camera cam;
	public static FaceController Instance;
	public SerialPort serial = new SerialPort ("\\\\.\\COM5",9600);
    //public SerialPort serial = new SerialPort("\\\\.\\COM7", 9600);
    private Rigidbody rigidbodyComponent;
	private Vector3 updateVector;
	public Vector3 updateMag;
	public Vector3 referenceMag=new Vector3(0,0,0);
	private Vector3 referenceVector=new Vector3(0,0,0);
	private Vector3 resultantVector;	//private Vector3 referenceGravity;
	//public Vector3 previousUpdateVector=new Vector3(0,0,0);
	public Vector3 resultantMag;
	private StreamWriter outputFile;
	public bool rightHand=true;
	public Vector3 newpos = new Vector3 (0, 0, 0);

	public GameObject collectOrangeFace;
	public GameObject collectPurpleFace;
	public GameObject collectGreenFace;

//	private float maxWidth;
//	private bool canControl;
		// Use this for initialization
	void Startcontrol () {
		serial.Open ();
		serial.ReadTimeout = 22;
		if (cam == null) {
			cam = Camera.main;
		}
	


//	
//		Vector3 upperCorner = new Vector3 (Screen.width, Screen.height, 0.0f);
//		Vector3 targetWidth = cam.ScreenToWorldPoint (upperCorner);
//		float hatWidth = GetComponent<Renderer>().bounds.extents.x;
//		maxWidth = targetWidth.x - hatWidth;
//		canControl = false;
	}
	public Vector3 getInstantaniousVec3()
	{
		return updateVector;

	}

	public void applyReferenceGravity(Vector3 grav)
	{
		referenceVector = grav;

	}

	public Vector3 getInstantaniousMagVec3()
	{
		return updateMag;

	}
	public void applyReferenceMag(Vector3 dir)
	{
		referenceMag = dir;

	}




	void Update()
	{
		string value=""; 
		string[] vec=new string[6];
		vec[0]=" ";
		vec[1]=" ";
		vec[2]=" ";
		vec[3]=" ";
		vec[4]=" ";
		vec[5]=" ";



		if (!serial.IsOpen) {
			serial.Open ();
		} else 
		{
			value = serial.ReadLine();
			vec = value.Split (',');


			//print("ax: "+(float)(decimal.Parse(vec[0]))+" ay: "+(float)(decimal.Parse(vec[1]))+" az: "+(float)(decimal.Parse(vec[2]))+" mx: "+(float)(decimal.Parse(vec[3]))+" my: "+(float)(decimal.Parse(vec[4]))+" mz: "+(float)(decimal.Parse(vec[5])));

			updateVector=new Vector3 ((float)(decimal.Parse(vec[0]))/16600.0f,(float)(decimal.Parse(vec[1]))/16600.0f,(float)(decimal.Parse(vec[2]))/16600.0f);
			//updateVector.Normalize();
			updateVector = new Vector3(Mathf.Floor(updateVector.x*10.0f),Mathf.Floor(updateVector.y*10.0f),Mathf.Floor(updateVector.z*10.0f))/10.0f;
			updateMag = new Vector3 ((float)(decimal.Parse (vec [3])), (float)(decimal.Parse (vec [4])), (float)(decimal.Parse (vec [5])));

//		outputFile = new StreamWriter ("C:\\Users\\jon\\Desktop\\Data\\WriteLines.txt",true);
//		outputFile.WriteLine (value);
//		outputFile.Close ();
		}
	}
	
	// Update is called once per physics timestep
	void FixedUpdate () {
//		if (canControl) {
//			Vector3 rawPosition = cam.ScreenToWorldPoint (Input.mousePosition);
//			Vector3 targetPosition = new Vector3 (rawPosition.x, 0.0f, 0.0f);
//			float targetWidth = Mathf.Clamp (targetPosition.x, -maxWidth, maxWidth);
//			 targetPosition = new Vector3 (targetWidth, targetPosition.y, targetPosition.z);
//			GetComponent<Rigidbody2D>().MovePosition (targetPosition);

		if (rigidbodyComponent == null) {
			rigidbodyComponent = GetComponent<Rigidbody> ();
		}


		//resultantVector = (updateVector + previousUpdateVector) / 2;
		//resultantVector=updateVector-referenceVector;


		resultantMag = updateMag - referenceMag;
		//resultantVector.Normalize ();
		if (rightHand == true) {
			if (referenceMag != Vector3.zero) {
			
				collectOrangeFace.SetActive (true);


				if (Math.Abs (resultantMag.x) > 1.0f || Math.Abs (resultantMag.y) > 1.0f || Math.Abs (resultantMag.z) > 1.0f) {


					//transform.position.x=resultantMag.magnitude ;
					Vector3 newpos = transform.position;
					newpos.x = (resultantMag.magnitude) * 15.0f / 360.0f - 7.5f; 
					if (newpos.x > 7.5f) {
						newpos.x = 7.5f;
					}
					transform.position = newpos;
				
//					outputFile = new StreamWriter ("C:\\Users\\jon\\Desktop\\Data\\Position.txt",true);
//					outputFile.WriteLine (newpos.x);
////
////
////				outputFile.WriteLine (String.Format("{0} {1}", newpos.x, resultantMag.magnitude/1.414213562f));
////
////
//											outputFile.Close ();
				}
			}
		} else {
			if (referenceMag != Vector3.zero) {

				collectOrangeFace.SetActive (true);


				if (Math.Abs (resultantMag.x) > 1.0f || Math.Abs (resultantMag.y) > 1.0f || Math.Abs (resultantMag.z) > 1.0f) {


					//transform.position.x=resultantMag.magnitude ;
					Vector3 newpos = transform.position;
					newpos.x = -(resultantMag.magnitude) * 15.0f / 360.0f + 7.5f; 
					if (newpos.x > 7.5f) {
						newpos.x = 7.5f;
					}
					transform.position = newpos;

				}
			}
		}
			

//		if (referenceVector != Vector3.zero) {
//
//
//			if (Math.Abs (resultantVector.x) > 0.1f || Math.Abs (resultantVector.y) > 0.1f || Math.Abs (resultantVector.z) > 0.1f) {
//
//
//				//rigidbodyComponent.AddForce (resultantVector*3.0f, ForceMode.Force);
//				//print ("Added force: " + resultantVector*3.0f);
//
//			}
			//previousUpdateVector=previousUpdateVector*0.3f-referenceVector*0.3f;
			//previousUpdateVector = resultantVector;

		}
}




	
		

	

