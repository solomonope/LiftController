/*  Author:     Folorunsho Solomon Opeyemi(1148183)
 *  Company:    The University Of Birmingham
 *  CodeFileName:ElevatorCar.cs
 *  ClassName:   ElevatorCar         
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
    ///An Object to describe the elevatorcar behaviour
    ///</summary>
    public class ElevatorCar
    {
        #region privatefield
        //buttons in the elevator car
        private ElevatorCarButton m_ElevatorCarButton0;
        private ElevatorCarButton m_ElevatorCarButton1;
        private ElevatorCarButton m_ElevatorCarButton2;
        private ElevatorCarButton m_ElevatorCarButton3;
        #endregion

        #region Properties
        //public properties to the elevator buttons
        public ElevatorCarButton ElevatorCarButton0
        {
            get
            {
                return m_ElevatorCarButton0;
            }
            set
            {
                m_ElevatorCarButton0 = value;
            }
        }
        public ElevatorCarButton ElevatorCarButton1
        {
            get
            {
                return m_ElevatorCarButton1;
            }
            set
            {
                m_ElevatorCarButton1 = value;
            }
        }
        public ElevatorCarButton ElevatorCarButton2
        {
            get
            {
                return m_ElevatorCarButton2;
            }
            set
            {
                m_ElevatorCarButton2 = value;
            }
        }
        public ElevatorCarButton ElevatorCarButton3
        {
            get
            {
                return m_ElevatorCarButton3;
            }
            set
            {
                m_ElevatorCarButton3 = value;
            }
        }
    }
        #endregion
}
