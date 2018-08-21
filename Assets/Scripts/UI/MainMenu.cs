using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public Button startExperiment;
    public Button quit;

    public GameObject httpServer;
    public GameObject trialIDInput;

    // Use this for initialization
    void Start () {
        //Button for LoadData

        Button btn = quit.GetComponent<Button>();
        btn.onClick.AddListener(QuitExperiment);

        btn = startExperiment.GetComponent<Button>();
        btn.onClick.AddListener(StartExperiment);
    }

    void StartExperiment()
    {
        int accessCode;
        if (int.TryParse(trialIDInput.GetComponent<Text>().text, out accessCode))
        {
            StartCoroutine(httpServer.GetComponent<HTTPServer>().GetText(accessCode, () => {
                if (httpServer.GetComponent<HTTPServer>().invalidAccessCode == false)
                {
                    SceneManager.LoadScene("Client");
                }
                else
                {
                    Debug.Log("That was an invalid access code");
                }

            }));
            //Debug.Log("Data for Access code: " + accessCode + response.ToString());
        }
    }

    void QuitExperiment()
    {
        Application.Quit();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }


    }
}
