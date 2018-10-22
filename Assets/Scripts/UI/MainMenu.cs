using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public Button startExperiment;
    public Button quit;

    public GameObject ErrorText;

    public GameObject httpServer;
    public GameObject trialIDInput;
    public GameObject partIDInput;

    // Use this for initialization
    void Start () {
        //Button for LoadData

        Button btn = quit.GetComponent<Button>();
        btn.onClick.AddListener(QuitExperiment);

        btn = startExperiment.GetComponent<Button>();
        btn.onClick.AddListener(StartExperiment);

        HTTPServer.InvalidAccessCode += InvalidAccessCode;
    }

    //TODO: HANDLE INVALID ACCESS CODES CORRECTLY AND DONT LET START EXPERIMENT UNTIL BOTH FIELDS ARE ENTERED
    void StartExperiment()
    {
        string participantID = partIDInput.GetComponent<Text>().text;
        int accessCode;
        if (int.TryParse(trialIDInput.GetComponent<Text>().text, out accessCode))
        {
            StartCoroutine(httpServer.GetComponent<HTTPServer>().GetText(accessCode, participantID, () => {
                SceneManager.LoadScene("Client");

            }));
            //Debug.Log("Data for Access code: " + accessCode + response.ToString());
        } else
        {
            InvalidAccessCode("Enter a valid access code");
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
            QuitExperiment();
        }
    }

    void InvalidAccessCode(string errorText)
    {
        ErrorText.SetActive(true);
        ErrorText.GetComponent<Text>().text = errorText;
    }
}
