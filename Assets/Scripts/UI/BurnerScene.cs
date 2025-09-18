using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnerScene : MonoBehaviour
{
    private void Start()
    {
        FindObjectOfType<SceneLoader>().LoadScene(1);
    }
}
