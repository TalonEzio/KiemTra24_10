using System;
using System.Text;
using System.Threading;

namespace De03
{
    internal class Program
    {
        private static string _c;
        static readonly object LockObject = new object();
        private static bool _isStopTimer1 = false, _isStopTimer2 = false;
        private static Timer _timer1, _timer2;
        static void Main(string[] args)
        {
            Console.InputEncoding = Console.OutputEncoding = Encoding.Unicode;
            _timer1 = new Timer(Timer1, null, 0, 5);
            _timer2 = new Timer(Timer2, null, 10, 10);
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

                Console.Write("Timer1: c = ");
                _c = Console.ReadLine();
                if (_c != null && _c.Equals("thoat"))
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
                Console.WriteLine($"Timer2: c = {_c}");
                if (_c != null) Console.WriteLine($"Timer2: Số từ: {_c.Split(' ').Length}");
                if (_c != null && _c.ToLower().Equals("thoat"))
                {
                    _isStopTimer2 = true;

                    _timer2.Dispose();

                }

            }
        }
    }
}
