using System;
using System.Collections.Generic;

namespace Assets.Scripts.Interfaces
{
    public interface ICardObserver
    {
        void OnCardFlipped(Card card);
    }
}
