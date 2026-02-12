using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exercicio8;

namespace Exercicio8
{
    public class Usuario : IAutenticavel
    {
        public string Nome = "";
        public string Senha = "";
        public bool Autenticar(string senha)
        {
            return Senha == senha;
        }
    }
}