using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Xml;
using UnityEngine.Networking;
using Assets.Scripts.Output;
using System;

public class HTTPServer : MonoBehaviour
{
    private const string XML_FILE_PATH = @"Assets/Data/Experiment.xml";

    Queue<TrialDataModel> trialQueue = new Queue<TrialDataModel>();

    // Use this for initialization
    void Start()
    {

    }

    public IEnumerator GetText(int accessCode, Action callback)
    {
        Debug.Log("Recieving HTTP Request");
        UnityWebRequest www = UnityWebRequest.Get("http://astralqueen.bw.edu/hci/experimentData.php?id=" + accessCode);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            trialQueue = new Queue<TrialDataModel>();
            Debug.Log(www.downloadHandler.text);
            parseXML(www.downloadHandler.text);
            callback.Invoke();
        }
    }

    private void parseXML(string xml)
    {
        try
        {
            int trialCount = 1;
            var doc = new XmlDocument();
            doc.LoadXml(xml);
            XmlNodeList xmlTrials = doc.SelectNodes("/experiment/trial");
            foreach (XmlNode xmlTrial in xmlTrials)
            {

                // LOAD XML DOCUMENT 
                XmlNode ship = xmlTrial.SelectSingleNode("ship");
                XmlNode asteroid = xmlTrial.SelectSingleNode("asteroid");

                // Ship
                float shipSpawnX = float.Parse(ship.SelectSingleNode("spawn").Attributes["x"].InnerText);
                float shipSpawnY = float.Parse(ship.SelectSingleNode("spawn").Attributes["y"].InnerText);
                float shipSpawnZ = float.Parse(ship.SelectSingleNode("spawn").Attributes["z"].InnerText);

                bool shipCanMove = bool.Parse(ship.Attributes["canMove"].Value);
                bool shipCanRotate = bool.Parse(ship.Attributes["canRotate"].Value);

                float shipMoveSpeed = float.Parse(ship.Attributes["moveSpeed"].Value);
                float shipRotSpeed = float.Parse(ship.Attributes["rotationSpeed"].Value);

                // Asteroid
                XmlNode spawnPoint = asteroid.SelectSingleNode("spawn");
                float asteroidSpawnX = float.Parse(spawnPoint.Attributes["x"].InnerText);
                float asteroidSpawnY = float.Parse(spawnPoint.Attributes["y"].InnerText);
                float asteroidSpawnZ = float.Parse(spawnPoint.Attributes["z"].InnerText);

                float asteroidMoveX = float.Parse(asteroid.Attributes["movementX"].Value);
                float asteroidMoveY = float.Parse(asteroid.Attributes["movementY"].Value);

                float asteroidRotSpeed = float.Parse(asteroid.Attributes["rotationSpeed"].Value);

                // LOAD DATAMODELS WITH INFORMATION FROM XML
                // Trial
                TrialDataModel tempTrial = new TrialDataModel();

                // Trial Ship
                tempTrial.ShipSpawnX = shipSpawnX;
                tempTrial.ShipSpawnY = shipSpawnY;

                tempTrial.ShipMove = shipCanMove;
                tempTrial.ShipRotate = shipCanRotate;

                tempTrial.ShipMoveSpeed = shipMoveSpeed;
                tempTrial.ShipRotateSpeed = shipRotSpeed;

                // Trial Asteroid
                tempTrial.AsteroidSpawnX = asteroidSpawnX;
                tempTrial.AsteroidSpawnY = asteroidSpawnY;

                tempTrial.AsteroidMovementX = asteroidMoveX;
                tempTrial.AsteroidMovementY = asteroidMoveY;

                tempTrial.AsteroidRotation = asteroidRotSpeed;

                // Trial Specific Information
                tempTrial.TrialID = trialCount;
                trialCount++;

                // Add TrialDataModel To List
                //trialQueue.Enqueue(tempTrial);
                xmlData.TrialQueue.Enqueue(tempTrial);
            }
        }
        catch (XmlException e)
        {
            Debug.Log(e.GetType().ToString() + e.Message);
        }

    }
}
