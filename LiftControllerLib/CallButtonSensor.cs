/*  Author:     Folorunsho Solomon Opeyemi(1148183)
 *  Company:    The University Of Birmingham
 *  CodeFileName:CallButtonSensor.cs
 *  ClassName:   CallButtonSensor         
 *  Description: A  specialized sensor 
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
    public class CallButtonSensor:Sensor
    {

        #region privatefield
        //present floor of the button
        private int m_FloorNo;
        //the direction the button is representing
        private Direction m_Direction;
        //event called when button is pressed
        private event CallButtonSwitchChangeHandler m_CallButtonSwitchChangeEvent;
        #endregion

        #region Properties
        ///<summary>
        ///public property for  event
        ///</summary>
        public event CallButtonSwitchChangeHandler CallButtonSwitchChangeEvent
        {
            add
            {
                m_CallButtonSwitchChangeEvent += value;
            }

            remove
            {
                m_CallButtonSwitchChangeEvent -= value;
            }
        }

        ///<summary>
        ///public property for floor number
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
        ///public property for  direction
        ///</summary>
        public Direction Direction
        {
            get
            {
                return m_Direction;
            }
            set
            {
                m_Direction = value;
            }
        }
        ///<summary>
        ///public property for  the status of the button
        ///</summary>
        public new Boolean IsSensorSwitchOpen
        {
            get
            {
                return base.IsSensorSwitchOpen;
            }
            set
            {
                //check if value is true before firing the event
                if (value == true)
                {
                    base.IsSensorSwitchOpen = value;
                    //checking that there is @least one refrence to this delegate before firing the event
                    if (m_CallButtonSwitchChangeEvent != null)
                    {
                        m_CallButtonSwitchChangeEvent(null, this);
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
