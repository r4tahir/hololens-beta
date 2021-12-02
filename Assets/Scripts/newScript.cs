using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class newScript : MonoBehaviour
{
    public Text txt;
    public Master MasterScript;

    // Start is called before the first frame update
    void Start()
    {
        GameObject MasterObj = GameObject.Find("MasterClass");
        txt = gameObject.GetComponent<Text>();
        MasterScript = MasterObj.GetComponent<Master>();
        txt.text = "asdf";
    }

    // Update is called once per frame
    void Update()
    {
        txt.text = MasterScript.text1;
    }
}
