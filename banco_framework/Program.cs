using Domain.Model;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.Clear();
        Console.WriteLine("Seja bem vindo ao banco Framework");
        Console.WriteLine("Por favor, identifique-se");
        Console.WriteLine("");

        var pessoa = Identificacao();

        Menu();

    }

    static Pessoa Identificacao()
    {
        var pessoa = new Pessoa();

        Console.WriteLine("Seu número de identificação:");
        pessoa.Id = int.Parse(Console.ReadLine());

        Console.WriteLine("Seu nome:");
        pessoa.Nome = Console.ReadLine();

        Console.WriteLine("Seu CPF:");
        pessoa.Cpf = Console.ReadLine();
        Console.Clear();

        Console.WriteLine($"Como posso ajudar {pessoa.Nome}?");

        return pessoa;
    }

    static void Menu()
    {
        ItensMenu();
        int opcao;
        
        while (!int.TryParse(Console.ReadLine(), out opcao)) 
        {
            Console.Clear();
            ItensMenu();
        }

        switch (opcao)
        {
            case 1:
                Console.WriteLine("Deposito");
                Console.ReadKey();
                break;
            case 2:
                Console.WriteLine("Saque");
                Console.ReadKey();
                break;
            case 3:
                Environment.Exit(0);
                break;
            default:
                Console.Clear();
                Menu();
                break;

        }
    }

    static void ItensMenu()
    {
        Console.WriteLine("1 - Deposito \n2 - Saque \n3 - Sair ");
    }

}