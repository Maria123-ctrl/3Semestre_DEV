using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercicio5
{
    public class Funcionario : Pessoa
    {
        public double salario = 0;

        public void ExibirDados()
        {
            System.Console.WriteLine($"Salario: {salario}");
            System.Console.WriteLine($"Nome: {Nome}");
            System.Console.WriteLine($"Idade: {Idade}");
        }
        
    }
}