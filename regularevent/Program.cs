using System;
using System.Threading;

namespace regularevent
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            { // scope
                var twe = new ThingWithEvent();
                { 
                    var l = new Listener(twe);
                    twe.Fire();
                    Console.WriteLine("leave inner scope automatically, no finalizers expected");
                }

                System.GC.Collect( 2, GCCollectionMode.Forced, true, true );
                GC.WaitForPendingFinalizers(); 
                twe.Fire();
                Console.WriteLine("leave outer scope on enter, finalizers expected");
                Console.ReadLine();    
            }
            System.GC.Collect( 2, GCCollectionMode.Forced, true, true );
            GC.WaitForPendingFinalizers(); 
            Thread.Sleep(3*1000);
            Console.WriteLine("Exiting");            

        }
        
        class Listener {
            public Listener(ThingWithEvent twe)
            {
                twe.MyEvent += e;
            }

            void e(object sender, EventArgs lala) {
                Console.WriteLine("Listener fires still");
            }

            ~Listener(){
                Console.WriteLine("Finalizer Listener");
            }
        }

        class ThingWithEvent {

            public event EventHandler<EventArgs> MyEvent;


            public void Fire() {
                MyEvent?.Invoke(this, new EventArgs());
            }

            ~ThingWithEvent(){
                Console.WriteLine("Finalizer ThingWithEvent");
            }
        }
    }
}
