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

namespace RgbLedTest
{
    public partial class Program : Gadgeteer.Program
    {
        // GTM.Module defintions
		Gadgeteer.Modules.GHIElectronics.MulticolorLed led1;
		Gadgeteer.Modules.GHIElectronics.MulticolorLed led2;

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
			led1 = new GTM.GHIElectronics.MulticolorLed(14);
		
			led2 = new GTM.GHIElectronics.MulticolorLed(led1.DaisyLinkSocketNumber);

        }
    }
}
