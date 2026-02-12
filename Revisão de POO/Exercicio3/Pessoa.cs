using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Exercicio3
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

         public Pessoa(string n, int i)
     {
        if(i > 0)
        {
            Nome = n;
            Idade = i;
        }
        else
        {
            Console.WriteLine($"A idade não pode ser definida menor 0");
            Nome = n;
            Idade = 0;
        }
     }
    }
}