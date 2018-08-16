using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class NetworkScript : MonoBehaviour {

    // Use this for initialization
    [SerializeField]
    private Button _createServerBtn;
    [SerializeField]
    private Button _joinServerBtn;

    [SerializeField]
    private InputField _serverAdressTextBox;

    [SerializeField]
    private Button _showClientUIBtn;

    [SerializeField]
    private Button _hideClientUIBtn;


    void Start () {
        _createServerBtn.onClick.AddListener(ServerBtnClick);
        _joinServerBtn.onClick.AddListener(ClientBtnClick);
        _hideClientUIBtn.onClick.AddListener(HideClientUI);
        _showClientUIBtn.onClick.AddListener(ShowClientUI);
	}
    void HideClientUI()
    {
        _createServerBtn.gameObject.SetActive(true);
        _showClientUIBtn.gameObject.SetActive(true);
        _hideClientUIBtn.gameObject.SetActive(false);
        _serverAdressTextBox.gameObject.SetActive(false);
        _joinServerBtn.gameObject.SetActive(false);
    }

    void ShowClientUI()
    {
        _createServerBtn.gameObject.SetActive(false);
        _showClientUIBtn.gameObject.SetActive(false);
        _hideClientUIBtn.gameObject.SetActive(true);
        _serverAdressTextBox.gameObject.SetActive(true);
        _joinServerBtn.gameObject.SetActive(true);
    }

    void ClientBtnClick()
    {
        NetworkSettings.IP = _serverAdressTextBox.text;
        NetworkSettings.isServer = false;
        SceneManager.LoadSceneAsync("TankTutorial");
    }
	void ServerBtnClick()
    {
        NetworkSettings.isServer = true;
        SceneManager.LoadSceneAsync("TankTutorial");
    }
	// Update is called once per frame
	void Update () {
		
	}
}

public static class NetworkSettings
{
    public static string IP
    {
        get;
        set;
    }
    public static bool isServer
    {
        get;
        set;
    }
}
