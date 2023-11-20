using System;
using System.Text;
using System.Threading;

namespace De03
{
    internal class Program
    {
        private static string _c;
        static readonly object LockObject = new object();
        private static bool _isStopTimer1, _isStopTimer2;
        private static Timer _timer1, _timer2;
        private static readonly CountdownEvent CountdownEvent = new CountdownEvent(2);
        static void Main(string[] args)
        {

            Console.InputEncoding = Console.OutputEncoding = Encoding.Unicode;
            _timer1 = new Timer(Timer1, null, 0, 5);
            _timer2 = new Timer(Timer2, null, 10, 10);

            CountdownEvent.Wait();

            Console.WriteLine("Bấm phím bất kì để kết thúc chương trình");
            Console.ReadLine();
        }
        static void Timer1(object state)
        {
            lock (LockObject)
            {
                if (_isStopTimer1) return;

                Console.ForegroundColor = ConsoleColor.Cyan;

                Console.Write("Timer1: c = ");
                _c = Console.ReadLine() ?? "";

                Console.ResetColor();


                if (_c.ToLower().Equals("thoat") || String.IsNullOrEmpty(_c))
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
                if (_isStopTimer2) return;
                if (_c.ToLower().Equals("thoat") || String.IsNullOrEmpty(_c))
                {
                    _isStopTimer2 = true;
                    CountdownEvent.Signal();
                    _timer2.Dispose();
                    return;
                }

                Console.ForegroundColor = ConsoleColor.Green;

                Console.WriteLine($"Timer2: c = {_c}");
                Console.WriteLine($"Timer2: Số từ: {_c.Split(' ').Length}");

                Console.ResetColor();


            }
        }
    }
}
