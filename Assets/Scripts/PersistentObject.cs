using UnityEngine;

namespace Assets.Scripts
{
    public class PersistentObject : MonoBehaviour
    {
        void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}