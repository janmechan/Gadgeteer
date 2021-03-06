﻿using System;
using Microsoft.SPOT;

using GT = Gadgeteer;
using GTM = Gadgeteer.Modules;
using GTI = Gadgeteer.Interfaces;

namespace Gadgeteer.Modules.ManufacturerName
{
    /// <summary>
    /// A PiezoModule module for Microsoft .NET Gadgeteer
    /// </summary>
    public class PiezoModule : GTM.Module
    {
        // This example implements  a driver in managed code for a simple Gadgeteer module.  The module uses a 
        // single GTI.InterruptInput to interact with a sensor that can be in either of two states: low or high.
        // The example code shows the recommended code pattern for exposing the property (IsHigh). 
        // The example also uses the recommended code pattern for exposing two events: PiezoModuleHigh, PiezoModuleLow. 
        // The triple-slash "///" comments shown will be used in the build process to create an XML file named
        // GTM.ManufacturerName.PiezoModule. This file will provide Intellisense and documention for the
        // interface and make it easier for developers to use the PiezoModule module.        

        // Note: A constructor summary is auto-generated by the doc builder.
        /// <summary></summary>
        /// <param name="socketNumber">The socket that this module is plugged in to.</param>
        public PiezoModule(int socketNumber)
        {
            // This finds the Socket instance from the user-specified socket number.  
            // This will generate user-friendly error messages if the socket is invalid.
            // If there is more than one socket on this module, then instead of "null" for the last parameter, 
            // put text that identifies the socket to the user (e.g. "S" if there is a socket type S)
            Socket socket = Socket.GetSocket(socketNumber, true, this, null);

            // This creates an GTI.InterruptInput interface. The interfaces under the GTI namespace provide easy ways to build common modules.
            // This also generates user-friendly error messages automatically, e.g. if the user chooses a socket incompatible with an interrupt input.
            this.input = new GTI.InterruptInput(socket, GT.Socket.Pin.Three, GTI.GlitchFilterMode.On, GTI.ResistorMode.PullUp, GTI.InterruptMode.RisingAndFallingEdge, this);

            // This registers a handler for the interrupt event of the interrupt input (which is below)
            this.input.Interrupt += new GTI.InterruptInput.InterruptEventHandler(this._input_Interrupt);
        }

        private void _input_Interrupt(GTI.InterruptInput input, bool value)
        {
            this.OnPiezoModuleEvent(this, value ? PiezoModuleState.Low : PiezoModuleState.High);
        }

        private GTI.InterruptInput input;

        /// <summary>
        /// Gets a value that indicates whether the state of this PiezoModule is high.
        /// </summary>
        public bool IsHigh
        {
            get
            {
                return this.input.Read();
            }
        }

        /// <summary>
        /// Represents the state of the <see cref="PiezoModule"/> object.
        /// </summary>
        public enum PiezoModuleState
        {
            /// <summary>
            /// The state of PiezoModule is low.
            /// </summary>
            Low = 0,
            /// <summary>
            /// The state of PiezoModule is high.
            /// </summary>
            High = 1
        }

        /// <summary>
        /// Represents the delegate that is used to handle the <see cref="PiezoModuleHigh"/>
        /// and <see cref="PiezoModuleLow"/> events.
        /// </summary>
        /// <param name="sender">The <see cref="PiezoModule"/> object that raised the event.</param>
        /// <param name="state">The state of the PiezoModule</param>
        public delegate void PiezoModuleEventHandler(PiezoModule sender, PiezoModuleState state);

        /// <summary>
        /// Raised when the state of <see cref="PiezoModule"/> is high.
        /// </summary>
        /// <remarks>
        /// Implement this event handler and the <see cref="PiezoModuleLow"/> event handler
        /// when you want to provide an action associated with PiezoModule activity.
        /// The state of the PiezoModule is passed to the <see cref="PiezoModuleEventHandler"/> delegate,
        /// so you can use the same event handler for both PiezoModule states.
        /// </remarks>
        public event PiezoModuleEventHandler PiezoModuleHigh;

        /// <summary>
        /// Raised when the state of <see cref="PiezoModule"/> is low.
        /// </summary>
        /// <remarks>
        /// Implement this event handler and the <see cref="PiezoModuleHigh"/> event handler
        /// when you want to provide an action associated with PiezoModule activity.
        /// Since the state of the PiezoModule is passed to the <see cref="PiezoModuleEventHandler"/> delegate,
        /// you can use the same event handler for both PiezoModule states.
        /// </remarks>
        public event PiezoModuleEventHandler PiezoModuleLow;

        private PiezoModuleEventHandler onPiezoModule;

        /// <summary>
        /// Raises the <see cref="PiezoModuleHigh"/> or <see cref="PiezoModuleLow"/> event.
        /// </summary>
        /// <param name="sender">The <see cref="PiezoModule"/> that raised the event.</param>
        /// <param name="PiezoModuleState">The state of the PiezoModule.</param>
        protected virtual void OnPiezoModuleEvent(PiezoModule sender, PiezoModuleState PiezoModuleState)
        {
            if (this.onPiezoModule == null)
            {
                this.onPiezoModule = new PiezoModuleEventHandler(this.OnPiezoModuleEvent);
            }

            if (Program.CheckAndInvoke((PiezoModuleState == PiezoModuleState.High ? this.PiezoModuleHigh : this.PiezoModuleLow), this.onPiezoModule, sender, PiezoModuleState))
            {
                switch (PiezoModuleState)
                {
                    case PiezoModuleState.High:
                        this.PiezoModuleHigh(sender, PiezoModuleState);
                        break;
                    case PiezoModuleState.Low:
                        this.PiezoModuleLow(sender, PiezoModuleState);
                        break;
                }
            }
        }
    }
}
