using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Expression1
{
    class Program
    {
        static void Main(string[] args)
        {
            Func<int, int> doubleMe = (x) => x * 2;

            Func<double> pi = () => 22.00 / 7.00;

            Console.WriteLine(doubleMe(2));
            Console.WriteLine(pi());

            Expression<Func<int,int>> doubleMeExpression = (x) => x * 2;

            var xv = doubleMeExpression.Compile();
            Console.WriteLine(xv(2));
        }
    }
}
