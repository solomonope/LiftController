/*  Author:     Folorunsho Solomon Opeyemi(1148183)
 *  Company:    The University Of Birmingham
 *  CodeFileName:Enumeration.cs
 *  ClassName:      
 *  Description:
 *  This Codefile holds the declaration of all the Enumerations that would used within the LifControllerLib Project
 * **/

using System;
namespace UoB.LiftControllerLib
{
    ///<summary>
    ///Constants to the present direction of the elevator car
    ///</summary>
    public enum Direction
    {
        NotSet = 0,
        Up,
        Down,
        Stationary,
        DestinationStop
    }

    ///<summary>
    ///Constants to the present speed of the elevator car
    ///</summary>
    public enum LiftSpeed
    {
        NotSet = 0,
        One,
        Two,
        Three,
        Four,
        Five
    };

}