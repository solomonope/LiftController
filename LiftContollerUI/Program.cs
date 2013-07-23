/*  Author:     Folorunsho Solomon Opeyemi(1148183)
 *  Company:    The University Of Birmingham
 *  CodeFileName:Program.cs
 *  ClassName:   Program         
 *  Description: 
 *  
 * **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace LiftContollerUI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainForm _MainForm = new MainForm();
            Application.Run(_MainForm);
        }
    }
}
