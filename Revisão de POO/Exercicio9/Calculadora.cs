using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercicio9
{
    public class Calculadora
    {
        public static double Somar(double a, double b)
        {
            Console.WriteLine($"O resultado da soma é: {a + b}");
            return a + b;
        }

        public static double Multiplicar(double a, double b)
        {
            Console.WriteLine($"O resultado da multiplicação é: {a * b}");
            return a * b;
        } 
    }
}