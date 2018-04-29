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
        StartCoroutine(PostText());

        StartCoroutine(GetText());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator PostText()
    {
        var doc = new XmlDocument();
        doc.Load(XML_FILE_PATH);

        string xml = doc.OuterXml;

        string url = "http://astralqueen.bw.edu/hci/experimentData.php";

        WWWForm form = new WWWForm();
        form.AddField("XMLData", "xml=" + xml);

        using (var w = UnityWebRequest.Post(url, form))
        {
            yield return w.SendWebRequest();
            if (w.isNetworkError || w.isHttpError)
            {
                print(w.error);
            }
            else
            {
                print("Finished Uploading XML");
            }
        }


    }

    IEnumerator GetText()
    {
        Debug.Log("Recieving HTTP Request");
        UnityWebRequest www = UnityWebRequest.Get("http://astralqueen.bw.edu/hci/experimentData.php");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            Debug.Log("Results:");
            Debug.Log(www.downloadHandler.text);
            //Debug.Log(www.downloadHandler.text);
            //Debug.Log(www.ToString());

            // Or retrieve results as binary data
            //byte[] results = www.downloadHandler.data;
        }
    }
}
