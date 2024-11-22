using System.Collections.Generic;
using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class EventManager:ISubject
    {
        private List<IObserver> observers = new List<IObserver>();

        public void AddObserver(IObserver observer)
        {
            observers.Add(observer);
        }

        public void RemoveObserver(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void NotifyObservers(MonoBehaviour publisher, object eventType)
        {
            foreach (IObserver observer in observers)
            {
                observer.OnNotify(publisher, eventType);
            }

        }
    }
}
