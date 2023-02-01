using System;

namespace DissassemblyLine.Interfaces
{
    /// <summary>
    /// Any object which spawns and moves on the conveyor belt needs to extends from this.
    /// </summary>
    public interface IMoveable
    {
        public Action<IMoveable> OnObjectDisabled
        {
            get;
            set;
        }

        public int ConveyorBeltID
        {
            get;
            set;
        }

        public void MoveOnBelt(float speed);
    }
}
