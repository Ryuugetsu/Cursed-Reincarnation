using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartupManager : MonoBehaviour {
    
    // Use this for initialization
    private IEnumerator start() {
        while (!LocalizationManager.instance.GetIsRead())
        {
            yield return null;
        }
        SceneManager.LoadScene(0);
	}
}
