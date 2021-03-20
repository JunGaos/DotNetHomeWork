using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlarmClock
{
    public delegate void TickHanlder(object sender, DateTime args);
    public delegate void AlarmHandler(object sender, DateTime args);
    public class Clock
    {
        public event TickHanlder Tick;
        public event AlarmHandler Alarm;

        private DateTime time;
        private DateTime alarmtime;
        public DateTime Time { get => time; set => time = value; }
        public DateTime AlarmTime { get => alarmtime; set => alarmtime = value; }

        public Clock()
        {
            time = DateTime.Now;
        }
        public void Begin()
        {
            while (true)
            {
                DateTime nowTime = DateTime.Now;
                Tick(this, nowTime);
                if (nowTime.ToString() == alarmtime.ToString())
                {
                    Alarm(this, alarmtime);
                }
                System.Threading.Thread.Sleep(1000);
            }
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            Clock c = new Clock();
            c.Tick += ShowTick;
            c.Alarm += ShowAlarm;
            c.AlarmTime = c.Time.AddSeconds(10);
            c.Begin();
        }
        static void ShowTick(object sender, DateTime time)
        {
            Console.WriteLine("当前时间:" + time);
        }
        static void ShowAlarm(object sender, DateTime time)
        {
            Console.WriteLine("闹钟时间:" + time);
        }
    }
}
