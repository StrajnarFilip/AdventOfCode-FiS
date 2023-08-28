using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fprog.Algorithms.Common.Structures;

namespace Day16
{
    public class TraversingEntity
    {
        private int _timeLeft;

        public Valve CurrentValve { get; }
        public string Name { get; }
        public int TimeLeft => _timeLeft;

        public TraversingEntity(string name, int initialTime, Valve currentValve)
        {
            this.Name = name;
            this._timeLeft = initialTime;
            CurrentValve = currentValve;
        }

        public bool HasTimeToMove(int minutes) => _timeLeft >= minutes;

        public TraversingEntity MakeMoves(List<Edge<Valve>> moves) =>
            new TraversingEntity(
                this.Name,
                this.TimeLeft - Convert.ToInt32(moves.Sum(e => e.Weight)),
                moves.Last().To
            );

        public TraversingEntity OpenAndMakeMoves(List<Edge<Valve>> moves) =>
            new TraversingEntity(
                this.Name,
                this.TimeLeft - Convert.ToInt32(moves.Sum(e => e.Weight)) - 1,
                moves.Last().To
            );

        public bool OutOfTime() => _timeLeft < 1;
    }
}
