   using System;
 
using System.Collections;
 
using System.Collections.Generic;
 
using static System.Console;
 
using System.Threading.Tasks;
 
using System.Timers;
 

namespace timer
{

 

 
 
 
    public class MyTimer
 
    {
 
 
 
        public System.Timers.Timer aTimer;
 
 
 
        public MyTimer()
 
        {
 
            aTimer = new System.Timers.Timer();
        aTimer.Disposed += (_, __ ) => Console.WriteLine("Real timer disposed");
            aTimer.Interval = 2000;
 
            // Have the timer fire repeated events (true is the default)
 
            aTimer.AutoReset = true;
 
            // Start the timer
 
            aTimer.Enabled = true;
 
        }
 
 
 
        ~MyTimer()
 
        {
 
            Console.WriteLine( "Finalizer for MyTimer is executing." );
 
        }
 
    }
 
 
 
    public class CreateTimer
 
    {
 
        public void CreateTheTimer()
 
        {
 
            MyTimer testTimer = new MyTimer();
 
            testTimer.aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
 
        }
 
 
 
        ~CreateTimer()
 
        {
 
            Console.WriteLine( "Finalizer for CreateTimer is executing." );
 
        }
 
 
 
        private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
 
        {
 
            Console.WriteLine("The Elapsed event was raised at {0} for an object of type {1}.", e.SignalTime, source.GetType().Name );
 
        }
 
    }
 
 
 
    class Program
 
    {
 
 
 
        static void Main(string[] args)
 
        {
 
 
 
           {
 
               CreateTimer aTimer = new CreateTimer();
 
 
 
               aTimer.CreateTheTimer();
 
 
 
           }
 
 
 
           while(true)
 
           {
 
               System.GC.Collect( 2, GCCollectionMode.Forced, true, true );
 
               System.Threading.Thread.Sleep(100);
 
           }
 
 
 
        }
 
 
 
    }
 
}

