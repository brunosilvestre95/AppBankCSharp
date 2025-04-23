using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exercicioContaBancaria
{
    // conta
    public class Conta
    {
        // att
        public int Numero { get; set; } // n
        public decimal Saldo { get; set; } // saldo
        public Cliente Cliente { get; set; } // cliente

        // criar conta
        public Conta(int numero, Cliente cliente)
        {
            Numero = numero;
            Cliente = cliente;
            Saldo = 0; 
        }

        //depositar
        public void Depositar(decimal valor)
        {
            if (valor > 0) 
            {
                Saldo += valor;
                Console.WriteLine($"Deposito de: {valor:C} Realizado com Sucesso");
            }
            else
            {
                Console.WriteLine("Valor Invalido. \nNecessário Valor Acima de R$0");
            }
        }

        //sacar
        public void Sacar (decimal valor)
        {
            if(valor > 0 && valor <= Saldo) 
            { 
                Saldo -= valor;
                Console.WriteLine($"Saque de: {valor:C} Realizado com Sucesso.");
            }
            else
            {
                Console.WriteLine("Saldo Insuficiente ou Valor Inválido para Saque.");
            }
        }

        //consultar
        public void ConsultarSaldo()
        {
            Console.WriteLine($"Saldo Atual: R${Saldo:C}");
        }

        //transferir
        public void Transferir(decimal valor, Conta contaDestino)
        {
            if (valor > 0 && valor <= Saldo) 
            {
                Saldo -= valor; 
                contaDestino.Saldo += valor;
                Console.WriteLine($"Transferência de: {valor:C} Realizada com Sucesso.");
            }
            else
            {
                Console.WriteLine("Saldo Insuficiente ou Valor Inválido para Transferência.");
            }
        }



        }


    }
