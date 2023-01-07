using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LDEnums 
{
    public enum OrgansType
    {
        None  = 0,
        Heart = 1,
        brain = 2,
        lungs = 3,
        leg   = 4
    }

    public enum BodyType
    {
        None    = 0,
        Type1   = 1,
        Type2   = 2,
        Type3   = 3,
    }


    public enum Tools
    {
        None    = 0,
        Saw     = 1,
        scoop   = 2,
        Drill   = 3,
        scalpel = 4
    }

    public enum Interactable
    {
        None = 0,
        AC = 1,
        ConveyorBelt = 2,
        FuseBox = 3,
    }

    public enum ConveyorBeltMotionStatus
    {
        None = 0,
        Active = 1,
        Paused = 2,
    }
}
