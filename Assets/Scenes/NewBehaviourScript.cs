using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {
    public GameObject[] levels;
    private Camera mainCamera;
    private Vector2 screenBounds;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = gameObject.GetComponent<Camera>();
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
foreach(GameObject obj in levels)
        {
            loadChildObjects(obj);
        }

  }


    void loadChildObjects(GameObject obj)
    {
        Debug.Log(obj.name);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
