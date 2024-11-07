using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Interfaces
{
    public interface IGameObserver: IObserver
    {
        public void OnGameStart();
        public void OnGameEnd();
        public void OnGamePause();
        public void OnGameResume();
    }
}
