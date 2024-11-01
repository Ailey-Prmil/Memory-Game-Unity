using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace Assets.Scripts.Command
{
    internal class LoadLevelCommand : ICommand
    {
        private int _levelIndex;
        private CloudTransition _cloudTransition;

        public LoadLevelCommand(CloudTransition cloudTransition, int levelIndex)
        {
            _cloudTransition = cloudTransition;
            _levelIndex = levelIndex;
        }

        public void Execute()
        {
            _cloudTransition.StartCoroutine(_cloudTransition.LoadLevel(_levelIndex));

        }

        public void Undo()
        {
        }
    }
}
