using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercicio6
{
    public class Pessoa
    {
        
        public string Nome = "";
        public int Idade = 0;

        public Pessoa(string nome, int idade)
        {
            Nome = nome;
            if (idade < 0)
            {
                Console.WriteLine("Idade não pode ser negativa. Idade definida como 0.");
                Idade = 0;
            }
            else
            {
                Idade = idade;
            }
        }
        public Pessoa()
        {
        }

        public void ExibirDados()
        {
            Console.WriteLine($"Nome: {Nome}");
            Console.WriteLine($"Idade: {Idade}");
        }
        public void AlterarIdade(int novaIdade)
        {
            if (novaIdade < 0)
            {
                Console.WriteLine("Idade não pode ser negativa. Idade permanece inalterada.");
            }
            else
            {
                Idade = novaIdade;
            }
        }

        public void Apresentar()
        {
            Console.WriteLine($"Olá, meu nome é {Nome} e tenho {Idade} anos.");
        }

        public void Apresentar(string sobrenome)
        {
            Console.WriteLine($"Olá, meu nome é {Nome} {sobrenome} e tenho {Idade} anos.");
        }
    }
    }
