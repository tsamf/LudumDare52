using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

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
        Type1   = 0,
        Type2   = 1,
        Type3   = 2,
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
        DeadBody = 4,
    }

    public enum ConveyorBeltMotionStatus
    {
        None = 0,
        Active = 1,
        Paused = 2,
    }

    public static Random test()
    {
       return new Random();       
    }
}
