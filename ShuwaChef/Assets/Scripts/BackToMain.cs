using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToMain : MonoBehaviour
{
    

    public void BackToMainMenu()
    {
        Loader.Load(Loader.Scene.MainMenuScene);
    }
}
