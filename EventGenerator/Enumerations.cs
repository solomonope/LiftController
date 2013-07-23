/*  Author:     Folorunsho Solomon Opeyemi(1148183)
 *  Company:    The University Of Birmingham
 *  CodeFileName:Enumeration.cs
 *  ClassName:      
 *  Description:
 *  This Codefile holds the declaration of all the Enumerations that would used within the EventGenerator Project
 * **/
using System;

namespace UoB.EventGenerator.Enumerations
{
    ///<summary>
    ///Constants to the present Speed of events
    ///</summary>
    public enum EventSpeed
    {
        NotSet = 0,
        One,
        Two,
        Three,
        Four,
        Five,
        
    }
    ///<summary>
    ///Constants to the present event  randomly generator
    ///</summary>
    public enum Event
    {
        NotSet = 0,
        ThirdFloorDownCallButton,
        SecondFloorDownCallButton,
        SecondFloorUpCallButton,
        FirstFloorDownCallButton,
        FirstFloorUpCallButton,
        GroundFloorUpCallButton,
        ElevatorCarButton0,
        ElevatorCarButton1,
        ElevatorCarButton2,
        ElevatorCarButton3
    }
}