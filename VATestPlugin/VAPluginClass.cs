﻿using System;



//there are actually two namespaces in this .cs (VATestPlugin, VATestWinampPlugin... see way down below) and each contain a class that would be considered a, 'plugin' (since each contain the necessary static functions to be a VoiceAttack plugin)

namespace EDNeutronRouterPlugin
{
    public class EDNeutronRouterPlugin
    {
        private static RouteManager route = new RouteManager();

        public static string VA_DisplayName()
        {
            return "Neutron Router - v0.1.0+";  //this is what you will want displayed in dropdowns as well as in the log file to indicate the name of your plugin
        }

        public static string VA_DisplayInfo()
        {
            return "Voice Attack Plugin to use ed neutron router easily with a VR headset";  //this is just extended info that you might want to give to the user.  note that you should format this up properly.
        }
        public static void VA_StopCommand()
        {

        }

        public static Guid VA_Id()
        {
            return new Guid("{C16BEBA9-2017-4b7f-BFA2-55B7C667125B}");  //this id must be generated by YOU... it must be unique so VoiceAttack can identify and use the plugin
        }

        public static void VA_Init1(dynamic vaProxy)
        {

        }

        public static void VA_Exit1(dynamic vaProxy)
        {
        }

        public static void VA_Invoke1(dynamic vaProxy)
        {
            try
            {
                switch(vaProxy.Context)
                {
                    case "setRoute":
                        route = new RouteManager();

                        string SystemName = vaProxy.GetText("System name");
                        string TargetSystem = vaProxy.GetText("Next system name");
                        decimal jumpRange = 0.0m;
                        if(vaProxy.GetDecimal("Jump range") != null)
                            jumpRange = vaProxy.GetDecimal("Jump range");

                        route.CalculateRoute(vaProxy, SystemName, TargetSystem, jumpRange, 60);


                        vaProxy.SetText("nextSystem", route.GetNextSystemName());
                        break;
                    case "nextSystem":
                        vaProxy.SetText("nextSystem", route.GetNextSystemName());
                        vaProxy.WriteToLog(vaProxy.GetText("nextSystem"), "blue");

                        break;
                    case "previousSystem":
                        vaProxy.SetText("previousSystem", route.GetPreviousSystemName());
                        vaProxy.WriteToLog(vaProxy.GetText("previousSystem"), "blue");

                        break;
                    case "clearRoute":
                        vaProxy.WriteToLog("route cleared", "blue");
                        route = new RouteManager();
                        break;
                    case "moduleCheck":
                        VATestPlugin.CalculeOptionalModule fff = new VATestPlugin.CalculeOptionalModule();
                        ///fff.CalculateModules(vaProxy);
                        break;

                    default:
                        vaProxy.WriteToLog("incorrect context", "red");
                        break;
                }
            }
            catch(Exception error)
            {
                vaProxy.WriteToLog("Error : " + error, "red");
            }
            
        }
    }
}

