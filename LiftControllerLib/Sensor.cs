/*  Author:     Folorunsho Solomon Opeyemi(1148183)
 *  Company:    The University Of Birmingham
 *  CodeFileName:Sensor.cs
 *  ClassName:   Sensor         
 *  Description: A class to describe the behaviour of a sensor
 *  
 * **/

using System;
using UoB.LiftControllerLib.Events;

namespace UoB.LiftControllerLib
{
    ///<summary>
    ///Class to act as a sensor object
    ///</summary>
    public class Sensor
    {
        #region privatefields
        //open=0 closed = 1
        private Boolean m_IsSensorSwitchOpen;
        //floor number to indicate the floor the sensor is present
        private int m_FloorNo;
        private event SensorSwitchChangeHandler m_SensorSwitchChangeEvent;
        #endregion
        #region  Properties
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
        ///Event for Sensor Switch change
        ///</summary>
        public event SensorSwitchChangeHandler SensorSwitchChangeEvent
        {
            add
            {
                m_SensorSwitchChangeEvent += value;
            }

            remove
            {
                m_SensorSwitchChangeEvent -= value;
            }

        }

        ///<summary>
        ///to Indicate if the switch is open or closed
        ///</summary>
        public Boolean IsSensorSwitchOpen
        {
            get
            {
                return m_IsSensorSwitchOpen;
            }
            set
            {
                //Checking to see if the set value to true because the event would only fire when true
                if (value == true)
                {
                    m_IsSensorSwitchOpen = value;
                    //Ensuring that there is a subcriber to this event before firing
                    if (m_SensorSwitchChangeEvent != null)
                    {
                        m_SensorSwitchChangeEvent(null, this);
                    }

                }
                    //false so normal setting only
                else
                {
                    m_IsSensorSwitchOpen = value;
                }
            }
        }
        #endregion
    }
}
