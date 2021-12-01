using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class algoCOR : MonoBehaviour
{
    public Vector3 pos;
    public Vector3 COR;
    public List<Vector3> vecList;

    public List<Vector3> listCOR;
    int counter;
    int i;

    // Start is called before the first frame update
    void Start()
    {
        Resources playerScript = GetComponent<Resources>();

        while (i < vecList.Count)
        {
            listCOR[counter] = Resources.CenterOfRotation(vecList[i], vecList[i + 1], vecList[i + 2], vecList[i + 3]);

            i += 4;
            counter++;
        }

        Vector3 sum = new Vector3(0,0,0);

        for (int j = 0; j < listCOR.Count; j++)
        {
            sum += listCOR[j];
        }

        COR = sum / listCOR.Count;

        Debug.Log(COR);

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
