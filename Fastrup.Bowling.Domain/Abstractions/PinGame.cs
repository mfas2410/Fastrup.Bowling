using Fastrup.Bowling.Domain.Model;
using Fastrup.Bowling.Domain.Model.Player;
using System.Collections.Generic;
using System.Linq;

namespace Fastrup.Bowling.Domain.Abstractions
{
    public abstract class PinGame
    {
        protected PinGame(Id id, IEnumerable<Player> players)
        {
            Id = id;
            PlayerIds = players.Select(x => x.Id);
        }

        public Id Id { get; }

        public IEnumerable<Id> PlayerIds { get; }

        public abstract bool IsComplete { get; protected set; }

        public abstract void AddRoll(PinRoll pinRoll);
    }
}