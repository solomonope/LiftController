/*  Author:     Folorunsho Solomon Opeyemi(1148183)
 *  Company:    The University Of Birmingham
 *  CodeFileName:Delegates.cs
 *  ClassName:      
 *  Description:
 *  This Codefile holds the declaration of all the Delegates that would used within the EventGenerator Project
 * **/

using System;
using UoB.EventGenerator.Enumerations;
namespace UoB.EventGenerator.Delegates
{

    ///<summary>
    ///The Delegate to handle events fired  to update the liftpanel in the UI with its top property Async
    ///<param name="EventArgs" >The Arguments required to service this event</param>
    ///<param name="Sender" > The Sending object of this event</param>
    ///<returns>void</returns>
    ///</summary>
    ///
    public delegate void EventCountChangeHandler(int Count);

    ///<summary>
    ///The Delegate to handle events fired  to update the liftpanel in the UI with its top property Async
    ///<param name="EventArgs" >The Arguments required to service this event</param>
    ///<param name="Sender" > The Sending object of this event</param>
    ///<returns>void</returns>
    ///</summary>
    ///
    public delegate void LiftControllerEventHandler(Event Event);

}