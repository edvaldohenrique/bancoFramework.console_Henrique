﻿using Application;
using CpfCnpjLibrary;
using Domain.Model;
using Repository;

internal class Program
{
    private static void Main(string[] args)
    {
        bool ClienteValido = false;
        Cliente cliente = new Cliente();
        ClienteRepository clienteRepository = new ClienteRepository();

        while (!ClienteValido)
        {
            Console.Clear();
            Console.WriteLine("Seja bem vindo ao banco Framework");
            Console.WriteLine("Por favor, identifique-se");
            Console.WriteLine("");

            cliente = Identificacao();
            ClienteValido = ValidaCliente(cliente);

            if (cliente.NovoCliente && ClienteValido)
                clienteRepository.Insert(cliente);
        }

        Menu(cliente);

    }

    private static Cliente Identificacao()
    { 
        ClienteRepository clienteRepository = new ClienteRepository();
        
        int id = ObterId();

        Cliente clienteConsulta = clienteRepository.GetbyId(id);

        if (clienteConsulta != null)
        {
            Console.Clear();
            clienteConsulta.NovoCliente = false;
            return clienteConsulta;
        }

        return SolicitaDadosUsuario(id);

    }

    private static Cliente SolicitaDadosUsuario(int id)
    {
        var cliente = new Cliente();
        cliente.Id = id;

        Console.WriteLine("Seu nome:");
        cliente.Nome = Console.ReadLine();

        Console.WriteLine("Seu CPF:");
        cliente.Cpf = Console.ReadLine();

        Console.WriteLine("Seu saldo: ");
        cliente.Saldo = float.Parse(Console.ReadLine());
        Console.Clear();

        cliente.NovoCliente = true;

        return cliente;
    }

    private static int ObterId()
    {
        int id; 
        while(true)
        {
            Console.WriteLine("Seu número de identificação:");

            if (int.TryParse(Console.ReadLine(), out id))
            {
                return id;   
            }

            Console.WriteLine("Indentificação inválida. Informe apenas números.");
            Console.ReadKey();
            Console.Clear();
        }
    }

    static void Menu(Cliente cliente)
    {
        int opcao = 0;

        
        ItensMenu(cliente.Nome);

        while (true)
        {

            while (!int.TryParse(Console.ReadLine(), out opcao))
            {
                Console.Clear();
                ItensMenu(cliente.Nome);
            }

            switch (opcao)
            {
                case 1:
                    Depositar(cliente);
                    break;
                case 2:
                    Sacar(cliente);
                    break;
                case 3:
                    Environment.Exit(0);
                    break;
                default:
                    Console.Clear();
                    Menu(cliente);
                    break;

            }
        }
    }

    static void ItensMenu(string nomeCliente)
    {
        Console.WriteLine($"Como posso ajudar {nomeCliente}?");
        Console.WriteLine("1 - Deposito \n2 - Saque \n3 - Sair \n*-------------");
    }

    private static void Depositar(Cliente cliente)
    {
        var calculo = new Calculo();
        ClienteRepository clienteRepository = new ClienteRepository();

        Console.WriteLine("Informe quanto deseja depositar: ");
        float valorDeposito = float.Parse(Console.ReadLine());

        cliente.Saldo = calculo.Soma(cliente.Saldo, valorDeposito);
        clienteRepository.UpdateSaldo(cliente);
        Console.WriteLine($"O valor atualizado do saldo é: {cliente.Saldo}");
    }

    private static void Sacar(Cliente cliente)
    {
        var calculo = new Calculo();
        ClienteRepository clienteRepository = new ClienteRepository();

        Console.WriteLine("Informe quanto deseja sacar: ");
        float valorSaque = float.Parse(Console.ReadLine());

        float valorFinal = calculo.Subtrai(cliente.Saldo, valorSaque);

        if(valorFinal < 0)
        {
            Console.WriteLine("Saldo insuficiente para operação.");
        }
        else
        {
            cliente.Saldo = valorFinal;
            clienteRepository.UpdateSaldo(cliente);
            Console.WriteLine($"O valor atualizado do saldo é: {valorFinal}");
        }
       
    }

    private static bool ValidaCliente(Cliente cliente)
    {
        IList<string> erros = new List<string>();
        
        if (!Cpf.Validar(cliente.Cpf))
            erros.Add("CPF invalido");
        if (cliente.Saldo <= 0)
            erros.Add("Saldo informado deve ser maior que zero");

        if (erros.Count > 0)
        {
            Console.WriteLine("As seguintes inconsistencias foram detectadas: \n");
            foreach (var item in erros)
            {
                Console.WriteLine(" - "+item+"\n");
            }

            Console.ReadKey();

            return false;
        }

        return true;
    }

}