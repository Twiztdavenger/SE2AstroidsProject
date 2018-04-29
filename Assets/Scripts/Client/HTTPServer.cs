using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Xml;
using UnityEngine.Networking;

public class HTTPServer : MonoBehaviour {

    private const string XML_FILE_PATH = @"Assets/Data/Experiment.xml";

    // Use this for initialization
    void Start () {
        StartCoroutine(GetText());
    }

    IEnumerator GetText()
    {
        Debug.Log("Recieving HTTP Request");
        UnityWebRequest www = UnityWebRequest.Get("http://astralqueen.bw.edu/hci/experimentData.php?id=861");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            xmlData.Data = www.downloadHandler.text;
        }
    }
}
