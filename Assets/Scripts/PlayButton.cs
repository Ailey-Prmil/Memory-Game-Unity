using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class PlayButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() =>
        {
            SceneTransitionManager.Instance.StartGame();
        });

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
