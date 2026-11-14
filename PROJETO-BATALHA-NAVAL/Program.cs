using System.ComponentModel;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Text;
Console.OutputEncoding = Encoding.UTF8; // Para aceitar o caracter □

string[,] tabuleiro1 = new string[10, 10];
string[,] tabuleiro2 = new string[10, 10];
int[] valorMenu = new int[4];
string[,] posicaoMatriz = new string[11, 11];



int linhaPeca = 0;
int colunaPeca = 0;
int tamanhoPeca = 0;
int menu = 0;
int contador = 0;
int sentidopeca = 0;
int jogador = 0;
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
            Console.Clear();
            Console.WriteLine("Você já colocou essa peça");
            return MenuEscolha();

        }
    }

    valorMenu[contador] = menu;

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
            Console.Clear();
            Console.WriteLine("Opção inválida! Tente novamente.");
            return MenuEscolha();

    }
}
void LugarPecaPosicaoPeca(string[,] tabuleiro)
{
    //FUNÇÃO IMPRESSÃO ESCOLHA POSIÇÃO ↓ (PROPRIA FUNÇÃO, NÃO ESTÁ CHAMANDO)
    void ImpressaoEscolhePosicao(string tipoPeca)
    {
        Console.Clear();
        Console.WriteLine("====================================");
        Console.WriteLine($" POSICIONAR {tipoPeca}");
        Console.WriteLine("====================================");
        Console.WriteLine();
        Console.WriteLine("Informe a posição inicial do navio.");
        Console.WriteLine();
        Console.WriteLine("Exemplo:");
        Console.WriteLine("LINHA: 9");
        Console.WriteLine("COLUNA: 10");
        Console.Write("Digite as coordenadas (Linha | Coluna): ");
        Console.WriteLine();
    }

    for (int j = 0; j < 4; j++)
    {

        MenuEscolha();
        contador++;

        Console.Clear();
        ImprimeTabuleiro(tabuleiro);
        Console.WriteLine();
        Console.Write($"\nVocê irá posicionar o Posicionar o {tipoPeca} colocar na \n Digite 1 - HORIZONTAL \n Digite 2 - VERTICAL  \n-> ");
        sentidopeca = int.Parse(Console.ReadLine());
        ImprimeTabuleiro(tabuleiro);

        if (sentidopeca == 1)
        {
            ImpressaoEscolhePosicao(tipoPeca);
            ImprimeTabuleiro(tabuleiro);
            Console.WriteLine("\n");
            Console.Write("LINHA: ");
            int linha = int.Parse(Console.ReadLine());

            Console.Write("COLUNA: ");
            int coluna = int.Parse(Console.ReadLine());
            ImprimeTabuleiro(tabuleiro);

            int[] armazenaPosicao = { linha, coluna };

            linhaPeca = linha;
            colunaPeca = coluna;

            colunaPeca = colunaPeca - 1;
            linhaPeca = linhaPeca - 1;

            ValidacaoPecaHorizontal(tabuleiro, colunaPeca, linhaPeca, tamanhoPeca);
            ImprimeTabuleiro(tabuleiro);
        }
        else
        {
            ImpressaoEscolhePosicao(tipoPeca);
            ImprimeTabuleiro(tabuleiro);


            Console.Write("LINHA: ");
            int linha = int.Parse(Console.ReadLine());

            Console.Write("COLUNA: ");
            int coluna = int.Parse(Console.ReadLine());

            ImprimeTabuleiro(tabuleiro);


            int[] armazenaPosicao = { linha, coluna };

            linhaPeca = linha;
            colunaPeca = coluna;

            colunaPeca = colunaPeca - 1;
            linhaPeca = linhaPeca - 1;

            ValidacaoPecaVertical(tabuleiro, colunaPeca, linhaPeca, tamanhoPeca);
            ImprimeTabuleiro(tabuleiro);
        }
    }


}
void ImprimeTabuleiro(string[,] tabuleiro)
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
void ValidacaoPecaHorizontal(string[,] tabuleiro, int colunaPeca, int linhaPeca, int tamanhoPeca)
{
    do
    {
        menu = 0;
        Console.WriteLine($"Você deseja colocar seu barco na linha {colunaPeca + 1} e qual sentido para ESQUERDA ou DIREITA ?");
        Console.Write("DIGITE 1 PARA ESQUERDA OU 2 PARA DIREITA :");
        menu = int.Parse(Console.ReadLine());
    } while (menu != 1 && menu != 2);

    if (ValidaEspacoVazioHorizontal(tabuleiro, linhaPeca, colunaPeca, tamanhoPeca))
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
    {
        Console.WriteLine("Posição Inválida, pressione ENTER para continuar");
        Console.ReadKey();
        Console.Clear();
        ValidacaoPecaHorizontal(tabuleiro, colunaPeca, linhaPeca, tamanhoPeca);
    }
}
void ValidacaoPecaVertical(string[,] tabuleiro, int colunaPeca, int linhaPeca, int tamanhoPeca)
{
    do
    {
        Console.Clear();
        menu = 0;
        Console.WriteLine($"Você deseja colocar seu barco na coluna {colunaPeca + 1} e qual sentido para CIMA ou BAIXO ?");
        Console.Write("DIGITE 1 PARA CIMA OU 2 PARA BAIXO :");
        menu = int.Parse(Console.ReadLine());
    } while (menu != 1 && menu != 2);

    if (ValidaEspacoVazioVertical(tabuleiro, linhaPeca, colunaPeca, tamanhoPeca))
    {
        if (menu == 1)
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
    {
        Console.WriteLine("Posição Inválida, pressione ENTER para continuar");
        Console.ReadKey();
        Console.Clear();
        ValidacaoPecaVertical(tabuleiro, colunaPeca, linhaPeca, tamanhoPeca);
    }
}
bool ValidaEspacoVazioHorizontal(string[,] tabuleiro, int linhaPeca, int colunaPeca, int tamanhoPeca)
{
    if (menu == 1)
    {
        int limiteEsquerda = colunaPeca - tamanhoPeca;
        for (int i = colunaPeca; i > limiteEsquerda; i--)
        {
            if (i >= tabuleiro.GetLength(1) || i < 0)
            {
                return false;
            }
            else if (tabuleiro[linhaPeca, i] == "x")
            {
                return false;
            }
        }
    }
    else
    {
        int limiteDireita = colunaPeca + tamanhoPeca;
        for (int i = colunaPeca; i < limiteDireita; i++)
        {
            if (i >= tabuleiro.GetLength(1) || i < 0)
            {
                return false;
            }
            else if (tabuleiro[linhaPeca, i] == "x")
            {
                return false;
            }

        }
    }
    return true;
}
bool ValidaEspacoVazioVertical(string[,] tabuleiro, int linhaPeca, int colunaPeca, int tamanhoPeca)
{
    if (menu == 1)
    {
        int limiteCima = linhaPeca - tamanhoPeca;
        for (int i = linhaPeca; i > limiteCima; i--)
        {
            if (i >= tabuleiro.GetLength(0) || i < 0)
            {
                return false;
            }
            else if (tabuleiro[i, colunaPeca] == "x")
            {
                return false;
            }
        }
    }
    else
    {

        int limiteBaixo = linhaPeca + tamanhoPeca;
        for (int i = linhaPeca; i < limiteBaixo; i++)
        {
            if (i >= tabuleiro.GetLength(0) || i < 0)
            {
                return false;
            }
            else if (tabuleiro[i, colunaPeca] == "x")
            {
                return false;
            }

        }
    }
    return true;
}

void LugarPecaPosicaoPecaComputador(string[,] tabuleiro)
{
    for (int j = 0; j < 4; j++)
    {

        MenuEscolhaComputador();
        contador++;
        bool posicionou = false;
        do
        {
            sentidopeca = RandomNumberGenerator.GetInt32(1, 3);
            linhaPeca = RandomNumberGenerator.GetInt32(1, 10);
            colunaPeca = RandomNumberGenerator.GetInt32(1, 10);


            if (sentidopeca == 1)
            {

                posicionou = ValidacaoPecaHorizontalComputador(tabuleiro, colunaPeca, linhaPeca, tamanhoPeca); 

            }
            else
            {

                posicionou = ValidacaoPecaVerticalComputador(tabuleiro, colunaPeca, linhaPeca, tamanhoPeca);

            }
        } while (posicionou == false); //Enquanto Posicionou for false ele continua rodando 
    }
}
bool ValidacaoPecaHorizontalComputador(string[,] tabuleiro, int colunaPeca, int linhaPeca, int tamanhoPeca)
{
    menu = RandomNumberGenerator.GetInt32(1, 3);

    if (ValidaEspacoVazioHorizontal(tabuleiro, linhaPeca, colunaPeca, tamanhoPeca))
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
        return true;
    }
    else
    {
        return false;
    }
}
bool ValidacaoPecaVerticalComputador(string[,] tabuleiro, int colunaPeca, int linhaPeca, int tamanhoPeca)
{
    menu = RandomNumberGenerator.GetInt32(1, 3);

    if (ValidaEspacoVazioVertical(tabuleiro, linhaPeca, colunaPeca, tamanhoPeca))
    {
        if (menu == 1)
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
        return true;
    }
    else
    {
 
        return false;
    }
}
string MenuEscolhaComputador()

{
    tamanhoPeca = 0;
    menu = 0;
    menu = RandomNumberGenerator.GetInt32(1, 5);


    for (int i = 0; i < contador; i++)
    {
        if (valorMenu[i] == menu)
        {

            return MenuEscolhaComputador();

        }
    }

    valorMenu[contador] = menu;

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
            return MenuEscolhaComputador();

    }
}


void AtaqueJogador() 
{
    Console.WriteLine("Qual Posição deseja atacar ?");
}

void TabuleiroComputador()
{
    PopulaTabuleiro(tabuleiro2);
    LugarPecaPosicaoPecaComputador(tabuleiro2);
    ImprimeTabuleiro(tabuleiro2);
}
void TabuleiroJogador()
{
    PopulaTabuleiro(tabuleiro1);
    LugarPecaPosicaoPeca(tabuleiro1);
}

TabuleiroJogador();
TabuleiroComputador();












