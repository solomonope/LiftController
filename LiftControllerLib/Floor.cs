/*  Author:     Folorunsho Solomon Opeyemi(1148183)
 *  Company:    The University Of Birmingham
 *  CodeFileName:Floor.cs
 *  ClassName:   Floor         
 *  Description: 
 *  
 * **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UoB.LiftControllerLib
{
    ///<summary>
    ///A floor object implementing IComparable so as to make it sortable
    ///</summary>
    public class Floor : IComparable<Floor>
    {
        #region Privatefields
        //number representing the position of the floor 
        private int m_FloorNumber;
        //Height of the floor
        private int m_FloorHeight;
        //callbutton objects of the floor
        private CallButtonSensor m_UpCallButtonSensor;
        private CallButtonSensor m_DownCallButtonSensor;
        //floor sensor
        private Sensor m_FloorSensor;

        #endregion

        public Floor()
        {
           
        }

        #region Properties
        ///<summary>
        ///public property for  the event
        ///</summary>
        public CallButtonSensor UpCallButtonSensor
        {
            get
            {
                return m_UpCallButtonSensor;
            }
            set
            {
                m_UpCallButtonSensor = value;
            }
        }

        ///<summary>
        ///public property for  the event
        ///</summary>
        public CallButtonSensor DownCallButtonSensor
        {
            get
            {
                return m_DownCallButtonSensor;
            }
            set
            {
                m_DownCallButtonSensor = value;
            }
        }
        ///<summary>
        ///public property for  the floor sensor object
        ///</summary>
        public Sensor FloorSensor
        {

            get
            {
                return m_FloorSensor;
            }
            set
            {
                m_FloorSensor = value;
            }
        }
        ///<summary>
        ///public property for  the floor number
        ///</summary>
        public int FloorNumber
        {
            get
            {
                return m_FloorNumber;
            }
            set
            {
                m_FloorNumber = value;
            }
        }
        ///<summary>
        ///public property for  the floor height
        ///</summary>
        public int FloorHeight
        {
            get
            {
                return m_FloorHeight;
            }
            set
            {
                m_FloorHeight = value;
            }
        }
        #endregion

        #region ICompare
        ///<summary>
        ///the implemation method for IComparable interface
        ///<param name="other" >The Floor Object required  </param>
        ///<returns>int</returns>
        ///</summary>
        public int CompareTo(Floor other)
        {
            return this.FloorNumber.CompareTo(other.FloorNumber);
        }
        #endregion
    }
}
