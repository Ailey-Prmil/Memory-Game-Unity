//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;

//public class CloudTransition : MonoBehaviour
//{
//    public Animator animator;

//    [SerializeField] public float TransitionTime;

//    public void Start()
//    {

//    }

//    public void Update()
//    {
//        if (Input.GetMouseButton(0))
//        {
//            LoadNextLevel();
//        }

//    }

//    public void LoadNextLevel()
//    {
//        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
//    }

//    IEnumerator LoadLevel(int levelIndex)
//    {
//        animator.SetTrigger("Start");
//        yield return new WaitForSeconds(TransitionTime);
//        SceneManager.LoadScene(levelIndex);

//    }
//}
using Assets.Scripts.Command;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CloudTransition : MonoBehaviour
{
    public Animator animator;
    [SerializeField] public float TransitionTime;

    private ICommand _loadLevelCommand;

    void Start()
    {
        _loadLevelCommand = new LoadLevelCommand(this, SceneManager.GetActiveScene().buildIndex + 1);
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            _loadLevelCommand.Execute();
        }
    }

    public IEnumerator LoadLevel(int levelIndex)
    {
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(TransitionTime);
        SceneManager.LoadScene(levelIndex);
    }
}
