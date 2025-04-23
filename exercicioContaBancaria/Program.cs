using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace exercicioContaBancaria
{
    internal class Program
    {
        //listas p/ guardar clientes e contas
        static List<Cliente> clientes = new List<Cliente>();
        static List<Conta> contas = new List<Conta>();
        static void Main(string[] args)
        {
            //loop menu
            while (true)
            {
                Console.WriteLine("\n       Console do Banco\n");

                Console.WriteLine("Opção 1 - Cadastrar Cliente.");
                Console.WriteLine("Opção 2 - Criar Conta.");
                Console.WriteLine("Opção 3 - Depositar.");
                Console.WriteLine("Opção 4 - Sacar.");
                Console.WriteLine("Opção 5 - Consultar Saldo.");
                Console.WriteLine("Opção 6 - Transferir.");
                Console.WriteLine("Opção 7 - Exit.");
                Console.WriteLine();
                Console.WriteLine("Escolha uma Opção: ");
                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        CadastrarCliente();
                        break;

                    case "2":
                        CriarConta();
                        break;

                    case "3":
                        Depositar();
                        break;

                    case "4":
                        Sacar();
                        break;

                    case "5":
                        ConsultarSaldo();
                        break;
                    case "6":
                        Transferir();
                        break;

                    case "7":
                        return; // retornar

                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }

            }
        //fim loop menu
        }

        //cadastrar cliente
        static void CadastrarCliente()
        {
            string cpf;
            bool cpfValido;
            do
            {
                Console.WriteLine("Digite o CPF: ");
                cpf = Console.ReadLine();

                cpf = new string(cpf.Where(char.IsDigit).ToArray());

                cpfValido = cpf.Length == 11 && cpf.All(char.IsDigit);

                if (!cpfValido)
                {
                    Console.WriteLine("CPF inválido. \nUse Apenas Número e Necessário Conter 11 dígitos.");
                }
            } while (!cpfValido);

            if (clientes.Any(c => c.Cpf == cpf))
            {
                Console.WriteLine("CPF já Cadastrado.");
                return;
            }

            string nome;
            bool nomeValido;
            do
            {
                Console.WriteLine("Digite o Nome: ");
                nome = Console.ReadLine().Trim(); 

                nomeValido = !string.IsNullOrWhiteSpace(nome) && nome.Length <= 50 &&
                             nome.All(c => char.IsLetter(c) || char.IsWhiteSpace(c));

                if (!nomeValido)
                {
                    Console.WriteLine("Nome Inválido. \nUse Apenas Letras e Espaços, e Não Deixe o Campo Vazio.");
                }
            } while (!nomeValido);

            nome = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(nome.ToLower());

            Cliente cliente = new Cliente(cpf, nome);
            clientes.Add(cliente);
            Console.WriteLine("Cliente Cadastrado com Sucesso.");
        }
        //fim cadastrar
        //criar conta
        static void CriarConta()
        {
            Console.Write("Digite o CPF do Cliente: ");
            string cpf = Console.ReadLine();

            Cliente cliente = clientes.FirstOrDefault(c => c.Cpf == cpf);
            if (cliente == null)
            {
                Console.WriteLine("Cliente Não Encontrado.");
                return;
            }

            string numeroStr;
            int numero;
            bool numeroValido;
            do
            {
                Console.WriteLine("Digite o Número da Conta (apenas números, máximo 5 dígitos): ");
                numeroStr = Console.ReadLine();

                numeroValido = numeroStr.All(char.IsDigit) && numeroStr.Length <= 5;

                if (!numeroValido)
                {
                    Console.WriteLine("Número da Conta inválido. Use Apenas Números (máximo 5 dígitos).");
                }
            } while (!numeroValido); 

            numero = int.Parse(numeroStr);

            if (contas.Any(c => c.Numero == numero))
            {
                Console.WriteLine("Número da Conta já Cadastrado.");
                return;
            }

            Conta conta = new Conta(numero, cliente);
            contas.Add(conta);
            Console.WriteLine("Conta Criada com Sucesso!");
        }
        //fim criar conta
        //depositar
        static void Depositar()
        {

            string numeroStr;
            int numero;
            bool numeroValido;
            do
            {
                Console.WriteLine("Digite o Número da Conta (5 dígitos): ");
                numeroStr = Console.ReadLine();

                numeroValido = int.TryParse(numeroStr, out numero) && numeroStr.Length == 5;

                if (!numeroValido)
                {
                    Console.WriteLine("Número da conta inválido. Use apenas números com 5 dígitos.");
                }
            } while (!numeroValido); 

            Conta conta = contas.FirstOrDefault(c => c.Numero == numero);
            if (conta == null)
            {
                Console.WriteLine("Conta Não Encontrada.");
                return;
            }

            string valorStr;
            decimal valor;
            bool valorValido;
            do
            {
                Console.WriteLine("Digite o Valor para Depositar: ");
                valorStr = Console.ReadLine();

                valorValido = decimal.TryParse(valorStr, out valor) && valor > 0 && valor <= 1000000;

                if (!valorValido)
                {
                    Console.WriteLine("Valor Inválido. Use Apenas Números Positivos.");
                }
            } while (!valorValido); 


            conta.Depositar(valor);
            Console.WriteLine($"Depósito de {valor.ToString("C2")} Realizado com Sucesso.");
        }
        //fim depositar
        //sacar
        static void Sacar()
        {

            string numeroStr;
            int numero;
            bool numeroValido;
            do
            {
                Console.WriteLine("Digite o Número da Conta (5 Dígitos): ");
                numeroStr = Console.ReadLine();

                numeroValido = int.TryParse(numeroStr, out numero) && numeroStr.Length == 5;

                if (!numeroValido)
                {
                    Console.WriteLine("Número da Conta inválido. Use Apenas Números com 5 Dígitos.");
                }
            } while (!numeroValido); 

            Conta conta = contas.FirstOrDefault(c => c.Numero == numero);
            if (conta == null)
            {
                Console.WriteLine("Conta Não Encontrada.");
                return;
            }

            string valorStr;
            decimal valor;
            bool valorValido;
            do
            {
                Console.WriteLine("Digite o Valor para Sacar: ");
                valorStr = Console.ReadLine();

                valorValido = decimal.TryParse(valorStr, out valor) && valor > 0 && valor <= 1000000;

                if (!valorValido)
                {
                    Console.WriteLine("Valor Inválido.");
                }
            } while (!valorValido);

            conta.Sacar(valor);
            Console.WriteLine($"Saque de {valor.ToString("C2")} Realizado com Sucesso.");
        }
        //fim sacar
        //consultar
        static void ConsultarSaldo()
        {
            Console.WriteLine("Digite o Número da Conta: ");
            int numero = int.Parse(Console.ReadLine());

            Conta conta = contas.FirstOrDefault(c => c.Numero == numero);
            if (conta == null)
            {
                Console.WriteLine("Conta Não Encontrada.");
                return;
            }

            conta.ConsultarSaldo();
        }
        //fim consultar
        //transferir
        static void Transferir()
        {
            Console.WriteLine("Digite o Número da Conta de Origem: ");
            int numeroOrigem = int.Parse(Console.ReadLine());

            Conta contaOrigem = contas.FirstOrDefault(c => c.Numero == numeroOrigem);
            if (contaOrigem == null)
            {
                Console.WriteLine("Conta de Origem Não Encontrada.");
                return;
            }
            Console.WriteLine("Digite o Número da Conta de Destino: ");
            int numeroDestino = int.Parse(Console.ReadLine());

            Conta contaDestino = contas.FirstOrDefault(c => c.Numero == numeroDestino);
            if (contaDestino == null)
            {
                Console.WriteLine("Conta de Destino Não Encontrada.");
                return;
            }

            Console.WriteLine("Digite o Valor para Transferir: ");
            decimal valor = decimal.Parse(Console.ReadLine());
            contaOrigem.Transferir(valor, contaDestino);
        }
        //fim transferir

        }
}
