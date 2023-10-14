using Application;
using CpfCnpjLibrary;
using Domain.Model;

internal class Program
{
    private static void Main(string[] args)
    {
        bool flag = false;
        Cliente cliente = new Cliente();

        while (!flag)
        {
            Console.Clear();
            Console.WriteLine("Seja bem vindo ao banco Framework");
            Console.WriteLine("Por favor, identifique-se");
            Console.WriteLine("");

            cliente = Identificacao();
            flag = ValidaCliente(cliente);
        }

        Menu(cliente);

    }

    static Cliente Identificacao()
    {
        var cliente = new Cliente();

        
        cliente.Id = ObterId();

        Console.WriteLine("Seu nome:");
        cliente.Nome = Console.ReadLine();

        Console.WriteLine("Seu CPF:");
        cliente.Cpf = Console.ReadLine();

        Console.WriteLine("Seu saldo: ");
        cliente.Saldo = float.Parse(Console.ReadLine());
        Console.Clear();

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

        Console.WriteLine($"Como posso ajudar {cliente.Nome}?");
        ItensMenu();

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
        Console.WriteLine("1 - Deposito \n2 - Saque \n3 - Sair \n*-------------");
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