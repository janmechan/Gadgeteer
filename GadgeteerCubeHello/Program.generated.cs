﻿
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by the Gadgeteer Designer.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Gadgeteer;
using GTM = Gadgeteer.Modules;

namespace GadgeteerCubeHello
{
    public partial class Program : Gadgeteer.Program
    {
        // GTM.Module defintions
		Gadgeteer.Modules.GHIElectronics.Button button;
		Gadgeteer.Modules.GHIElectronics.Extender extender7;
		Gadgeteer.Modules.GHIElectronics.Extender extender4;
		Gadgeteer.Modules.GHIElectronics.UsbClientDP usbClient;

		public static void Main()
        {
			//Important to initialize the Mainboard first
            Mainboard = new GHIElectronics.Gadgeteer.FEZHydra();			

            Program program = new Program();
			program.InitializeModules();
            program.ProgramStarted();
            program.Run(); // Starts Dispatcher
        }

        private void InitializeModules()
        {   
			// Initialize GTM.Modules and event handlers here.		
			usbClient = new GTM.GHIElectronics.UsbClientDP(2);
		
			extender7 = new GTM.GHIElectronics.Extender(7);
		
			extender4 = new GTM.GHIElectronics.Extender(8);
		
			button = new GTM.GHIElectronics.Button(12);

        }
    }
}
