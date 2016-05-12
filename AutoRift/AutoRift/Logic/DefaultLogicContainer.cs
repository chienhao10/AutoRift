using System;
using System.Collections.Generic;
using AutoRift.Data;

namespace AutoRift.Logic
{
    public abstract class DefaultLogicContainer : ILogic
    {
        public Status Status { get; set; }

        protected DefaultLogicContainer()
        {
            Status = new Status();
        }
        public abstract MovementData GetMovementData();
        public abstract void Update();

        public virtual bool Finished()
        {
            return false;
        }

        public virtual void Init() { }
        public virtual void Start() { }
        public virtual void End() { }

        public virtual Status StatusMessages()
        {
            return Status;
        }

        
        
    }
}