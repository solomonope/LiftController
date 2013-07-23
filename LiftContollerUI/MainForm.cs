/*  Author:     Folorunsho Solomon Opeyemi(1148183)
 *  Company:    The University Of Birmingham
 *  CodeFileName:MainForm.cs
 *  ClassName:   MainForm         
 *  Description: 
 *  
 * **/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UoB.LiftControllerLib;
using UoB.LiftControllerLib.Events;
using UoB.EventGenerator;
using UoB.EventGenerator.Enumerations;

namespace LiftContollerUI
{
    delegate void SetEventCallBack(Button Button,int c);
    public partial class MainForm : Form
    {
        private LiftController m_LiftController;
        private int GroundFloorHeight, FirstFloorHeight, SecondFloorHeight, ThirdFloorHeight;
        private EventGenerator m_EventGenerator;
        public MainForm()
        {
            InitializeComponent();
            GroundFloorHeight = 455;
            FirstFloorHeight =356 ;
            SecondFloorHeight = 197;
            ThirdFloorHeight = 57;
            m_LiftController = new LiftController(GroundFloorHeight, ThirdFloorHeight, GroundFloorHeight);
            m_LiftController.LiftSpeed = LiftSpeed.One;
            m_LiftController.LiftDirectionChangeEvent += new LiftDirectionChangeHandler(m_LiftController_LiftDirectionChangeEvent);
            m_LiftController.LiftPositionChangeEvent += new LiftPositionChangeHandler(m_LiftController_LiftPositionChangeEvent);
            m_LiftController.GroundFloor.FloorSensor.SensorSwitchChangeEvent += new SensorSwitchChangeHandler(FloorSensor_SensorSwitchChangeEvent);
            m_LiftController.FirstFloor.FloorSensor.SensorSwitchChangeEvent += new SensorSwitchChangeHandler(FloorSensor_SensorSwitchChangeEvent);
            m_LiftController.SecondFloor.FloorSensor.SensorSwitchChangeEvent += new SensorSwitchChangeHandler(FloorSensor_SensorSwitchChangeEvent);
            m_LiftController.ThirdFloor.FloorSensor.SensorSwitchChangeEvent += new SensorSwitchChangeHandler(FloorSensor_SensorSwitchChangeEvent);
            m_EventGenerator = new EventGenerator(m_LiftController);
            m_EventGenerator.EventSpeed = EventSpeed.One;
            m_EventGenerator.EventCountChange += new UoB.EventGenerator.Delegates.EventCountChangeHandler(m_EventGenerator_EventCountChange);
            m_EventGenerator.LifControllerEvent += new UoB.EventGenerator.Delegates.LiftControllerEventHandler(m_EventGenerator_LifControllerEvent);
            this.label11.BackColor = Color.Navy;
            this.label10.BackColor = Color.Navy;

            this.button1.BackColor = Color.White;
            this.button2.BackColor = Color.White;
            this.button3.BackColor = Color.White;
            this.button4.BackColor = Color.White;
            this.button7.BackColor = Color.White;
            this.button8.BackColor = Color.White;
            this.button9.BackColor = Color.White;
            this.button10.BackColor = Color.White;
            this.button11.BackColor = Color.White;
            this.button12.BackColor = Color.White;
            this.button13.BackColor = Color.White;
            this.button16.BackColor = Color.White;
            this.button17.BackColor = Color.White;
            this.button18.BackColor = Color.White;
            this.button19.BackColor = Color.White;
            //this.button25.BackColor = Color.White;

            
        }

        void m_EventGenerator_LifControllerEvent(Event Event)
        {
            //throw new NotImplementedException();

            toolStripStatusLabel1.Text = "EVENT";
            switch (Event)
            {
                case Event.ThirdFloorDownCallButton:
                    toolStripStatusLabel1.Text = "ThirdFloorDownCallButton";
                    if (button1.InvokeRequired)
                    {
                        var dd = new SetEventCallBack(UpdateEventDisplay);
                        this.button1.Invoke(dd, button1, 1);
                    }
                    break;
                case Event.SecondFloorDownCallButton:
                    toolStripStatusLabel1.Text = "SecondFloorDownCallButton";
                    if (button2.InvokeRequired)
                    {
                        var dd = new SetEventCallBack(UpdateEventDisplay);
                        this.button2.Invoke(dd, button2, 1);
                    }
                    break;
                case Event.SecondFloorUpCallButton:
                    toolStripStatusLabel1.Text = "SecondFloorUpCallButton";
                    if (button3.InvokeRequired)
                    {
                        var dd = new SetEventCallBack(UpdateEventDisplay);
                        this.button3.Invoke(dd, button3, 1);
                    }
                    break;
                case Event.FirstFloorDownCallButton:
                    toolStripStatusLabel1.Text = "FirstFloorDownCallButton";

                    if (button11.InvokeRequired)
                    {
                        var dd = new SetEventCallBack(UpdateEventDisplay);
                        this.button11.Invoke(dd, button11, 1);
                    }
                    break;
                case Event.FirstFloorUpCallButton:
                    toolStripStatusLabel1.Text = "FirstFloorUpCallButton";
                    if (button4.InvokeRequired)
                    {
                        var dd = new SetEventCallBack(UpdateEventDisplay);
                        this.button4.Invoke(dd, button4, 1);
                    }
                    break;
                case Event.GroundFloorUpCallButton:
                    toolStripStatusLabel1.Text = "GroundFloorUpCallButton";
                    if (button12.InvokeRequired)
                    {
                        var dd = new SetEventCallBack(UpdateEventDisplay);
                        this.button12.Invoke(dd, button12, 1);
                    }
                    break;
                case Event.ElevatorCarButton0:
                    toolStripStatusLabel1.Text = "ElevatorCarButton0";
                    if (button19.InvokeRequired)
                    {
                        var dd = new SetEventCallBack(UpdateEventDisplay);
                        this.button19.Invoke(dd, button19, 0);
                    }
                    break;
                case Event.ElevatorCarButton1:
                    toolStripStatusLabel1.Text = "ElevatorCarButton1";
                    if (button16.InvokeRequired)
                    {
                        var dd = new SetEventCallBack(UpdateEventDisplay);
                        this.button16.Invoke(dd, button16, 0);
                    }
                    break;
                case Event.ElevatorCarButton2:
                    toolStripStatusLabel1.Text = "ElevatorCarButton2";
                    if (button18.InvokeRequired)
                    {
                        var dd = new SetEventCallBack(UpdateEventDisplay);
                        this.button18.Invoke(dd, button18, 0);
                    }
                    break;
                case Event.ElevatorCarButton3:
                    toolStripStatusLabel1.Text = "ElevatorCarButton3";
                    if (button17.InvokeRequired)
                    {
                        var dd = new SetEventCallBack(UpdateEventDisplay);
                        this.button17.Invoke(dd, button17, 0);
                    }
                    
                    break;
                case Event.NotSet:
                    break;
            }
        }
        void UpdateEventDisplay(Button Sender, int control)
        {
            if (control == 0)
            {
                Sender.ForeColor = Color.Green;
            }
            else
            {
                Sender.BackColor = Color.Green;
            }
        }
        void m_EventGenerator_EventCountChange(int Count)
        {
            //throw new NotImplementedException();
            if (this.label3.InvokeRequired)
            {
                var dd = new SetTopCallBack(UpdateEventCount);
                this.label3.Invoke(dd, Count);
            }
            else
            {
                label3.Text = String.Format("Event Count= {0}", Count);
            }
        }
        void UpdateEventCount(int UpdateEventCount)
        {
            label3.Text = String.Format("Event Count= {0}", UpdateEventCount);
        }
        void FloorSensor_SensorSwitchChangeEvent(object EventArgs, object Sender)
        {
             var _Sender = Sender as Sensor;

            //_Sender.IsSensorSwitchOpen = false;

             this.label9.Text = _Sender.FloorNo.ToString();
        }

        void m_LiftController_LiftPositionChangeEvent(LiftPositionChangeEventArgs EventArgs, object Sender)
        {
            if (this.LiftPanel.InvokeRequired)
            {
                var dd = new SetTopCallBack(SetLiftPosition);
                this.LiftPanel.Invoke(dd, EventArgs.Top);
            }
            else
            {
                if (EventArgs.Top == GroundFloorHeight)
                {
                    button19.ForeColor = Color.Black;
                    if (m_LiftController.LiftDirection == Direction.Up)
                    {
                    }
                    else if (m_LiftController.LiftDirection == Direction.Down)
                    {
                    }
                    m_LiftController.GroundFloor.FloorSensor.IsSensorSwitchOpen = true;
                    this.label11.BackColor = Color.Navy;
                    this.label10.BackColor = Color.Navy;
                }
                else if (EventArgs.Top == FirstFloorHeight)
                {
                    button16.ForeColor = Color.Black;
                    if (m_LiftController.LiftDirection == Direction.Up)
                    {
                    }
                    else if (m_LiftController.LiftDirection == Direction.Down)
                    {
                    }
                    m_LiftController.FirstFloor.FloorSensor.IsSensorSwitchOpen = true;
                }
                else if (EventArgs.Top == SecondFloorHeight)
                {
                    button18.ForeColor = Color.Black;
                    if (m_LiftController.LiftDirection == Direction.Up)
                    {
                    }
                    else if (m_LiftController.LiftDirection == Direction.Down)
                    {
                    }
                    m_LiftController.SecondFloor.FloorSensor.IsSensorSwitchOpen = true;
                }
                else if (EventArgs.Top == ThirdFloorHeight)
                {
                    button17.ForeColor = Color.Black;
                    if (m_LiftController.LiftDirection == Direction.Up)
                    {
                    }
                    else if (m_LiftController.LiftDirection == Direction.Down)
                    {
                    }
                    m_LiftController.ThirdFloor.FloorSensor.IsSensorSwitchOpen = true;
                }
                this.LiftPanel.Top = EventArgs.Top; ;
            }
        }
        private void SetLiftPosition(int Position)
        {
            if (Position == GroundFloorHeight)
            {
                button19.ForeColor = Color.Black;
                if (m_LiftController.LiftDirection == Direction.Up)
                {
                    button12.BackColor = Color.White;
                }
                else if (m_LiftController.LiftDirection == Direction.Down)
                {
                    button12.BackColor = Color.White;
                }
                m_LiftController.GroundFloor.FloorSensor.IsSensorSwitchOpen = true;
                this.label11.BackColor = Color.Navy;
                this.label10.BackColor = Color.Navy;
               
            }
            else if (Position == FirstFloorHeight)
            {
                button16.ForeColor = Color.Black;
                if (m_LiftController.LiftDirection == Direction.Up)
                {
                    button4.BackColor = Color.White;
                    if (button11.BackColor == Color.Green)
                    {
                        button11.BackColor = Color.White;
                    }
                }
                else if (m_LiftController.LiftDirection == Direction.Down)
                {
                    button11.BackColor = Color.White;
                }
                m_LiftController.FirstFloor.FloorSensor.IsSensorSwitchOpen = true;
                
            }
            else if (Position == SecondFloorHeight)
            {
                button18.ForeColor = Color.Black;
                if (m_LiftController.LiftDirection == Direction.Up)
                {
                    button3.BackColor = Color.White;
                    button2.BackColor = Color.White;
                }
                else if (m_LiftController.LiftDirection == Direction.Down)
                {
                    button2.BackColor = Color.White;
                }
                m_LiftController.SecondFloor.FloorSensor.IsSensorSwitchOpen = true;
            }
            else if (Position == ThirdFloorHeight)
            {
                button17.ForeColor = Color.Black;
                if (m_LiftController.LiftDirection == Direction.Up)
                {
                    button1.BackColor = Color.White;
                }
                else if (m_LiftController.LiftDirection == Direction.Down)
                {
                    button1.BackColor = Color.White;
                }
                m_LiftController.ThirdFloor.FloorSensor.IsSensorSwitchOpen = true;
            }
            this.LiftPanel.Top = Position;
        }
        void m_LiftController_LiftDirectionChangeEvent(LiftDirectionChangeEventArgs EventArgs, object Sender)
        {
            //throw new NotImplementedException();
            if (m_LiftController.LiftDirection == Direction.Up)
            {
                this.label11.BackColor = Color.Green;
                this.label10.BackColor = Color.Red;
            }
            else if (m_LiftController.LiftDirection == Direction.Down)
            {
                this.label11.BackColor = Color.Red;
                this.label10.BackColor = Color.Green;
            }
            else if (m_LiftController.LiftDirection == Direction.Stationary)
            {
                this.label11.BackColor = Color.Transparent;
                this.label10.BackColor = Color.Transparent;
            }
            else
            {
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var _AboutBox = new AboutBox();
            _AboutBox.ShowDialog();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (m_EventGenerator.IsSimulatorRunning)
            {
                m_EventGenerator.StopEventGenerator();
            }
            else
            {
                m_EventGenerator.StartEventGenerator();
                label6.Text = String.Format(" Mean Event Time : {0}", (int)m_EventGenerator.EventSpeed * 5000);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //third floor downbutton
            //check if elevator is stationary
            this.m_LiftController.ThirdFloor.DownCallButtonSensor.IsSensorSwitchOpen = true;
            (sender as Button).BackColor = Color.Green;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //second floor downbutton
            this.m_LiftController.SecondFloor.DownCallButtonSensor.IsSensorSwitchOpen = true;
            (sender as Button).BackColor = Color.Green;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //second floor up button
            this.m_LiftController.SecondFloor.UpCallButtonSensor.IsSensorSwitchOpen = true;
            (sender as Button).BackColor = Color.Green;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            //first floor downbutton
            this.m_LiftController.FirstFloor.DownCallButtonSensor.IsSensorSwitchOpen = true;
            (sender as Button).BackColor = Color.Green;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //first floor up button
            this.m_LiftController.FirstFloor.UpCallButtonSensor.IsSensorSwitchOpen = true;
            (sender as Button).BackColor = Color.Green;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            //grounfloor up button
            this.m_LiftController.GroundFloor.DownCallButtonSensor.IsSensorSwitchOpen = true;
            (sender as Button).BackColor = Color.Green;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //increase elevator speed button
            if ((int)m_LiftController.LiftSpeed > 1)
            {
                this.m_LiftController.LiftSpeed = (LiftSpeed)Enum.Parse(typeof(LiftSpeed), ((int)this.m_LiftController.LiftSpeed - 1).ToString());
            }
            else
            {
                return;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //reduce elevator speed button
            if ((int)m_LiftController.LiftSpeed < 5)
            {
                this.m_LiftController.LiftSpeed = (LiftSpeed)Enum.Parse(typeof(LiftSpeed), ((int)this.m_LiftController.LiftSpeed + 1).ToString());
                
            }
            else
            {
                return;
            };
        }

        private void button10_Click(object sender, EventArgs e)
        {
            //mean event time increase
            
            if ((int)m_EventGenerator.EventSpeed > 1)
            {
                this.m_EventGenerator.EventSpeed = (EventSpeed)Enum.Parse(typeof(EventSpeed), ((int)this.m_EventGenerator.EventSpeed - 1).ToString());
                label6.Text = String.Format(" Mean Event Time : {0}", (int)m_EventGenerator.EventSpeed*5000);
            }
            else
            {
                return;
            };
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //mean event time decrease
            if ((int)m_EventGenerator.EventSpeed < 5)
            {
                this.m_EventGenerator.EventSpeed = (EventSpeed)Enum.Parse(typeof(EventSpeed), ((int)this.m_EventGenerator.EventSpeed +1).ToString());
                label6.Text = String.Format(" Mean Event Time : {0}", (int)m_EventGenerator.EventSpeed * 5000);
            }
            else
            {
                return;
            };
        }

        private void button19_Click(object sender, EventArgs e)
        {
            //elevator cart 0 button
            this.m_LiftController.ElevatorCar.ElevatorCarButton0.IsSensorSwitchOpen = true;
            (sender as Button).ForeColor = Color.Green;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            //elevator cart 1 button
            this.m_LiftController.ElevatorCar.ElevatorCarButton1.IsSensorSwitchOpen = true;
            (sender as Button).ForeColor = Color.Green;
        }

        private void button18_Click(object sender, EventArgs e)
        {
            //elevator cart 2 button
            this.m_LiftController.ElevatorCar.ElevatorCarButton2.IsSensorSwitchOpen = true;
            (sender as Button).ForeColor = Color.Green;
        }

        private void button17_Click(object sender, EventArgs e)
        {
            //elevator cart 3 button
            this.m_LiftController.ElevatorCar.ElevatorCarButton3.IsSensorSwitchOpen = true;
            (sender as Button).ForeColor = Color.Green;
        }
    }
}
