using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour
{
    private Vector3 newRot;

    public bool isRotating;

	// Use this for initialization
	void Start ()
    {
        newRot = transform.rotation.eulerAngles;
        isRotating = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (isRotating)
        {
            // Rotate
            newRot = new Vector3(newRot.x, newRot.y + Time.deltaTime * 10, newRot.z);
            transform.rotation = Quaternion.Euler(newRot);
        }
	}
}
