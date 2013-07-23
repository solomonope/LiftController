/*  Author:     Folorunsho Solomon Opeyemi(1148183)
 *  Company:    The University Of Birmingham
 *  CodeFileName:Delegates.cs
 *  ClassName:      
 *  Description:
 *  This Codefile holds the declaration of all the Delegates that would used within the LifControllerLib Project
 * **/

using System;
namespace UoB.LiftControllerLib.Events
{
    ///<summary>
    ///The Delegate to handle events fired when positions Change
    ///<param name="EventArgs" >The Arguments required to service this event</param>
    ///<param name="Sender" > The Sending object of this event</param>
    ///<returns>void</returns>
    ///</summary>
    public delegate void LiftPositionChangeHandler(LiftPositionChangeEventArgs EventArgs, Object Sender);

    ///<summary>
    ///The Delegate to handle events fired when Direction Change
    ///<param name="EventArgs" >The Arguments required to service this event</param>
    ///<param name="Sender" > The Sending object of this event</param>
    ///<returns>void</returns>
    ///</summary>
    

    public delegate void LiftDirectionChangeHandler(LiftDirectionChangeEventArgs EventArgs, Object Sender);

    ///<summary>
    ///The Delegate to handle events fired when Status of a Sensor  Changes
    ///<param name="EventArgs" >The Arguments required to service this event</param>
    ///<param name="Sender" > The Sending object of this event</param>
    ///<returns>void</returns>
    ///</summary>
    ///

    public delegate void SensorSwitchChangeHandler(Object EventArgs, Object Sender);

    ///<summary>
    ///The Delegate to handle events fired when  Elevator Cart button is pushed Change
    ///<param name="EventArgs" >The Arguments required to service this event</param>
    ///<param name="Sender" > The Sending object of this event</param>
    ///<returns>void</returns>
    ///</summary>
    ///

    public delegate void ElevatorCarButtonSwitchChangeHandler(Object EventArgs, Object Sender);


    ///<summary>
    ///The Delegate to handle events fired when Callbutton Change
    ///<param name="EventArgs" >The Arguments required to service this event</param>
    ///<param name="Sender" > The Sending object of this event</param>
    ///<returns>void</returns>
    ///</summary>
    ///

    public delegate void CallButtonSwitchChangeHandler(Object EventArgs, Object Sender);

    ///<summary>
    ///The Delegate to handle events fired  to update the liftpanel in the UI with its top property Async
    ///<param name="EventArgs" >The Arguments required to service this event</param>
    ///<param name="Sender" > The Sending object of this event</param>
    ///<returns>void</returns>
    ///</summary>
    ///
    public delegate void SetTopCallBack(int Top);

}