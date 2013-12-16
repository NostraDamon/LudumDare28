using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class _PartManager : MonoBehaviour
{
    public List<GameObject>[] listParts = new List<GameObject>[5];
    public List<GameObject> listPartsCore;
    public List<GameObject> listPartsCanon;
    public List<GameObject> listPartsSecondary;
    public List<GameObject> listPartsAmmo;
    public List<GameObject> listPartsAccessory;

    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(this);

        // Load parts in array
        listParts[0] = listPartsCore;
        listParts[1] = listPartsCanon;
        listParts[2] = listPartsSecondary;
        listParts[3] = listPartsAmmo;
        listParts[4] = listPartsAccessory;

        // Go to main scene
        Invoke("GoToMain", 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        // Start game
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Instantiate(Resources.Load("Prefabs/Player"), new Vector3(0, 1, 0), Quaternion.identity);
            Destroy(GameObject.Find("Builder"));

            Screen.lockCursor = true;
            Screen.showCursor = false;
        }

        //if (Input.GetMouseButtonDown(0))
        //{
        //    Screen.lockCursor = true;
        //    Screen.showCursor = false;
        //}

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Screen.showCursor = true;
            Screen.lockCursor = false;
        }
    }

    private void GoToMain()
    {
        Application.LoadLevel("Main");
    }
}
