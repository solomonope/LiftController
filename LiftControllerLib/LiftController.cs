/*  Author:     Folorunsho Solomon Opeyemi(1148183)
 *  Company:    The University Of Birmingham
 *  CodeFileName:LiftController.cs
 *  ClassName:   LifController   
 *  Description:
 * **/
using System;
using UoB.LiftControllerLib.Events;
using System.Threading;
using System.Collections.Generic;

namespace UoB.LiftControllerLib
{
    public class LiftController
    {
        #region privatefields

        //lift event delegates
        private event  LiftPositionChangeHandler   m_LiftPositionChangeEvent;
        private event  LiftDirectionChangeHandler m_LiftDirectionChangeEvent;

        //LiftController State Fields
        private Direction m_LiftDirection;
        private LiftSpeed m_LiftSpeed;
        private int m_CurrentPosition;

        //integer values to reprsent the postions
        private int m_TopFloorPosition, m_BottomFloorPosition;

        //Floor Objects
        private Floor m_GroundFloor, m_FirstFloor, m_SecondFloor, m_ThirdFloor;

        private ElevatorCar m_ElevatorCar;
        //sort in acsendin order
        private List<Floor> m_UpDestinations;

        //sort in descendin  order
        private List<Floor> m_DownDestinations;

        private Thread ElevatorServiceThread;
        //An Object for lock in multithreaded scenarios
        private Object m_LockHandle;
        #endregion 

        #region properties
        ///<summary>
        ///public property to Indicate if the Queue is empty
        ///</summary>
        public bool IsUpQueueEmpty
        {
            get
            {
               
                lock (m_LockHandle)
                {
                    if (this.m_UpDestinations.Count > 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
        }
        ///<summary>
        ///public property to Indicate if the Queue is empty
        ///</summary>
        public bool IsDownQueueEmpty
        {
            get
            {
                lock (m_LockHandle)
                {
                    if (this.m_DownDestinations.Count > 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
        }
        ///<summary>
        ///public property for the GroundFloor
        ///</summary>
        public Floor GroundFloor
        {
            get
            {
                return m_GroundFloor;
            }
            set
            {
                m_GroundFloor = value;
            }
        }
        ///<summary>
        ///public property for the FirstFloor
        ///</summary>
        public Floor FirstFloor
        {
            get
            {
                return m_FirstFloor;
            }
            set
            {
                m_FirstFloor = value;
            }
        }
        ///<summary>
        ///public property for the SecondFloor
        ///</summary>
        public Floor SecondFloor
        {
            get
            {
                return m_SecondFloor;
            }
            set
            {
                m_SecondFloor = value;
            }
        }
        ///<summary>
        ///public property for the ThirdFloor
        ///</summary>
        public Floor ThirdFloor
        {
            get
            {
                return m_ThirdFloor;
            }
            set
            {
                m_ThirdFloor = value;
            }
        }
        ///<summary>
        ///public property for the ElevatorCar
        ///</summary>
        public ElevatorCar ElevatorCar
        {
            get
            {
                return m_ElevatorCar;
            }
            set
            {
                m_ElevatorCar = value;
            }
        }
        ///<summary>
        ///public property for the LiftSpeed
        ///</summary>
        public LiftSpeed LiftSpeed
        {
            get
            {
               
                return m_LiftSpeed;
            }
            set
            {
                m_LiftSpeed = value;
            }
        }
        ///<summary>
        ///public property for the LiftDirection
        ///</summary>
        public Direction LiftDirection
        {

            get
            {
                return m_LiftDirection;
            }
            set
            {
                //Check if there is at least one subscriber to this event
                if (m_LiftDirectionChangeEvent != null)
                {
                    if (m_LiftDirection == Direction.Stationary)
                    {
                        m_LiftDirection = value;
                        m_LiftDirectionChangeEvent(null, this);

                    }
                    else
                    {
                        m_LiftDirection = value;
                        m_LiftDirectionChangeEvent(null, this);
                    }

                }
            }
           
           
        }

        ///<summary>
        ///public property for the CurrentPosition
        ///</summary>
        public int CurrentPosition
        {
            get
            {
                return m_CurrentPosition;
            }
            set
            {
                //check if it is a change
                if (m_CurrentPosition != value)
                {
                    //Check if there is atleast one method subscribing to the event
                    if (m_LiftPositionChangeEvent != null)
                    {
                        m_CurrentPosition = value;
                        //Passing the Currentposition
                        m_LiftPositionChangeEvent(new LiftPositionChangeEventArgs() { Top = m_CurrentPosition }, null);
                    }
                }
            }
        }
        ///<summary>
        ///public property for LiftPositionChangeEvent  
        ///</summary>
        public event LiftPositionChangeHandler LiftPositionChangeEvent
        {
            add
            {
                this.m_LiftPositionChangeEvent += value;
            }

            remove
            {
                this.m_LiftPositionChangeEvent -= value;
            }
        }

        ///<summary>
        ///public property for LiftDirectionChangeEvent  
        ///</summary>
        public event LiftDirectionChangeHandler LiftDirectionChangeEvent
        {
            add
            {
                this.m_LiftDirectionChangeEvent += value;
            }

            remove
            {
                this.m_LiftDirectionChangeEvent -= value;
            }
        }
        #endregion




        ///<summary>
        ///The constructor
        ///<param name="initialPosition" >The initial position of the elevator</param>
        ///<param name="top" > The top position of the elevator</param>
        ///<param name="bottom" > The top position of the elevator</param>
        ///<returns>void</returns>
        ///</summary>
        public LiftController(int initialPosition, int top, int bottom)
        {
            //Initialise ElevatorController to initial position
            m_CurrentPosition = initialPosition;
            m_TopFloorPosition = top;
            m_BottomFloorPosition = bottom;
            //intialising the Lock object
            m_LockHandle = new object();

            //initialise the Elevator Thread object adding refrence to the serviceLifRequest method
            this.ElevatorServiceThread = new Thread(new ParameterizedThreadStart(serviceLiftRequests));

            this.ElevatorCar =  new ElevatorCar();

            this.ElevatorCar.ElevatorCarButton0 =  new ElevatorCarButton()
            {
                 FloorNo = 0,
                 IsSensorSwitchOpen = false,   
            };
            this.ElevatorCar.ElevatorCarButton1 =  new ElevatorCarButton()
            {
                 FloorNo = 1,
                 IsSensorSwitchOpen = false,   
            };
            this.ElevatorCar.ElevatorCarButton2 =  new ElevatorCarButton()
            {
                 FloorNo = 2,
                 IsSensorSwitchOpen = false,   
            };
            this.ElevatorCar.ElevatorCarButton3 =  new ElevatorCarButton()
            {
                 FloorNo = 3,
                 IsSensorSwitchOpen = false,   
            };
            // initiliaze events
            this.ElevatorCar.ElevatorCarButton0.ElevatorCarButtonSwitchChangeEvent += new ElevatorCarButtonSwitchChangeHandler(ElevatorCartButtonEventHandler);
            this.ElevatorCar.ElevatorCarButton1.ElevatorCarButtonSwitchChangeEvent += new ElevatorCarButtonSwitchChangeHandler(ElevatorCartButtonEventHandler);
            this.ElevatorCar.ElevatorCarButton2.ElevatorCarButtonSwitchChangeEvent += new ElevatorCarButtonSwitchChangeHandler(ElevatorCartButtonEventHandler);
            this.ElevatorCar.ElevatorCarButton3.ElevatorCarButtonSwitchChangeEvent += new ElevatorCarButtonSwitchChangeHandler(ElevatorCartButtonEventHandler);

            this.m_GroundFloor = new Floor()
            {
                FloorNumber= 0
            };
            m_GroundFloor.DownCallButtonSensor = new CallButtonSensor()
            {
                FloorNo=0,
                Direction= Direction.Down,
                IsSensorSwitchOpen=false
            };

            m_GroundFloor.UpCallButtonSensor = new CallButtonSensor()
            {
                FloorNo = 0,
                Direction = Direction.Up,
                IsSensorSwitchOpen = false
            };


            this.m_FirstFloor = new Floor()
            {
                FloorNumber = 1
            };
            m_FirstFloor.DownCallButtonSensor = new CallButtonSensor()
            {
                FloorNo = 1,
                Direction = Direction.Down,
                IsSensorSwitchOpen = false
            };

            m_FirstFloor.UpCallButtonSensor = new CallButtonSensor()
            {
                FloorNo = 1,
                Direction = Direction.Up,
                IsSensorSwitchOpen = false
            };

            this.m_SecondFloor = new Floor()
            {
                FloorNumber = 2
            };
            m_SecondFloor.DownCallButtonSensor = new CallButtonSensor()
            {
                FloorNo = 2,
                Direction = Direction.Down,
                IsSensorSwitchOpen = false
            };

            m_SecondFloor.UpCallButtonSensor = new CallButtonSensor()
            {
                FloorNo = 2,
                Direction = Direction.Up,
                IsSensorSwitchOpen = false
            };

            this.m_ThirdFloor = new Floor()
            {
                FloorNumber = 3
            };
            m_ThirdFloor.DownCallButtonSensor = new CallButtonSensor()
            {
                FloorNo = 3,
                Direction = Direction.Down,
                IsSensorSwitchOpen = false
            };

            m_ThirdFloor.UpCallButtonSensor = new CallButtonSensor()
            {
                FloorNo = 3,
                Direction = Direction.Up,
                IsSensorSwitchOpen = false
            };

            m_GroundFloor.FloorSensor = new Sensor()
            {
                 IsSensorSwitchOpen= false,
                 FloorNo = 0
            };
            m_FirstFloor.FloorSensor = new Sensor()
            {
                IsSensorSwitchOpen = false,
                FloorNo = 1
            };
            m_SecondFloor.FloorSensor = new Sensor()
            {
                IsSensorSwitchOpen = false,
                FloorNo = 2
            };
            m_ThirdFloor.FloorSensor = new Sensor()
            {
                IsSensorSwitchOpen = false,
                FloorNo = 3
            };

            //initialize events
            m_GroundFloor.FloorSensor.SensorSwitchChangeEvent += new SensorSwitchChangeHandler(FloorSensorEventHandler);
            m_GroundFloor.UpCallButtonSensor.CallButtonSwitchChangeEvent += new CallButtonSwitchChangeHandler(CallButtonEventHandler);
            m_GroundFloor.DownCallButtonSensor.CallButtonSwitchChangeEvent += new CallButtonSwitchChangeHandler(CallButtonEventHandler);

            m_FirstFloor.FloorSensor.SensorSwitchChangeEvent += new SensorSwitchChangeHandler(FloorSensorEventHandler);
            m_FirstFloor.UpCallButtonSensor.CallButtonSwitchChangeEvent += new CallButtonSwitchChangeHandler(CallButtonEventHandler);
            m_FirstFloor.DownCallButtonSensor.CallButtonSwitchChangeEvent += new CallButtonSwitchChangeHandler(CallButtonEventHandler);


            m_SecondFloor.FloorSensor.SensorSwitchChangeEvent += new SensorSwitchChangeHandler(FloorSensorEventHandler);
            m_SecondFloor.UpCallButtonSensor.CallButtonSwitchChangeEvent += new CallButtonSwitchChangeHandler(CallButtonEventHandler);
            m_SecondFloor.DownCallButtonSensor.CallButtonSwitchChangeEvent += new CallButtonSwitchChangeHandler(CallButtonEventHandler);



            m_ThirdFloor.FloorSensor.SensorSwitchChangeEvent += new SensorSwitchChangeHandler(FloorSensorEventHandler);
            m_ThirdFloor.UpCallButtonSensor.CallButtonSwitchChangeEvent += new CallButtonSwitchChangeHandler(CallButtonEventHandler);
            m_ThirdFloor.DownCallButtonSensor.CallButtonSwitchChangeEvent += new CallButtonSwitchChangeHandler(CallButtonEventHandler);

            this.m_UpDestinations = new List<Floor>();
            this.m_DownDestinations = new List<Floor>();
        }

        /*
        public void startLift(Direction dir)
        {
            serviceLiftRequests(dir);
        }
         * */
        ///<summary>
        ///The Method handles the actual motion of the elevator
        ///<param name="Direction" >This represent the prefered direction of the Elevatorcar</param>
        ///<returns>void</returns>
        ///</summary>
        private void serviceLiftRequests(Object direction)
        {
            Direction OnAbortDirection = Direction.Down;
          
            try
            {
                var d = (Direction)direction;
                lock (m_LockHandle)
                {
                    this.LiftDirection = d;
                }
                if (d == Direction.Up)
                {
                    while (m_CurrentPosition != m_TopFloorPosition)
                    {
                        Thread.Sleep((int)(this.LiftSpeed) * 40);
                        if (this.LiftDirection == Direction.DestinationStop)
                        {
                            Thread.Sleep(1000);
                            lock (m_LockHandle)
                            {

                                if ((!(this.m_UpDestinations.Count > 0)) || m_DownDestinations.Count > 0)
                                {
                                    //Make Elevator Go down

                                    OnAbortDirection = Direction.Down;
                                    this.ElevatorServiceThread.Abort();

                                }
                            }
                        }
                        
                        lock (m_LockHandle)
                        {
                            this.LiftDirection = d;
                        }
                        CurrentPosition -= 1;
                    }
                    //Elevator Shld always go down
                    this.ElevatorServiceThread = new Thread(new ParameterizedThreadStart(serviceLiftRequests));
                    this.ElevatorServiceThread.Start(Direction.Down);
                    
                }
                else if (d == Direction.Down)
                {
                    while (m_CurrentPosition != m_BottomFloorPosition)
                    {
                        Thread.Sleep((int)(this.LiftSpeed) * 40);
                        if (this.LiftDirection == Direction.DestinationStop)
                        {
                            Thread.Sleep(1000);
                            lock (m_LockHandle)
                            {
                                if ((!(this.m_DownDestinations.Count > 0)) && m_UpDestinations.Count > 0)
                                {
                                    OnAbortDirection = Direction.Up;
                                    this.ElevatorServiceThread.Abort();
                                }
                            }
                        }
                       
                        lock (m_LockHandle)
                        {
                            this.LiftDirection = d;
                        }
                        CurrentPosition += 1;
                    }
                    if (m_UpDestinations.Count > 0)
                    {
                        this.ElevatorServiceThread = new Thread(new ParameterizedThreadStart(serviceLiftRequests));
                        this.ElevatorServiceThread.Start(Direction.Up);
                    }

                }

            }
            catch (ThreadAbortException _Ew)
            {
                if (true)
                {
                    this.ElevatorServiceThread = new Thread(new ParameterizedThreadStart(serviceLiftRequests));
                    this.ElevatorServiceThread.Start(OnAbortDirection);
                }
            }
            catch (ThreadStartException _Ew)
            {
            }
            catch (Exception _Ew)
            {
            }

        }

        ///<summary>
        ///The Delegate to handle events fired when CallButtonEvent is fired
        ///<param name="EventArgs" >The Arguments required to service this event</param>
        ///<param name="Sender" > The Sending object of this event</param>
        ///<returns>void</returns>
        ///</summary>
        private void CallButtonEventHandler(Object EventArgs, Object Sender)
        {
            var _Sender = Sender as CallButtonSensor;
            var _PreferedLiftDirection = _Sender.Direction;
            
                switch (_Sender.FloorNo)
                {
                    case 0:
                        if (_Sender.Direction == Direction.Up)
                        {
                            if (!m_UpDestinations.Contains(m_GroundFloor))
                            {
                                m_UpDestinations.Add(m_GroundFloor);
                            }
                        }
                        else if (_Sender.Direction == Direction.Down)
                        {

                            //check if elevator is presently at the bottom floor
                            //if (this.CurrentPosition == bottomFloorPosition)
                           // {
                               // _PreferedLiftDirection = Direction.Up;
                                //if (!m_UpDestinations.Contains(m_GroundFloor))
                                //{
                                 //   m_UpDestinations.Add(m_GroundFloor);
                               // }
                            //}
                            //else
                            {
                                if (!m_DownDestinations.Contains(m_GroundFloor))
                                {
                                    m_DownDestinations.Add(m_GroundFloor);
                                }
                            }
                        }
                        break;
                    case 1:
                        if (_Sender.Direction == Direction.Up)
                        {
                            if (!m_UpDestinations.Contains(m_FirstFloor))
                            {
                                m_UpDestinations.Add(m_FirstFloor);
                            }
                        }
                        else if (_Sender.Direction == Direction.Down)
                        {
                            //check if elevator is presently at the bottom floor
                            if (this.CurrentPosition == m_BottomFloorPosition)
                            {
                                _PreferedLiftDirection = Direction.Up;
                                if (!m_UpDestinations.Contains(m_FirstFloor))
                                {
                                    m_UpDestinations.Add(m_FirstFloor);
                                }
                            }
                            else
                            {
                                if (!m_DownDestinations.Contains(m_FirstFloor))
                                {
                                    m_DownDestinations.Add(m_FirstFloor);
                                }
                            }
                        }
                        break;
                    case 2:
                        if (_Sender.Direction == Direction.Up)
                        {
                            if (!m_UpDestinations.Contains(m_SecondFloor))
                            {
                                m_UpDestinations.Add(m_SecondFloor);
                            }
                        }
                        else if (_Sender.Direction == Direction.Down)
                        {
                            //check if elevator is presently at the bottom floor
                            if (this.CurrentPosition == m_BottomFloorPosition)
                            {
                                _PreferedLiftDirection = Direction.Up;
                                if (!m_UpDestinations.Contains(m_SecondFloor))
                                {
                                    m_UpDestinations.Add(m_SecondFloor);
                                }
                            }
                            else
                            {
                                if (!m_DownDestinations.Contains(m_SecondFloor))
                                {
                                    m_DownDestinations.Add(m_SecondFloor);
                                }
                            }
                        }
                        break;
                    case 3:
                        if (_Sender.Direction == Direction.Up)
                        {
                            if (!m_UpDestinations.Contains(m_ThirdFloor))
                            {
                                m_UpDestinations.Add(m_ThirdFloor);
                            }
                        }
                        else if (_Sender.Direction == Direction.Down)
                        {
                            //check if elevator is presently at the bottom floor
                            if (this.CurrentPosition == m_BottomFloorPosition||this.LiftDirection== Direction.Down)
                            {
                                _PreferedLiftDirection = Direction.Up;
                                if (!m_UpDestinations.Contains(m_ThirdFloor))
                                {
                                    m_UpDestinations.Add(m_ThirdFloor);
                                }
                            }
                            else
                            {
                                if (!m_UpDestinations.Contains(m_ThirdFloor))
                                {
                                    m_UpDestinations.Add(m_ThirdFloor);
                                }
                            }
                        }
                        break;
                }
                lock (m_LockHandle)
                {
                    if (!this.ElevatorServiceThread.IsAlive)
                    {
                        this.ElevatorServiceThread = new Thread(new ParameterizedThreadStart(serviceLiftRequests));
                        this.ElevatorServiceThread.Start(_PreferedLiftDirection);
                    }
                }
        }
        ///<summary>
        ///The Delegate to handle events fired when ElevatorCartButtonEvent is fired
        ///<param name="EventArgs" >The Arguments required to service this event</param>
        ///<param name="Sender" > The Sending object of this event</param>
        ///<returns>void</returns>
        ///</summary>
        private void ElevatorCartButtonEventHandler(Object EventArgs, Object Sender)
        {

            var _Sender = Sender as ElevatorCarButton;
            _Sender.IsSensorSwitchOpen = false;
            switch (_Sender.FloorNo)
            {
                case 0:
                    if (this.ElevatorServiceThread.IsAlive||this.ElevatorServiceThread.ThreadState== ThreadState.WaitSleepJoin)
                    {

                    if (this.LiftDirection == Direction.Down)
                    {
                        if (this.m_DownDestinations.Contains(m_GroundFloor))
                        {
                        }
                        else
                        {
                            this.m_DownDestinations.Add(m_GroundFloor);
                        }
                    }
                    else if (this.LiftDirection == Direction.Up)
                    {
                        if (this.m_UpDestinations.Contains(m_GroundFloor))
                        {
                        }
                        else
                        {
                            this.m_UpDestinations.Add(m_GroundFloor);
                        }
                    }

                    }
                    break;
                case 1:
                    if (this.ElevatorServiceThread.IsAlive || this.ElevatorServiceThread.ThreadState == ThreadState.WaitSleepJoin)
                    {

                        if (this.LiftDirection == Direction.Down)
                        {
                            if (this.m_DownDestinations.Contains(m_FirstFloor))
                            {
                            }
                            else
                            {
                                this.m_DownDestinations.Add(m_FirstFloor);
                            }
                        }
                        else if (this.LiftDirection == Direction.Up)
                        {
                            if (this.m_UpDestinations.Contains(m_FirstFloor))
                            {
                            }
                            else
                            {
                                this.m_UpDestinations.Add(m_FirstFloor);
                            }
                        }

                    }
                    else
                    {
                        if (this.m_UpDestinations.Contains(m_FirstFloor))
                        {
                        }
                        else
                        {
                            this.m_UpDestinations.Add(m_FirstFloor);
                        }
                        this.ElevatorServiceThread = new Thread(new ParameterizedThreadStart(serviceLiftRequests));
                        this.ElevatorServiceThread.Start(Direction.Up);
                    }
                    break;
                case 2:
                    if (this.ElevatorServiceThread.IsAlive || this.ElevatorServiceThread.ThreadState == ThreadState.WaitSleepJoin)
                    {

                        if (this.LiftDirection == Direction.Down)
                        {
                            if (this.m_DownDestinations.Contains(m_SecondFloor))
                            {
                            }
                            else
                            {
                                this.m_DownDestinations.Add(m_SecondFloor);
                            }
                        }
                        else if (this.LiftDirection == Direction.Up)
                        {
                            if (this.m_UpDestinations.Contains(m_SecondFloor))
                            {
                            }
                            else
                            {
                                this.m_UpDestinations.Add(m_SecondFloor);
                            }
                        }

                    }
                    else
                    {
                        if (this.m_UpDestinations.Contains(m_SecondFloor))
                        {
                        }
                        else
                        {
                            this.m_UpDestinations.Add(m_SecondFloor);
                        }
                        this.ElevatorServiceThread = new Thread(new ParameterizedThreadStart(serviceLiftRequests));
                        this.ElevatorServiceThread.Start(Direction.Up);
                    }
                    break;
                case 3:
                    if (this.ElevatorServiceThread.IsAlive || this.ElevatorServiceThread.ThreadState == ThreadState.WaitSleepJoin)
                    {

                        if (this.LiftDirection == Direction.Down)
                        {
                            if (this.m_DownDestinations.Contains(m_ThirdFloor))
                            {
                            }
                            else
                            {
                                this.m_DownDestinations.Add(m_ThirdFloor);
                            }
                        }
                        else if (this.LiftDirection == Direction.Up)
                        {
                            if (this.m_UpDestinations.Contains(m_ThirdFloor))
                            {
                            }
                            else
                            {
                                this.m_UpDestinations.Add(m_ThirdFloor);
                            }
                        }

                    }
                    else
                    {
                        if (this.m_UpDestinations.Contains(m_ThirdFloor))
                        {
                        }
                        else
                        {
                            this.m_UpDestinations.Add(m_ThirdFloor);
                        }
                        this.ElevatorServiceThread = new Thread(new ParameterizedThreadStart(serviceLiftRequests));
                        this.ElevatorServiceThread.Start(Direction.Up);
                    }
                    break;
            }
        }
        public void Stop()
        {
            this.m_UpDestinations.Clear();
            this.m_DownDestinations.Clear();
        }

        ///<summary>
        ///The Delegate to handle events fired when FloorSensorEvent is fired
        ///<param name="EventArgs" >The Arguments required to service this event</param>
        ///<param name="Sender" > The Sending object of this event</param>
        ///<returns>void</returns>
        ///</summary>
        private void FloorSensorEventHandler(Object EventArgs, Object Sender)
        {
            var _Sender = Sender as Sensor;

            _Sender.IsSensorSwitchOpen = false;
            switch (_Sender.FloorNo)
            {
                case 0:
                    //Elevator on Ground floor
                    if (this.ElevatorServiceThread.IsAlive||this.ElevatorServiceThread.ThreadState == ThreadState.WaitSleepJoin)
                    {
                        if (this.LiftDirection == Direction.Up)
                        {
                            if (m_UpDestinations.Contains(m_GroundFloor))
                            {
                                lock (m_LockHandle)
                                {
                                    this.LiftDirection = Direction.DestinationStop;
                                    m_UpDestinations.Remove(m_GroundFloor);
                                }
                            }
                        }
                        else if (this.LiftDirection == Direction.Down)
                        {
                            if (m_DownDestinations.Contains(m_GroundFloor))
                            {
                                lock (m_LockHandle)
                                {
                                    this.LiftDirection = Direction.DestinationStop;
                                    m_DownDestinations.Remove(m_GroundFloor);
                                }
                            }
                        }


                    }
                    break;
                case 1:
                    //elevator on first floor
                    if (this.ElevatorServiceThread.IsAlive || this.ElevatorServiceThread.ThreadState == ThreadState.WaitSleepJoin)
                    {
                        if (this.LiftDirection == Direction.Up)
                        {
                            if (m_UpDestinations.Contains(m_FirstFloor))
                            {
                                lock (m_LockHandle)
                                {
                                    this.LiftDirection = Direction.DestinationStop;
                                    this.m_UpDestinations.Remove(m_FirstFloor);
                                }
                                
                            }
                        }
                        else if (this.LiftDirection == Direction.Down)
                        {
                            if (m_DownDestinations.Contains(m_FirstFloor))
                            {
                                lock (m_LockHandle)
                                {
                                    this.LiftDirection = Direction.DestinationStop;
                                    m_DownDestinations.Remove(m_FirstFloor);
                                }
                            }
                        }
                        

                    }
                    break;
                case 2:
                    //elevator on second floor
                    if (this.ElevatorServiceThread.IsAlive || this.ElevatorServiceThread.ThreadState == ThreadState.WaitSleepJoin)
                    {
                        if (this.LiftDirection == Direction.Up)
                        {
                            if (m_UpDestinations.Contains(m_SecondFloor))
                            {
                                lock (m_LockHandle)
                                {
                                    this.LiftDirection = Direction.DestinationStop;
                                    m_UpDestinations.Remove(m_SecondFloor);
                                }
                            }
                        }
                        else if (this.LiftDirection == Direction.Down)
                        {
                            if (m_DownDestinations.Contains(m_SecondFloor))
                            {
                                lock (m_LockHandle)
                                {
                                    this.LiftDirection = Direction.DestinationStop;
                                    m_DownDestinations.Remove(m_SecondFloor);
                                }
                            }
                        }


                    }
                    break;
                case 3:

                    //elevator on third floor
                    if (this.ElevatorServiceThread.IsAlive || this.ElevatorServiceThread.ThreadState == ThreadState.WaitSleepJoin)
                    {
                        if (this.LiftDirection == Direction.Up)
                        {
                            if (m_UpDestinations.Contains(m_ThirdFloor))
                            {
                                lock (m_LockHandle)
                                {
                                    //m_DownDestinations.Add(m_GroundFloor);
                                    this.LiftDirection = Direction.DestinationStop;
                                    m_UpDestinations.Remove(m_ThirdFloor);
                                    
                                }
                            }
                        }
                        else if (this.LiftDirection == Direction.Down)
                        {
                            if (m_DownDestinations.Contains(m_ThirdFloor))
                            {
                                lock (m_LockHandle)
                                {
                                    this.LiftDirection = Direction.DestinationStop;
                                    m_DownDestinations.Remove(m_ThirdFloor);
                                }
                            }
                        }


                    }
                    break;
            }

           
        }
       
    }
}
