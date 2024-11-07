using System;
using System.Collections.Generic;

namespace Assets.Scripts.Interfaces
{
    public interface ICardObserver : IObserver
    {
        void OnCardFlipped(Card card);
    }
}
