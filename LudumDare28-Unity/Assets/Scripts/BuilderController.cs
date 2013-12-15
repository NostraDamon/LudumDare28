using UnityEngine;
using System.Collections;

public class BuilderController : MonoBehaviour
{
    private Vector3 screenPos;
    private RaycastHit hit;
    private GameObject partContainers;

    public GUIText textPart;
    private GameObject tab1;
    private GameObject tab2;
    private GameObject tab3;
    private GameObject tab4;
    private GameObject tab5;

    private GameObject buttonOK;
    private GameObject buttonBack;

	// Use this for initialization
	void Start ()
    {
        screenPos = Vector3.zero;
        textPart = GameObject.Find("TextPart").guiText;
        partContainers = GameObject.Find("PartContainers");
        tab1 = GameObject.Find("Icon_Tab1");
        tab2 = GameObject.Find("Icon_Tab2");
        tab3 = GameObject.Find("Icon_Tab3");
        tab4 = GameObject.Find("Icon_Tab4");
        tab5 = GameObject.Find("Icon_Tab5");
	}

    // Update is called once per frame
    void Update()
    {
        screenPos = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));

        // Cast ray from camera to GUI 3D
        if (Physics.Raycast(screenPos, Vector3.forward, out hit, Mathf.Infinity))
        {
            // Mouse over Part
            if (hit.collider.tag == "BuilderPart")
            {
                if (hit.collider.transform.childCount > 0)
                {
                    // Start rotating and enlighten
                    hit.collider.transform.GetChild(0).GetComponent<Rotation>().isRotating = true;
                    hit.collider.transform.GetChild(0).GetComponent<BuilderPartBehavior>().renderer.material = hit.collider.transform.GetChild(0).GetComponent<BuilderPartBehavior>().matLight;
                }
            }

            // Mouse over Tab
            if (hit.collider.tag == "BuilderTab")
            {
                // Click tab
                if (Input.GetMouseButtonDown(0))
                {
                    // Unselect everything
                    Color tempColor = new Color(0.25f, 0.25f, 0.25f, 0.25f);
                    tab1.guiTexture.color = tempColor;
                    tab2.guiTexture.color = tempColor;
                    tab3.guiTexture.color = tempColor;
                    tab4.guiTexture.color = tempColor;
                    tab5.guiTexture.color = tempColor;

                    // Select clicked tab, Update text
                    GameObject.Find("Icon_Tab" + hit.collider.name.Substring(0, 1)).guiTexture.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
                    textPart.guiText.text = hit.collider.name;
                }
            }

            // Mouse over Button
            if (hit.collider.tag == "BuilderButton")
            {
                // Click button
                if (Input.GetMouseButtonDown(0))
                {
                    // OK


                    // BACK

                }
            }
        }
        else
        {
            for (int i = 0; i < partContainers.transform.childCount; i++)
            {
                // Stop rotating and switch light off
                if (partContainers.transform.GetChild(i).childCount > 0)
                {
                    partContainers.transform.GetChild(i).transform.GetChild(0).GetComponent<Rotation>().isRotating = false;
                    partContainers.transform.GetChild(i).transform.GetChild(0).GetComponent<BuilderPartBehavior>().renderer.material = partContainers.transform.GetChild(i).transform.GetChild(0).GetComponent<BuilderPartBehavior>().matBasic;
                }
            }
        }
    }
}
