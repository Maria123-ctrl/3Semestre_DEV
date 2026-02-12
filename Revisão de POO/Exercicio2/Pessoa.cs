using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercicio2
{
    public class Pessoa
    {
        public string Nome = "";

        public int Idade;

          public void ExibirDados()
        {
            System.Console.WriteLine($"Nome: {Nome}");
            System.Console.WriteLine($"Idade: {Idade}");
        }
    }
}