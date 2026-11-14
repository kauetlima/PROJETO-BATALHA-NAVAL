using System.ComponentModel;
using System.Text;
Console.OutputEncoding = Encoding.UTF8; // Para aceitar o caracter □

string[,] tabuleiro = new string[10, 10];
int[] valorMenu = new int[4];



int linhaPeca = 0;
int colunaPeca = 0;
int tamanhoPeca = 0;
int menu = 0;
int contador = 0;
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
string MenuEscolha()
{
    tamanhoPeca = 0;
    menu = 0;
    Console.WriteLine("Escolha a peça que deseja posicionar\n " +
        "\n 1 - PORTA AVIÕES" +
        "\n 2 - NAVIO TANQUE" +
        "\n 3 - SUBMARINO" +
        "\n 4 - CONTRA-TORPEDEIRO\n");
    menu = int.Parse(Console.ReadLine());



    for (int i = 0; i < contador; i++)
    {
        if (valorMenu[i] == menu)
        {
            Console.WriteLine("Você já colocou essa peça, pressione enter para continuar");
            Console.ReadKey();
            MenuEscolha();
        }
    }

    valorMenu[contador] = menu;
    contador++;

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
            return MenuEscolha();
    }

}
void PosicionaPecas()
{
    void ImpressaoEscolhePosicao(string tipoPeca)
    {
        Console.Clear();
        Console.WriteLine("====================================");
        Console.WriteLine($" POSICIONAR {tipoPeca}");
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

        ImprimeTabuleiro();
    }

    linhaPeca = 0;
    colunaPeca = 0;
    for (int i = 0; i < 5; i++)
    {
        tipoPeca = MenuEscolha();

        if (tipoPeca == "Porta Aviões")
        {

            Console.Clear();
            ImprimeTabuleiro();
            Console.WriteLine();
            Console.Write("\nVocê Posicionar o PORTA-AVIÕES colocar na VERTICAL ou HORIZONTAL -> ");
            sentidoPeca = Console.ReadLine();
            ImprimeTabuleiro();

            if (sentidoPeca == "horizontal")
            {
                ImpressaoEscolhePosicao(tipoPeca);

                string[] armazenaPosicao = Console.ReadLine().Split(' ');

                linhaPeca = int.Parse(armazenaPosicao[0]);
                colunaPeca = int.Parse(armazenaPosicao[1]);

                colunaPeca = colunaPeca - 1;
                linhaPeca = linhaPeca - 1;

                ValidacaoPortaAviaoHorizontal(tamanhoPortaAviao, tipoPeca);
                ImprimeTabuleiro();
            }
            else
            {
                ImpressaoEscolhePosicao(tipoPeca);

                string[] armazenaPosicao = Console.ReadLine().Split(' ');

                linhaPeca = int.Parse(armazenaPosicao[0]);
                colunaPeca = int.Parse(armazenaPosicao[1]);

                colunaPeca = colunaPeca - 1;
                linhaPeca = linhaPeca - 1;

                ImprimeTabuleiro();
            }
        }
        else if (tipoPeca == "Navio-Tanque")
        {

            Console.Write("Escolha onde irá colocar o Navio-Tanque (EX: 9,10): ");
            string[] armazenaPosicao = Console.ReadLine().Split(' ');
            linhaPeca = int.Parse(armazenaPosicao[0]);
            colunaPeca = int.Parse(armazenaPosicao[1]);

        }
        else if (tipoPeca == "Submarino")
        {

            Console.Write("Escolha onde irá colocar o Submarino (EX: 9,10:) ");
            string[] armazenaPosicao = Console.ReadLine().Split(' ');
            linhaPeca = int.Parse(armazenaPosicao[0]);
            colunaPeca = int.Parse(armazenaPosicao[1]);

        }
        else if (tipoPeca == "Contratorpedeiro")
        {
            Console.Write("Escolha onde irá colocar o Porta Contratorpedeiro (EX: 9,10:) ");
            string[] armazenaPosicao = Console.ReadLine().Split(' ');
            linhaPeca = int.Parse(armazenaPosicao[0]);
            colunaPeca = int.Parse(armazenaPosicao[1]);
        }
    }
}
void ImprimeTabuleiro()
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
void ValidacaoPecaHorizontal(int tamanhoPeca, string tipoPeca)
{
    do
    {
        menu = 0;
        Console.WriteLine($"Você deseja colocar seu barco na linha {colunaPeca + 1} e qual sentido para ESQUERDA ou DIREITA ?");
        Console.Write("DIGITE 1 PARA ESQUERDA OU 2 PARA DIREITA :");
        menu = int.Parse(Console.ReadLine());
    } while (menu != 1 && menu != 2);

    if (ValidaEspacoVazioHorizontal(tabuleiro, linhaPeca, colunaPeca, tamanhoPeca, tipoPeca))
    {
        if (menu == 2)
        {
            int limiteDireita = colunaPeca + tamanhoPeca;
            for (int i = colunaPeca; i < limiteDireita; i++)
            {
                tabuleiro[linhaPeca, i] = "x";
            }

        }
        else
        {
            int limiteEsquerda = colunaPeca - tamanhoPeca;
            for (int i = colunaPeca; i > limiteEsquerda; i--)
            {
                tabuleiro[linhaPeca, i] = "x";
            }
        }
    }
    else
        Console.WriteLine("Posição Inválida");
}
void ValidacaoPecaVertical(int tamanhoPeca, string tipoPeca)
{
    do
    {
        menu = 0;
        Console.WriteLine($"Você deseja colocar seu barco na coluna {colunaPeca + 1} e qual sentido para CIMA ou BAIXO ?");
        Console.Write("DIGITE 1 PARA CIMA OU 2 PARA BAIXO :");
        menu = int.Parse(Console.ReadLine());
    } while (menu != 1 && menu != 2);

    if (ValidaEspacoVazioVertical(tabuleiro, linhaPeca, colunaPeca, tamanhoPeca, tipoPeca))
    {
        if (menu == 2)
        {
            int limiteCima = linhaPeca - tamanhoPeca;
            for (int i = linhaPeca; i > limiteCima; i--)
            {
                tabuleiro[i, colunaPeca] = "x";
            }

        }
        else
        {
            int limiteBaixo = linhaPeca + tamanhoPeca;
            for (int i = linhaPeca; i < limiteBaixo; i++)
            {
                tabuleiro[i, colunaPeca] = "x";
            }
        }
    }
    else
        Console.WriteLine("Posição Inválida");
}
bool ValidaEspacoVazioHorizontal(string[,] tabuleiro, int linhaPeca, int colunaPeca, int tamanhoPeca, string tipoPeca)
{
    int direcao;

    if (menu == 1)
    {
        direcao = -1;
    }
    else
    {
        direcao = 1;
    }

    for (int i = 0; i < tamanhoPeca; i++)
    {
        int colunaAtual = colunaPeca + (i * direcao);

        if (colunaAtual < 0 || colunaAtual >= tabuleiro.GetLength(1))
        {
            return false;
        }

        if (tabuleiro[linhaPeca, colunaAtual] == "x")
        {
            return false;
        }
    }
    return true;
}
bool ValidaEspacoVazioVertical(string[,] tabuleiro, int linhaPeca, int colunaPeca, int tamanhoPeca, string tipoPeca)
{
    int direcao;

    if (menu == 1)
    {
        direcao = -1;
    }
    else
    {
        direcao = 1;
    }

    for (int i = 0; i < tamanhoPeca; i++)
    {
        int linhaAtual = linhaPeca + (i * direcao);

        if (linhaAtual < 0 || linhaAtual >= tabuleiro.GetLength(0))
        {
            return false;
        }

        if (tabuleiro[linhaAtual, colunaPeca] == "x")
        {
            return false;
        }
    }

    return true;
}



PopulaTabuleiro(tabuleiro);
PosicionaPecas();



