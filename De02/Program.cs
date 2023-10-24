using System;
using System.Linq;
using System.Threading;

namespace De02
{
    internal class Program
    {
        private static string _s;
        static readonly object LockObject = new object();
        private static bool _isStopTimer1 = false, _isStopTimer2 = false;
        private static Timer _timer1, _timer2;
        static void Main(string[] args)
        {
            _timer1 = new Timer(Timer1, null, 0, 5);
            _timer2 = new Timer(Timer2, null, 0, 10);

            Thread.Sleep(10000000);
        }

        static void Timer1(object state)
        {

            lock (LockObject)
            {
                if (_isStopTimer1)
                {
                    Console.WriteLine("Timer1 is stopped");
                    return;
                }

                Console.Write("Timer1: s = ");
                _s = Console.ReadLine();
                if (_s != null && _s.Equals("thoat"))
                {
                    _isStopTimer1 = true;
                    _timer1.Dispose();

                }
            }
        }
        static void Timer2(object state)
        {


            lock (LockObject)
            {
                if (_isStopTimer2)
                {
                    Console.WriteLine("Timer2 is stopped");
                    return;
                }
                Console.WriteLine($"Timer2: s = {_s}");
                if (_s != null) Console.WriteLine($"Timer2: reverse s = {String.Concat(_s.Reverse())}");
                if (_s != null && _s.ToLower().Equals("thoat"))
                {
                    _isStopTimer2 = true;

                    _timer2.Dispose();

                }

            }
        }
    }
}
