using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DissassemblyLine.Interfaces 
{
    /// <summary>
    /// Any object which gets grind by the grinder needs to extends from this.
    /// </summary>
    public interface IGrindable
    {
        public void OnGrind();
    }
}
