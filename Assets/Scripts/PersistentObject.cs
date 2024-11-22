using Assets.Scripts.Objects;
using UnityEngine;

namespace Assets.Scripts
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