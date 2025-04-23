using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exercicioContaBancaria
{
    public class Cliente
    {
        public string Cpf { get; set; } //CPF
        public string Nome { get; set; } //NOME

        public Cliente(string cpf, string nome)
        {
            this.Cpf = cpf;
            this.Nome = nome;
        }
    }
}
