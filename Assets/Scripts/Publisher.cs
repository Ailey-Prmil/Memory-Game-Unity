using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts
{
    public class Publisher:ISubject
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
