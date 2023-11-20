using System;
using System.Linq;
using System.Text;
using System.Threading;

namespace De02
{
    internal class Program
    {
        private static string _s;
        static readonly object LockObject = new object();
        private static bool _isStopTimer1 = false, _isStopTimer2 = false;
        private static Timer _timer1, _timer2;
        private static readonly CountdownEvent CountdownEvent = new CountdownEvent(2);
        static void Main(string[] args)
        {
            Console.InputEncoding = Console.OutputEncoding = Encoding.Unicode;

            _timer1 = new Timer(Timer1, null, 0, 5);
            _timer2 = new Timer(Timer2, null, 0, 10);

            CountdownEvent.Wait();

            Console.WriteLine("Bấm phím bất kì để kết thúc chương trình");
            Console.ReadLine();
        }

        static void Timer1(object state)
        {

            lock (LockObject)
            {
                if (_isStopTimer1)
                {
                    return;
                }

                Console.ForegroundColor = ConsoleColor.Cyan;

                Console.Write("Timer1: s = ");
                _s = Console.ReadLine();

                Console.ResetColor();

                if (_s != null && _s.Equals("thoat"))
                {
                    _isStopTimer1 = true;
                    CountdownEvent.Signal();
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
                    //Console.WriteLine("Timer2 is stopped");
                    return;
                }

                if (String.IsNullOrEmpty(_s)) return;

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Timer2: s = {_s}");
                Console.WriteLine($"Timer2: reverse s = {String.Concat(_s.Reverse())}");
                Console.ResetColor();

                if (_s.ToLower().Equals("thoat"))
                {
                    _isStopTimer2 = true;
                    CountdownEvent.Signal();
                    _timer2.Dispose();

                }

            }
        }
    }
}
