using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using Microsoft.Devices.Sensors;

namespace Samurai
{
    public static class SAAccelerometer
    {
        static Accelerometer accelerometer;
        public static float X { get; set; }     //X���� 
        public static float Y { get; set; }     //Y����
        public static float Z { get; set; }     //Z����

        /// <summary>
        /// �������ټ�
        /// </summary>
        public static void StartAccelerometer()
        {
            accelerometer = new Accelerometer();
            accelerometer.CurrentValueChanged += accelerometer_CurrentValueChanged;
            accelerometer.Start();
        }

        /// <summary>
        /// ����������Stop����
        /// </summary>
        public static void RestartAccelerometer()
        {
            if (accelerometer != null)
            {
                accelerometer.Start();
            }
        }

        /// <summary>
        /// ��ͣ����������
        /// </summary>
        public static void StopAccelerometer()
        {
            accelerometer.Stop();
            X = Y = Z = 0;
        }

        /// <summary>
        /// �ͷ�
        /// </summary>
        public static void ReleaseAccelerometer()
        {
            if (accelerometer != null)
            {
                accelerometer.Stop();
                accelerometer = null;
            }
            X = Y = Z = 0;
        }

        static void accelerometer_CurrentValueChanged(object sender, SensorReadingEventArgs<AccelerometerReading> e)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() => NewReading(e));
        }

        private static void NewReading(SensorReadingEventArgs<AccelerometerReading> e)
        {
            X = e.SensorReading.Acceleration.X;
            Y = e.SensorReading.Acceleration.Y;
            Z = e.SensorReading.Acceleration.Z;
        }
    }
}
