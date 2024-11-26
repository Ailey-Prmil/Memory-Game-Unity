using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public enum FunctionType
    {
        Restart,
        Quit,
    }
    public class MessagePanel : MonoBehaviour
    {
        public Button AcceptButton;
        public Button DeclineButton;
        public TMP_Text Message;
        private FunctionType function;
        public FunctionType Function
        {
            get
            {
                return function;
            }
            set
            {
                function = value;
                Message.text = function + "?";
            }
        }

        void Awake()
        {
            AcceptButton.onClick.AddListener(OnAccept);
            DeclineButton.onClick.AddListener(OnDecline);
        }
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        void OnAccept()
        {
            if (Function == FunctionType.Restart)
            {
                // RestartGame();
                PauseSettingPanel.Instance.RestartGame();
            }
            else if (Function == FunctionType.Quit)
            {
                PauseSettingPanel.Instance.QuitGame();
            }
            Destroy(gameObject);
        }

        void OnDecline()
        {
            PauseSettingPanel.Instance.mainPanel.SetActive(true);
            Destroy(gameObject);
            
        }
    }
}