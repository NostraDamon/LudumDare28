using UnityEngine;
using System.Collections;

public class BuilderController : MonoBehaviour
{
    private Vector3 screenPos;
    private RaycastHit hit;
    private _PartManager PartManager;

    public GUIText textPart;
    private GameObject[] tabs = new GameObject[5];
    private GameObject[] partContainers = new GameObject[12];

    private GameObject buttonOK;
    private GameObject buttonBack;

	// Use this for initialization
	void Start ()
    {
        PartManager = GameObject.Find("_PartManager").GetComponent<_PartManager>();

        screenPos = Vector3.zero;
        textPart = GameObject.Find("TextPart").guiText;

        // Get tabs
        for (int i = 0; i < tabs.Length; i++)
        {
            tabs[i] = GameObject.Find("Icon_Tab" + (i + 1).ToString());
        }

        // Get containers
        for (int j = 0; j < partContainers.Length; j++)
        {
            partContainers[j] = GameObject.Find("PartContainer_" + (j + 1).ToString());
        }

        UnselectTabs(0);
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
            else
            {
                for (int i = 0; i < partContainers.Length; i++)
                {
                    // Stop rotating and switch light off
                    if (partContainers[i].transform.childCount > 0)
                    {
                        partContainers[i].transform.GetChild(0).GetComponent<Rotation>().isRotating = false;
                        partContainers[i].transform.GetChild(0).GetComponent<BuilderPartBehavior>().renderer.material = partContainers[i].transform.GetChild(0).GetComponent<BuilderPartBehavior>().matBasic;
                    }
                }
            }

            // Mouse over Tab
            if (hit.collider.tag == "BuilderTab")
            {
                // Click tab
                if (Input.GetMouseButtonDown(0))
                {
                    // Unselect everything except current
                    int temp;
                    if (int.TryParse(hit.collider.name.Substring(0, 1), out temp))
                    {
                        UnselectTabs(temp - 1);
                    }

                    // Update part text
                    textPart.guiText.text = hit.collider.name;

                    // Destroy current parts
                    for (int i = 0; i < partContainers.Length; i++)
                    {
                        if (partContainers[i].transform.childCount > 0)
                        {
                            Destroy(partContainers[i].transform.GetChild(0).gameObject);
                        }
                    }

                    // Show new parts
                    GameObject part;
                    for (int j = 0; j < PartManager.listParts[temp - 1].Count; j++)
                    {
                        // Spawn part
                        part = Instantiate(PartManager.listParts[temp - 1][j], partContainers[j].transform.position, Quaternion.Euler(new Vector3(0, 0, 25))) as GameObject;

                        // Parent part to container
                        part.transform.parent = partContainers[j].transform;
                    }
                }
            }

            // Mouse over Button
            if (hit.collider.tag == "BuilderButton")
            {
                // Click button
                if (Input.GetMouseButtonDown(0))
                {
					switch (hit.collider.name) {

	                    // OK
						case "BtnFight" :
							print ("Begin combat");
						break;

	                    // BACK
						case "BtnBack" :
							print ("Boss selection");
						break;
					}
                }
            }
        }
        else
        {
            for (int i = 0; i < partContainers.Length; i++)
            {
                // Stop rotating and switch light off
                if (partContainers[i].transform.childCount > 0)
                {
                    partContainers[i].transform.GetChild(0).GetComponent<Rotation>().isRotating = false;
                    partContainers[i].transform.GetChild(0).GetComponent<BuilderPartBehavior>().renderer.material = partContainers[i].transform.GetChild(0).GetComponent<BuilderPartBehavior>().matBasic;
                }
            }
        }
    }

    // Unselect every tabs
    private void UnselectTabs()
    {
        Color tempColor = new Color(0.25f, 0.25f, 0.25f, 0.25f);

        for (int i = 0; i < tabs.Length; i++)
        {
            tabs[i].guiTexture.color = tempColor;
        }
    }

    // Unselect tabs except choosen one
    private void UnselectTabs(int tab)
    {
        Color tempColorOff = new Color(0.25f, 0.25f, 0.25f, 0.25f);
        Color tempColorOn = new Color(0.5f, 0.5f, 0.5f, 0.5f);

        for (int i = 0; i < tabs.Length; i++)
        {
            if (tab == i)
            {
                tabs[i].guiTexture.color = tempColorOn;
            }
            else
            {
                tabs[i].guiTexture.color = tempColorOff;
            }
        }
    }
}
