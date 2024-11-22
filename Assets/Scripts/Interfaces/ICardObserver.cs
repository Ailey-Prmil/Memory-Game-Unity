using System;
using System.Collections.Generic;
using Assets.Scripts.Objects;

namespace Assets.Scripts.Interfaces
{
    public interface ICardObserver : IObserver
    {
        void OnCardFlipped(Card card);
    }
}
