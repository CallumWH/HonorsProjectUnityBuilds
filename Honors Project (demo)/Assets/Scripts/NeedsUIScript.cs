using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedsUIScript : MonoBehaviour
{

    public NeedsMonitor needsMontor;

    private TextMesh textMesh;
    public List<string> textUI = new List<string>();

    // Use this for initialization
    void Start()
    {
        textMesh = gameObject.GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < needsMontor.GetListSize(); i++)
        {
            textUI[i] = needsMontor.currentNeeds[i].needName + ":" + string.Format("{0:0.0}", needsMontor.currentNeeds[i].GetValue());
        }

        textMesh.text = string.Join("\n",textUI.ToArray());
    }

    public void addToList()
    {
        for (int i = 0; i < needsMontor.GetListSize(); i++)
        {
            textUI.Add("Debug");
        }
    }
}