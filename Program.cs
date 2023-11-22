using ADS_ED1I4_20231113.controllers;
using ADS_ED1I4_20231113.models;

TransporteController _transporteController = new();

void addVeiculo()
{
    Console.WriteLine("--- Cadastrar veículo ---");
    Console.WriteLine();

    Console.Write("Digite a lotação: ");
    int lotacao = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine();

    bool isSuccess = _transporteController.CadastrarVan(lotacao);

    Console.WriteLine(isSuccess ? "Veículo cadastrado !" : "Erro: Jornada já iniciada.");

    Console.WriteLine("\n--- Fim do cadastro de veículo ---\n");
}

void addGaragem()
{
    Console.WriteLine("--- Cadastrar garagem ---");
    Console.WriteLine();

    Console.Write("Digite o nome: ");
    string nome = Console.ReadLine();
    Console.WriteLine();

    bool isSuccess = _transporteController.CadastrarGaragem(nome);

    Console.WriteLine(isSuccess ? "Garagem cadastrada !" : "Erro: Jornada já iniciada.");

    Console.WriteLine("\n--- Fim do cadastro de garagem ---\n");
}

void iniciarJornada()
{
    Console.WriteLine("--- Início de jornada ---");
    Console.WriteLine();

    bool isSuccess = _transporteController.IniciarJornada();

    Console.WriteLine(isSuccess ? "Jornada iniciada !" : "Erro: Jornada já em andamento.");

    Console.WriteLine("\n--- Fim do início de jornada ---\n");
}

void encerrarJornada()
{
    Console.WriteLine("--- Encerramento de jornada ---");
    Console.WriteLine();

    bool isSuccess = _transporteController.EncerrarJornada();

    if (isSuccess)
    {
        if (_transporteController.Viagens.Count > 0)
        {
            foreach (Van van in _transporteController.Vans)
            {
                int qtde = _transporteController.Viagens.Aggregate(0, (acc, value) => value.Van.Id.Equals(van.Id) ? acc + value.Van.Lotacao : acc);

                Console.WriteLine($"Quantidade de passageiros transportados por Van(Id: {van.Id}, Lotação: {van.Lotacao}) = {qtde}");
            }
        } else
        {
            Console.WriteLine("Não foram realizadas viagem nessa jornada.");
        }
    } 
    else
    {
        Console.WriteLine("Erro: Jornada não iniciada.");
    }

    Console.WriteLine("\n--- Fim do encerramento de jornada ---\n");
}

void liberarViagem()
{
    Console.WriteLine("--- Liberação de viagem ---");
    Console.WriteLine();

    Console.Write("Digite o ID da garagem de origem: ");
    int idOrigem = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine();

    Console.Write("Digite o ID da garagem de destino: ");
    int idDestino = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine();

    Garagem origem = _transporteController.Garagens[idOrigem];
    Garagem destino = _transporteController.Garagens[idDestino];

    bool isSuccess = _transporteController.LiberarViagem(origem, destino);

    Console.WriteLine(isSuccess ? $"Viagem liberada de {origem.Nome} para {destino.Nome}." : "Houve um erro ao liberar a viagem.");
    Console.WriteLine("\n--- Fim da liberação da viagem ---\n");
}

void listarVeiculosPorGaragem()
{
    Console.WriteLine("--- Listagem de veículos por garagem ---");
    Console.WriteLine();

    Console.Write("Digite o ID da garagem: ");
    int id = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine();

    Garagem garagem = _transporteController.Garagens.ElementAt(id);

    Console.WriteLine($"Vans na garagem de {garagem.Nome}: ");

    if (garagem.Vans.Count == 0)
    {
        Console.WriteLine("Não há vans nessa garagem.");
    }
    else
    {
        foreach (var van in garagem.Vans)
        {
            Console.WriteLine($"Van(Id: {van.Id}, Lotação: {van.Lotacao})");
        }

        Console.WriteLine($"Potencial de transporte: {garagem.PotencialDeTransporte}");
    }

    Console.WriteLine("\n--- Fim da listagem de veículos ---\n");
}

void informarQuantidadeDeViagens()
{
    Console.WriteLine("--- Informação sobre quantidade de viagens ---");
    Console.WriteLine();

    Console.Write("Digite o ID da garagem de origem: ");
    int idOrigem = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine();

    Console.Write("Digite o ID da garagem de destino: ");
    int idDestino = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine();

    Garagem origem = _transporteController.Garagens[idOrigem];
    Garagem destino = _transporteController.Garagens[idDestino];

    List<Viagem> viagens = _transporteController.GetViagensByOrigemDestino(origem, destino);

    Console.WriteLine($"Foram efetuadas {viagens.Count} viagens de {origem.Nome} para {destino.Nome}.");

    Console.WriteLine("\n--- Fim da informação sobre quantidade de viagens ---\n");
}

void listarViagensEfetuadas()
{
    Console.WriteLine("--- Informação sobre viagens ---");
    Console.WriteLine();

    Console.Write("Digite o ID da garagem de origem: ");
    int idOrigem = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine();

    Console.Write("Digite o ID da garagem de destino: ");
    int idDestino = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine();

    Garagem origem = _transporteController.Garagens[idOrigem];
    Garagem destino = _transporteController.Garagens[idDestino];

    List<Viagem> viagens = _transporteController.GetViagensByOrigemDestino(origem, destino);

    if (viagens.Count == 0)
    {
        Console.WriteLine($"Não foram efetuadas viagens de {origem.Nome} para {destino.Nome}.");
    }
    else
    {
        foreach (Viagem viagem in viagens)
        {
            Console.WriteLine($"Viagem(Origem: {viagem.Origem.Nome}, Destino: {viagem.Destino.Nome}, Van: (Id: {viagem.Van.Id}, Lotação: {viagem.Van.Lotacao}))");
        }
    }

    Console.WriteLine("\n--- Fim da informação sobre viagens ---\n");
}

void informarQtdePassageiros()
{
    Console.WriteLine("--- Informação sobre quantidade de passageiros das viagens ---");
    Console.WriteLine();

    Console.Write("Digite o ID da garagem de origem: ");
    int idOrigem = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine();

    Console.Write("Digite o ID da garagem de destino: ");
    int idDestino = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine();

    Garagem origem = _transporteController.Garagens[idOrigem];
    Garagem destino = _transporteController.Garagens[idDestino];

    List<Viagem> viagens = _transporteController.GetViagensByOrigemDestino(origem, destino);

    int qtde = viagens.Aggregate(0, (acc, value) => acc + value.Van.Lotacao);

    Console.WriteLine($"A quantidade de passageiros transportados de {origem.Nome} para {destino.Nome} foi de: {qtde}");

    Console.WriteLine("\n--- Fim da informação sobre quantidade de passageiros das viagens ---\n");
}

while (true)
{
    Console.WriteLine("0. Finalizar");
    Console.WriteLine("1. Cadastrar veículo");
    Console.WriteLine("2. Cadastrar garagem");
    Console.WriteLine("3. Iniciar jornada");
    Console.WriteLine("4. Encerrar jornada");
    Console.WriteLine("5. Liberar viagem");
    Console.WriteLine("6. Listar veículos por garagem");
    Console.WriteLine("7. Informar quantidade de viagens por origem/destino");
    Console.WriteLine("8. Listar viagens por origem/destino");
    Console.WriteLine("9. Informar quantidade de passageiros transportados por origem/destino");

    Console.WriteLine();

    int option = Convert.ToInt32(Console.ReadLine());
    Console.Clear();

    if (option == 0) break;
    if (option == 1) addVeiculo();
    if (option == 2) addGaragem();
    if (option == 3) iniciarJornada();
    if (option == 4) encerrarJornada();
    if (option == 5) liberarViagem();
    if (option == 6) listarVeiculosPorGaragem();
    if (option == 7) informarQuantidadeDeViagens();
    if (option == 8) listarViagensEfetuadas();
    if (option == 9) informarQtdePassageiros();
}