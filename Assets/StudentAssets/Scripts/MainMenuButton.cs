using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;

public class MainMenuButton : NetworkBehaviour {
    [SerializeField]
    private NetworkManager _manager;
	// Use this for initialization
	void Start () {
        gameObject.GetComponent<Button>().onClick.AddListener(BtnClick);
        _manager = NetworkManager.singleton;
	}
	void BtnClick()
    {
        if (NetworkSettings.isServer)
        {
            _manager.StopHost();
        }
        else
        {
            _manager.StopClient();
        }
        Destroy(_manager.gameObject);
        SceneManager.LoadScene("MainMenu");
    }
    
	// Update is called once per frame
	void Update () {
		
	}
}
