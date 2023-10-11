using Application;
using Domain.Model;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.Clear();
        Console.WriteLine("Seja bem vindo ao banco Framework");
        Console.WriteLine("Por favor, identifique-se");
        Console.WriteLine("");

        var cliente = Identificacao();

        Menu(cliente);

    }

    static Cliente Identificacao()
    {
        var cliente = new Cliente();

        Console.WriteLine("Seu número de identificação:");
        cliente.Id = int.Parse(Console.ReadLine());

        Console.WriteLine("Seu nome:");
        cliente.Nome = Console.ReadLine();

        Console.WriteLine("Seu CPF:");
        cliente.Cpf = Console.ReadLine();

        Console.WriteLine("Seu saldo: ");
        cliente.Saldo = float.Parse(Console.ReadLine());
        Console.Clear();

        Console.WriteLine($"Como posso ajudar {cliente.Nome}?");

        return cliente;
    }

    static void Menu(Cliente cliente)
    {
        ItensMenu();
        int opcao = 0;

        while (true)
        {
            while (!int.TryParse(Console.ReadLine(), out opcao))
            {
                Console.Clear();
                ItensMenu();
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

    static void ItensMenu()
    {
        Console.WriteLine("1 - Deposito \n2 - Saque \n3 - Sair ");
    }

    private static void Depositar(Cliente cliente)
    {
        var calculo = new Calculo();

        Console.WriteLine("Informe quanto deseja depositar: ");
        float valorDeposito = float.Parse(Console.ReadLine());

        cliente.Saldo = calculo.Soma(cliente.Saldo, valorDeposito);
        Console.WriteLine($"O valor atualizado do saldo é: {cliente.Saldo}");
    }

    private static void Sacar(Cliente cliente)
    {
        var calculo = new Calculo();

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
            Console.WriteLine($"O valor atualizado do saldo é: {valorFinal}");
        }
       
    }

}