using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    void Start()
    {
        // Obtenez une référence à l'objet de jeu Main Camera
        GameObject mainCameraGameObject = Camera.main.gameObject;

        // Obtenez une référence au composant AudioListener sur la caméra principale
        AudioListener audioListener = mainCameraGameObject.GetComponent<AudioListener>();

        // Vérifiez si l'AudioListener existe
        if (audioListener != null)
        {
            // Assurez-vous que l'objet de jeu Main Camera ne soit pas détruit lors du chargement de nouvelles scènes
            DontDestroyOnLoad(mainCameraGameObject);
        }
        else
        {
            Debug.LogError("No AudioListener found on Main Camera.");
        }
    }

}
