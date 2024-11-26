using UnityEngine;

namespace Assets.Scripts.Objects
{
    public class PersistentObject : MonoBehaviour
    {
        void Awake()
        {
            if (FindObjectsOfType<CloudTransition>().Length > 1)
            {
                Destroy(gameObject);
            }
            else
            {
                DontDestroyOnLoad(this);
            }
        }
    }
}