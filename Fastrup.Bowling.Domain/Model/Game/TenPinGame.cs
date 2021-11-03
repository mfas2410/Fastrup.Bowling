using System.Collections.Generic;
using System.Linq;
using Fastrup.Bowling.Domain.Abstractions;
using Fastrup.Bowling.Domain.Events;
using Fastrup.Bowling.Domain.Exceptions;

namespace Fastrup.Bowling.Domain.Model.Game
{
    public sealed class TenPinGame : PinGame
    {
        private readonly IEventRegister _eventRegister;
        private readonly List<PinFrame> _frames = new();
        private readonly int _numberOfFrames = 10;

        private TenPinGame(IEventRegister eventRegister) => _eventRegister = eventRegister;

        public override bool IsComplete { get; protected set; }

        public override void AddRoll(PinRoll pinRoll)
        {
            if (IsComplete) throw new GameException("The game has ended");
            if (_frames.LastOrDefault() is null || _frames.Last().IsComplete) _frames.Add(TenPinFrame.Create(_frames.Count == _numberOfFrames - 1, _eventRegister));
            _frames.Last().AddRoll(pinRoll, _eventRegister);
            IsComplete = _frames.Count == _numberOfFrames && _frames.Last().IsComplete;
            if (IsComplete) _eventRegister.RegisterEvent(new GameCompletedEvent(this));
        }

        public static TenPinGame Create(IEventRegister eventRegister)
        {
            var tenPinGame = new TenPinGame(eventRegister);
            eventRegister.RegisterEvent(new GameCreatedEvent(tenPinGame));
            return tenPinGame;
        }
    }
}