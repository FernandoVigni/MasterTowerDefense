using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenURL : MonoBehaviour
{
    public string url = "https://www.linkedin.com/in/fernando-daniel-vigni-adanalian-b091a9226/"; // Reemplaza con tu perfil de LinkedIn

    public void OpenLink()
    {
        Application.OpenURL(url);
    }
}
