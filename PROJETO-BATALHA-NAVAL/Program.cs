using System.ComponentModel;
using System.Text;
Console.OutputEncoding = Encoding.UTF8; // Para aceitar o caracter □

string[,] tabuleiro = new string[10, 10];

int linhaPeca = 0;
int colunaPeca = 0;
int tamanhoPeca = 0;
int menu = 0;
string sentidoPeca = string.Empty;
string tipoPeca = string.Empty;

void PopulaTabuleiro(string[,] tabuleiro)
{
    for (int linha = 0; linha < 10; linha++)
    {
        for (int coluna = 0; coluna < 10; coluna++)
        {
            tabuleiro[linha, coluna] = "□";
            Console.Write($"[{tabuleiro[linha, coluna]}]");
        }
        Console.WriteLine();
    }
}
string menuEscolha()
{
    menu = 0;


    Console.WriteLine("Escolha a peça que deseja posicionar\n " +
        "\n 1 - PORTA AVIÕES" +
        "\n 2 - NAVIO TANQUE" +
        "\n 3 - SUBMARINO" +
        "\n 4 - CONTRATORPEDEIRO\n");
    menu = int.Parse(Console.ReadLine());

    switch (menu)
    {
        case 1:
            tamanhoPeca = 5;
            return tipoPeca = "Porta Aviões";

        case 2:
            tamanhoPeca = 4;
            return tipoPeca = "Navio-Tanque";

        case 3:
            tamanhoPeca = 3;
            return tipoPeca = "Submarino";

        case 4:
            tamanhoPeca = 2;
            return tipoPeca = "Contratorpedeiro";

        default:
            Console.WriteLine("Opção inválida! Tente novamente.");
            return menuEscolha();
    }

}
void ImpressaoEscolhePosicao(string pecaQueVaiImprimir)
{
    Console.Clear();
    Console.WriteLine("====================================");
    Console.WriteLine($" POSICIONAR {pecaQueVaiImprimir}");
    Console.WriteLine("====================================");
    Console.WriteLine();
    Console.WriteLine("Informe a posição inicial do navio.");
    Console.WriteLine();
    Console.WriteLine("       LINHA   COLUNA");
    Console.WriteLine("         ↓       ↓");
    Console.WriteLine("Exemplo: 9       10");
    Console.WriteLine("Não esqueça do espaço entre LINHA e COLUNA");
    Console.WriteLine();
    Console.Write("Digite as coordenadas (Linha Coluna): ");
    Console.WriteLine();

    ImprimeTabueleiro();
}
void PosicionaPecas()
{
    linhaPeca = 0;
    colunaPeca = 0;

    if (menuEscolha() == "Porta Aviões")
    {

        Console.Clear();
        ImprimeTabueleiro();
        Console.WriteLine();
        Console.Write("\nVocê Posicionar o PORTA-AVIÕES colocar na VERTICAL ou HORIZONTAL -> ");
        sentidoPeca = Console.ReadLine();
        ImprimeTabueleiro();

        if (sentidoPeca == "horizontal")
        {
            ImpressaoEscolhePosicao(tipoPeca);

            string[] armazenaPosicao = Console.ReadLine().Split(' ');

            linhaPeca = int.Parse(armazenaPosicao[0]);
            colunaPeca = int.Parse(armazenaPosicao[1]);

            colunaPeca = colunaPeca - 1;
            linhaPeca = linhaPeca - 1;

            ValidacaoPortaAviaoHorizontal();
            ImprimeTabueleiro();
        }
        else
        {
            ImpressaoEscolhePosicao(tipoPeca);

            string[] armazenaPosicao = Console.ReadLine().Split(' ');

            linhaPeca = int.Parse(armazenaPosicao[0]);
            colunaPeca = int.Parse(armazenaPosicao[1]);

            colunaPeca = colunaPeca - 1;
            linhaPeca = linhaPeca - 1;

            ValidacaoPortaAviaoVertical();
            ImprimeTabueleiro();
        }
    }
    else if (menuEscolha() == "Navio-Tanque")
    {

        Console.Write("Escolha onde irá colocar o Navio-Tanque (EX: 9,10): ");
        string[] armazenaPosicao = Console.ReadLine().Split(' ');
        linhaPeca = int.Parse(armazenaPosicao[0]);
        colunaPeca = int.Parse(armazenaPosicao[1]);

    }
    else if (menuEscolha() == "Submarino")
    {

        Console.Write("Escolha onde irá colocar o Submarino (EX: 9,10:) ");
        string[] armazenaPosicao = Console.ReadLine().Split(' ');
        linhaPeca = int.Parse(armazenaPosicao[0]);
        colunaPeca = int.Parse(armazenaPosicao[1]);

    }
    else if (menuEscolha() == "Contratorpedeiro")
    {
        Console.Write("Escolha onde irá colocar o Porta Contratorpedeiro (EX: 9,10:) ");
        string[] armazenaPosicao = Console.ReadLine().Split(' ');
        linhaPeca = int.Parse(armazenaPosicao[0]);
        colunaPeca = int.Parse(armazenaPosicao[1]);
    }
}
void ImprimeTabueleiro()
{
    for (int linha = 0; linha < 10; linha++)
    {
        for (int coluna = 0; coluna < 10; coluna++)
        {
            Console.Write($"[{tabuleiro[linha, coluna]}]");
        }
        Console.WriteLine();
    }
}
void ValidacaoPortaAviaoHorizontal()
{
    ValidaEspacoVazioHorizontal();
    int limiteDireita = colunaPeca - tamanhoPeca;
    int limiteEsquerda = colunaPeca + tamanhoPeca;

    if (colunaPeca > 5)
    {
        for (; colunaPeca > limiteDireita; colunaPeca--)
        {
            tabuleiro[linhaPeca, colunaPeca] = "x";

        }
    }
    else if (colunaPeca < 4)
    {
        for (; colunaPeca < limiteEsquerda; colunaPeca++)
        {
            tabuleiro[linhaPeca, colunaPeca] = "x";
        }
    }
    else if (colunaPeca == 5)
    {
        do
        {
            menu = 0;
            Console.WriteLine($"Você deseja colocar seu barco na linha {linhaPeca + 1}?");
            Console.WriteLine("1 - Horizontal: 2,3,4,5,6");
            Console.WriteLine("2 - Horizontal: 6,7,8,9,10");
            Console.Write("DIGITE 1 OU 2 :");
            menu = int.Parse(Console.ReadLine());
        } while (menu != 1 && menu != 2);

        Console.Clear();

        switch (menu)
        {
            case 1:
                for (; colunaPeca > limiteDireita; colunaPeca--)
                {
                    tabuleiro[linhaPeca, colunaPeca] = "x";
                }
                break;

            case 2:
                for (; colunaPeca < limiteEsquerda; colunaPeca++)
                {
                    tabuleiro[linhaPeca, colunaPeca] = "x";
                }
                break;

        }
    }
    else if (colunaPeca == 4)
    {
        do
        {
            menu = 0;
            Console.WriteLine($"Você deseja colocar seu barco na linha {linhaPeca + 1}?");
            Console.WriteLine("1 - Horizontal: 1,2,3,4,5");
            Console.WriteLine("2 - Horizontal: 5,6,7,8,9");
            Console.Write("DIGITE 1 OU 2 :");
            menu = int.Parse(Console.ReadLine());
        } while (menu != 1 && menu != 2);

        Console.Clear();

        switch (menu)
        {
            case 1:
                for (; colunaPeca > limiteDireita; colunaPeca--)
                {
                    tabuleiro[linhaPeca, colunaPeca] = "x";
                }
                break;

            case 2:
                for (; colunaPeca < limiteEsquerda; colunaPeca++)
                {
                    tabuleiro[linhaPeca, colunaPeca] = "x";
                }
                break;

        }
    }
}
void ValidacaoPortaAviaoVertical()
{
    ValidaEspacoVazioVertical();
    int limiteCima = linhaPeca + tamanhoPeca;
    int limiteBaixo = linhaPeca - tamanhoPeca;

    if (linhaPeca > 5)
    {
        for (; linhaPeca > limiteBaixo; linhaPeca--)
        {
            tabuleiro[linhaPeca, colunaPeca] = "x";
        }
    }
    else if (linhaPeca < 4)
    {
        for (; linhaPeca < limiteCima; linhaPeca++)
        {
            tabuleiro[linhaPeca, colunaPeca] = "x";
        }
    }
    else if (linhaPeca == 5)
    {
        do
        {
            menu = 0;
            Console.WriteLine($"Você deseja colocar seu barco na linha {colunaPeca + 1}?");
            Console.WriteLine("1 - Vertical: 2,3,4,5,6");
            Console.WriteLine("2 - Vertical: 6,7,8,9,10");
            Console.Write("DIGITE 1 OU 2 :");
            menu = int.Parse(Console.ReadLine());

        } while (menu != 1 && menu != 2);

        Console.Clear();

        switch (menu)
        {
            case 1:
                for (; linhaPeca > limiteBaixo; linhaPeca--)
                {
                    tabuleiro[linhaPeca, colunaPeca] = "x";
                }
                break;

            case 2:
                for (; linhaPeca < limiteCima; linhaPeca++)
                {
                    tabuleiro[linhaPeca, colunaPeca] = "x";
                }
                break;

        }
    }
    else if (linhaPeca == 4)
    {
        do
        {
            menu = 0;
            Console.WriteLine($"Você deseja colocar seu barco na linha {colunaPeca + 1}?");
            Console.WriteLine("1 - Vertical: 1,2,3,4,5");
            Console.WriteLine("2 - Vertical: 5,6,7,8,9");
            Console.Write("DIGITE 1 OU 2 :");
            menu = int.Parse(Console.ReadLine());

        } while (menu != 1 && menu != 2);

        Console.Clear();

        switch (menu)
        {
            case 1:
                for (; linhaPeca > limiteBaixo; linhaPeca--)
                {
                    tabuleiro[linhaPeca, colunaPeca] = "x";
                }
                break;

            case 2:
                for (; linhaPeca < limiteCima; linhaPeca++)
                {
                    tabuleiro[linhaPeca, colunaPeca] = "x";
                }
                break;

        }
    }
}
void ValidaEspacoVazioHorizontal()
{
    int limiteDireita = colunaPeca - tamanhoPeca;
    int limiteEsquerda = colunaPeca + tamanhoPeca;

    if (colunaPeca > 5)
    {
        for (; colunaPeca > limiteDireita; colunaPeca--)
        {
            if (tabuleiro[linhaPeca,colunaPeca] == "x")
            {
                break;
            }
        }
    }
    else if (colunaPeca < 4)
    {
        for (; colunaPeca < limiteEsquerda; colunaPeca++)
        {
             if (tabuleiro[linhaPeca,colunaPeca] == "x")
            {
                break;
            }
        }
    }
    else if (colunaPeca == 5)
    {
        do
        {
            menu = 0;
            Console.WriteLine($"Você deseja colocar seu barco na linha {linhaPeca + 1}?");
            Console.WriteLine("1 - Horizontal: 2,3,4,5,6");
            Console.WriteLine("2 - Horizontal: 6,7,8,9,10");
            Console.Write("DIGITE 1 OU 2 :");
            menu = int.Parse(Console.ReadLine());
        } while (menu != 1 && menu != 2);

        Console.Clear();

        switch (menu)
        {
            case 1:
                for (; colunaPeca > limiteDireita; colunaPeca--)
                {
                    if (tabuleiro[linhaPeca, colunaPeca] == "x")
                    {
                        break;
                    }
                }
                break;

            case 2:
                for (; colunaPeca < limiteEsquerda; colunaPeca++)
                {
                    if (tabuleiro[linhaPeca, colunaPeca] == "x")
                    {
                        break;
                    }
                }
                break;
        }
    }
    else if (colunaPeca == 4)
    {
        do
        {
            menu = 0;
            Console.WriteLine($"Você deseja colocar seu barco na linha {linhaPeca + 1}?");
            Console.WriteLine("1 - Horizontal: 1,2,3,4,5");
            Console.WriteLine("2 - Horizontal: 5,6,7,8,9");
            Console.Write("DIGITE 1 OU 2 :");
            menu = int.Parse(Console.ReadLine());
        } while (menu != 1 && menu != 2);

        Console.Clear();

        switch (menu)
        {
            case 1:
                for (; colunaPeca > limiteDireita; colunaPeca--)
                {
                    if (tabuleiro[linhaPeca, colunaPeca] == "x")
                    {
                        break;
                    }
                }
                break;

            case 2:
                for (; colunaPeca < limiteEsquerda; colunaPeca++)
                {
                    if (tabuleiro[linhaPeca, colunaPeca] == "x")
                    {
                        break;
                    }
                }
                break;

        }
    }
}
void ValidaEspacoVazioVertical()
{
    int limiteCima = linhaPeca + tamanhoPeca;
    int limiteBaixo = linhaPeca - tamanhoPeca;

    if (linhaPeca > 5)
    {
        for (; linhaPeca > limiteBaixo; linhaPeca--)
        {
            if (tabuleiro[linhaPeca, colunaPeca] == "x")
            {
                break;
            }
        }
    }
    else if (linhaPeca < 4)
    {
        for (; linhaPeca < limiteCima; linhaPeca++)
        {
            if (tabuleiro[linhaPeca, colunaPeca] == "x")
            {
                break;
            }
        }
    }
    else if (linhaPeca == 5)
    {
        do
        {
            menu = 0;
            Console.WriteLine($"Você deseja colocar seu barco na linha {colunaPeca + 1}?");
            Console.WriteLine("1 - Vertical: 2,3,4,5,6");
            Console.WriteLine("2 - Vertical: 6,7,8,9,10");
            Console.Write("DIGITE 1 OU 2 :");
            menu = int.Parse(Console.ReadLine());

        } while (menu != 1 && menu != 2);

        Console.Clear();

        switch (menu)
        {
            case 1:
                for (; linhaPeca > limiteBaixo; linhaPeca--)
                {
                    if (tabuleiro[linhaPeca, colunaPeca] == "x")
                    {
                        break;
                    }
                }
                break;

            case 2:
                for (; linhaPeca < limiteCima; linhaPeca++)
                {
                    if (tabuleiro[linhaPeca, colunaPeca] == "x")
                    {
                        break;
                    }
                }
                break;

        }
    }
    else if (linhaPeca == 4)
    {
        do
        {
            menu = 0;
            Console.WriteLine($"Você deseja colocar seu barco na linha {colunaPeca + 1}?");
            Console.WriteLine("1 - Vertical: 1,2,3,4,5");
            Console.WriteLine("2 - Vertical: 5,6,7,8,9");
            Console.Write("DIGITE 1 OU 2 :");
            menu = int.Parse(Console.ReadLine());

        } while (menu != 1 && menu != 2);

        Console.Clear();

        switch (menu)
        {
            case 1:
                for (; linhaPeca > limiteBaixo; linhaPeca--)
                {
                    if (tabuleiro[linhaPeca, colunaPeca] == "x")
                    {
                        break;
                    }
                }
                break;

            case 2:
                for (; linhaPeca < limiteCima; linhaPeca++)
                {
                    if (tabuleiro[linhaPeca, colunaPeca] == "x")
                    {
                        break;
                    }
                }
                break;

        }
    }
}


PopulaTabuleiro(tabuleiro);
PosicionaPecas();



