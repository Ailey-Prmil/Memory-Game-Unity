using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Interfaces
{
    public interface ISubject
    {
        public void AddObserver(IObserver observer);
        public void RemoveObserver(IObserver observer);
        public void NotifyObservers(MonoBehaviour publisher, object eventType);
    }
}
