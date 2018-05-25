using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenHardwareMonitor.Collections;
using OpenHardwareMonitor.Hardware;

namespace OHMWrapper
{
    public class OHMInstance
    {
        Computer computer;

        public OHMInstance()
        {
            computer = new Computer();
        }

        public void enableModule(OpenHardwareMonitor.Hardware.HardwareType hwtype)
        {
            switch (hwtype)
            {
                case HardwareType.Mainboard:
                    computer.MainboardEnabled = true;
                    break;
                case HardwareType.CPU:
                    computer.CPUEnabled = true;
                    break;
                case HardwareType.GpuNvidia:
                case HardwareType.GpuAti:
                    computer.GPUEnabled = true;
                    break;
                case HardwareType.HDD:
                    computer.HDDEnabled = true;
                    break;
                case HardwareType.RAM:
                    computer.RAMEnabled = true;
                    break;
            }
        }

        public void startMontoring()
        {
            computer.Open();
        }

        public byte countSensors(HardwareType hwtype, SensorType sensortype)
        {
            foreach (var hardware in computer.Hardware)
            {
                if (hardware.HardwareType == hwtype)
                {
                    hardware.Update();
                    byte counter = 0;
                    foreach (var sensor in hardware.Sensors)
                    {
                        if (sensor.SensorType == sensortype)
                        {
                            counter++;
                        }
                    }
                    return counter;
                }
            }
            return 0;
        }

        public byte countHardware(HardwareType hwtype)
        {
            byte counter = 0;
            foreach (var hardware in computer.Hardware)
            {
                if (hardware.HardwareType == hwtype)
                {
                    hardware.Update();
                    counter++;
                }
            }
            return counter;
        }

        public string getHardwareName(HardwareType hwtype, byte objectid = 0)
        {
            byte objectcounter = 0;
            foreach (var hardware in computer.Hardware)
            {
                if (hardware.HardwareType == hwtype)
                {
                    hardware.Update();
                    if (objectid == objectcounter)
                    {
                        return hardware.Name;
                    }
                    objectcounter++;
                }
            }
            return null;
        }

        public string getSensorName(HardwareType hwtype, SensorType sensortype, byte objectid = 0, byte sensorid = 0)
        {
            byte objectcounter = 0;
            foreach (var hardware in computer.Hardware)
            {
                if (hardware.HardwareType == hwtype)
                {
                    hardware.Update();
                    if (objectid == objectcounter)
                    {
                        foreach (var sensor in hardware.Sensors)
                        {
                            if (sensor.SensorType == sensortype)
                            {
                                if (sensorid == sensor.Index)
                                {
                                    return sensor.Name;
                                }
                            }
                        }
                    }
                }
            }
            return null;
        }

        public float getSensorValue(HardwareType hwtype, SensorType sensortype, byte objectid = 0, byte sensorid = 0)
        {
            byte objectcounter = 0;
            foreach (var hardware in computer.Hardware)
            {
                if (hardware.HardwareType == hwtype)
                {
                    hardware.Update();
                    if (objectid == objectcounter)
                    {
                        foreach (var sensor in hardware.Sensors)
                        {
                            if (sensor.SensorType == sensortype)
                            {
                                if (sensorid == sensor.Index)
                                {
                                    return sensor.Value.GetValueOrDefault();
                                }
                            }
                        }
                    }
                }
            }
            return 0;
        }

        public bool doesSensorExist(HardwareType hwtype, SensorType sensortype, byte objectid = 0, byte sensorid = 0)
        {
            byte objectcounter = 0;
            foreach (var hardware in computer.Hardware)
            {
                if (hardware.HardwareType == hwtype)
                {
                    hardware.Update();
                    if (objectid == objectcounter)
                    {
                        foreach (var sensor in hardware.Sensors)
                        {
                            if (sensor.SensorType == sensortype)
                            {
                                if (sensorid == sensor.Index)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }
    }
}
