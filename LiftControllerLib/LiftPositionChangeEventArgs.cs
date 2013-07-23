/*  Author:     Folorunsho Solomon Opeyemi(1148183)
 *  Company:    The University Of Birmingham
 *  CodeFileName:LiftPositionChangeEventArgs.cs
 *  ClassName:   LiftPositionChangeEventArgs         
 *  Description: 
 *  
 * **/
using System;
namespace UoB.LiftControllerLib.Events
{
    ///<summary>
    ///An Object to hold data when LiftPositionChange Event is fired
    ///</summary>
    public class LiftPositionChangeEventArgs
    {
        #region privatefield
        //present top position
        private int m_Top;
        #endregion

        #region Property
        ///<summary>
        ///A public property for the presentpostion
        ///</summary>
        public int Top
        {
            get
            {
                return m_Top;
            }
            set
            {
                m_Top = value;
            }
        }
        #endregion
    }
}
