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

	private GameObject builderPreview;
    private GameObject buttonOK;
    private GameObject buttonBack;

	private int partType;

	// Use this for initialization
	void Start ()
    {
        PartManager = GameObject.Find("_PartManager").GetComponent<_PartManager>();

		builderPreview = GameObject.Find("BuilderPreview");

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

					// get clic on part
					if (Input.GetMouseButtonDown(0)) {

						// list parts currently set to weapon
						for (int i = 0; i < builderPreview.transform.childCount; i++) {

							// delete currenty set clicked part if found
							if (builderPreview.transform.GetChild(i).name.Split('_')[2] == (partType + 1).ToString()) {
								Destroy(builderPreview.transform.GetChild(i).gameObject);
							}
						}

						// Calculate new part's coordinates
						float x = 0;
						float y = 0;
						switch(partType) {
							case 0:	x = -1;	y = 0;	break; // 1 - Core Part
							case 1:	x = 0;	y = 0;	break; // 2 - Main Canon
							case 2:	x = 1;	y = -.75f;	break; // 3 - Secondary Power
							case 3:	x = 0;	y = .75f;	break; // 4 - Ammunition Type
							case 4:	x = 1;	y = .75f;	break; // 5 - Accessory
						}

						// set part into preview
						GameObject part = Instantiate(Resources.Load ("Prefabs/Parts/" + hit.collider.transform.GetChild(0).name.Replace("(Clone)","") + "_preview"), new Vector3(builderPreview.transform.position.x + x, builderPreview.transform.position.y + y, 0), Quaternion.identity) as GameObject;

						// set instantiation into prefab
						part.transform.parent = builderPreview.transform;
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

            // Mouse over Tab
            if (hit.collider.tag == "BuilderTab")
            {
                // Click tab
                if (Input.GetMouseButtonDown(0))
                {
                    // Unselect everything except current
					if (int.TryParse(hit.collider.name.Substring(0, 1), out partType))
                    {
						partType--;
						UnselectTabs(partType);
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
					for (int j = 0; j < PartManager.listParts[partType].Count; j++)
                    {
                        // Spawn part
						part = Instantiate(PartManager.listParts[partType][j], partContainers[j].transform.position, Quaternion.Euler(new Vector3(0, 0, 25))) as GameObject;

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
