/*  Author:     Folorunsho Solomon Opeyemi(1148183)
 *  Company:    The University Of Birmingham
 *  CodeFileName:EventGenerator.cs
 *  ClassName:   EventGenerator         
 *  Description: 
 *  
 * **/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UoB.LiftControllerLib;
using System.Threading;
using UoB.EventGenerator.Enumerations;
using UoB.EventGenerator.Delegates;

namespace UoB.EventGenerator
{
    ///<summary>
    ///An object to handle event generation
    ///</summary>
    public class EventGenerator
    {

        #region privatefield
        //LiftController object 
        private LiftController m_LiftController;
        //Thread so as not to freeze the UI
        private Thread m_SimulatorThread;
        //The rate of event generation
        private EventSpeed m_EventSpeed;

        //event count
        private int m_EventCount;

        #endregion

        #region Events
        public event EventCountChangeHandler    EventCountChange;
        public event LiftControllerEventHandler LifControllerEvent;
        #endregion

        #region Properties

        ///<summary>
        ///public property for the  events counts
        ///</summary>
        public int EventCount
        {
            get
            {
                return m_EventCount;
            }
           private  set
            {
                m_EventCount = value;
                if (EventCountChange != null)
                {
                    EventCountChange(m_EventCount);
                }
            }
        }
        ///<summary>
        ///public property for the speed of events
        ///</summary>
        public EventSpeed EventSpeed
        {
            get
            {
                return m_EventSpeed;
            }
            set
            {
                m_EventSpeed = value;
            }

        }

        ///<summary>
        ///public property to know if the Eventgenerator is running
        ///</summary>
        public bool IsSimulatorRunning
        {
            get
            {
                try
                {
                    return m_SimulatorThread.IsAlive || m_SimulatorThread.ThreadState == ThreadState.WaitSleepJoin ? true : false;
                }
                catch
                {
                    return false;
                }
            }
           
        }

        #endregion

        #region Methods

        ///<summary>
        ///Event generator Constructor
        ///</summary>
        public EventGenerator(LiftController _LiftController)
        {
            m_LiftController = _LiftController;
        }


        ///<summary>
        ///Method call to start generation of events
        ///</summary>
        public void StartEventGenerator()
        {
            try
            {
                if (!IsSimulatorRunning)
                {
                    m_SimulatorThread = new Thread(new ThreadStart(Simulator));
                    m_SimulatorThread.Start();
                }
            }
            finally
            {
            }
        }

        ///<summary>
        ///Method call to stop generation of events
        ///</summary>
        public void StopEventGenerator()
        {
            try
            {
                if (m_SimulatorThread != null)
                {
                    m_SimulatorThread.Abort();
                }
            }
            finally
            {
               // this.EventCount = 0;
                //this.m_LiftController.Stop();
            }
        }
        ///<summary>
        ///Method call to handle the generation of events
        ///it runs on a seperate thread
        ///</summary>
        private void Simulator()
        {
            Event _Event = Event.NotSet;
            Random _Random = new Random();
            int _RandomNumber = 0;
           
            while (true)
            {
                //generate a random number between 1 and 10
                _RandomNumber = _Random.Next(1, 10);
                //Parse the number to the Enum representing to the constant
                _Event = (Event)Enum.Parse(typeof(Event), _RandomNumber.ToString());
                EventCount += 1;
                if (LifControllerEvent != null)
                {
                    LifControllerEvent(_Event);
                }
                switch (_Event)
                {
                    case Event.ThirdFloorDownCallButton:
                        m_LiftController.ThirdFloor.DownCallButtonSensor.IsSensorSwitchOpen = true;
                        
                        Thread.Sleep((int)this.m_EventSpeed * 5000);
                        break;
                    case Event.SecondFloorDownCallButton:
                        m_LiftController.SecondFloor.DownCallButtonSensor.IsSensorSwitchOpen = true;
                        Thread.Sleep((int)this.m_EventSpeed * 5000);
                        break;
                    case Event.SecondFloorUpCallButton:
                        m_LiftController.SecondFloor.UpCallButtonSensor.IsSensorSwitchOpen = true;
                        Thread.Sleep((int)this.m_EventSpeed * 5000);
                        break;
                    case Event.FirstFloorDownCallButton:
                        m_LiftController.FirstFloor.DownCallButtonSensor.IsSensorSwitchOpen = true;
                        Thread.Sleep((int)this.m_EventSpeed * 5000);
                        break;
                    case  Event.FirstFloorUpCallButton:
                        m_LiftController.FirstFloor.UpCallButtonSensor.IsSensorSwitchOpen = true;
                        Thread.Sleep((int)this.m_EventSpeed * 5000);
                        break;
                    case Event.GroundFloorUpCallButton:
                        m_LiftController.GroundFloor.UpCallButtonSensor.IsSensorSwitchOpen = true;
                        Thread.Sleep((int)this.m_EventSpeed * 5000);
                        break;
                    case Event.ElevatorCarButton0:
                        this.m_LiftController.ElevatorCar.ElevatorCarButton0.IsSensorSwitchOpen = true;
                        Thread.Sleep((int)this.m_EventSpeed * 5000);
                        break;
                    case Event.ElevatorCarButton1:
                        this.m_LiftController.ElevatorCar.ElevatorCarButton1.IsSensorSwitchOpen = true;
                        Thread.Sleep((int)this.m_EventSpeed * 5000);
                        break;
                    case Event.ElevatorCarButton2:
                        this.m_LiftController.ElevatorCar.ElevatorCarButton2.IsSensorSwitchOpen = true;
                        Thread.Sleep((int)this.m_EventSpeed * 5000);
                        break;
                    case Event.ElevatorCarButton3:
                        this.m_LiftController.ElevatorCar.ElevatorCarButton3.IsSensorSwitchOpen = true;
                        Thread.Sleep((int)this.m_EventSpeed * 5000);
                        break;
                    case Event.NotSet:
                        break;
                }

                
            }
        }

        #endregion 
    }
}
