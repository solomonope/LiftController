/*  Author:     Folorunsho Solomon Opeyemi(1148183)
 *  Company:    The University Of Birmingham
 *  CodeFileName:ElevatorCarButton.cs
 *  ClassName:   ElevatorCarButton         
 *  Description: 
 *  
 * **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UoB.LiftControllerLib.Events;

namespace UoB.LiftControllerLib
{
    ///<summary>
    ///A  specialized sensor 
    ///</summary>
   public class ElevatorCarButton:Sensor
   {
       #region PrivateFields
       //the floor number the button respresents
       private int m_FloorNo;
       //the button press event
       private event ElevatorCarButtonSwitchChangeHandler m_ElevatorCarButtonSwitchChangeEvent;
       #endregion

       #region Properties
       ///<summary>
       ///public property of the press event
       ///</summary>
       public event ElevatorCarButtonSwitchChangeHandler ElevatorCarButtonSwitchChangeEvent
       {
           add
           {
               m_ElevatorCarButtonSwitchChangeEvent += value;
           }

           remove
           {
               m_ElevatorCarButtonSwitchChangeEvent -= value;
           }
       }
       ///<summary>
       ///public property for  the floor number
       ///</summary>
       public int FloorNo
       {
           get
           {
               return m_FloorNo;
           }
           set
           {
               m_FloorNo = value;
           }
       }

       ///<summary>
       ///to Indicate if the switch is open or closed
       ///</summary>
       public new Boolean IsSensorSwitchOpen
       {
           get
           {
               //return from the base object
               return base.IsSensorSwitchOpen;
           }
           set
           {
               //firing event only when true
               if (value == true)
               {
                   base.IsSensorSwitchOpen = value;

                   //checking if there is atleast one refrence to the event
                   if (m_ElevatorCarButtonSwitchChangeEvent != null)
                   {
                       //fire event pass the ElevatorCarButton as Argument
                       m_ElevatorCarButtonSwitchChangeEvent(null, this);
                   }
               }
               else
               {
                   base.IsSensorSwitchOpen = value;
               }
           }
       }

       #endregion
   }
}
