using System.ComponentModel;
using System.Text;
Console.OutputEncoding = Encoding.UTF8; // Para aceitar o caracter □

string[,] tabuleiro = new string[10, 10];
int[] valorMenu = new int[4];

int linhaPeca = 0;
int colunaPeca = 0;
int tamanhoPortaAviao = 5;
int tamanhoNavioTanque = 4;
int tamanhoSubmarino = 3;
int tamanhoContraTorpedeiro = 2;
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
    menu = 0;
    Console.WriteLine("Escolha a peça que deseja posicionar\n " +
        "\n 1 - PORTA AVIÕES" +
        "\n 2 - NAVIO TANQUE" +
        "\n 3 - SUBMARINO" +
        "\n 4 - CONTRATORPEDEIRO\n");
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
            return tipoPeca = "Porta Aviões";

        case 2:
            return tipoPeca = "Navio-Tanque";

        case 3:

            return tipoPeca = "Submarino";

        case 4:
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

                ValidacaoPortaAviaoHorizontal(tamanhoPortaAviao);
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

                ValidacaoPortaAviaoVertical(tamanhoPortaAviao);
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
void ValidacaoPortaAviaoHorizontal(int tamanhoPeca)
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

    if (ValidaEspacoVazioHorizontal(tabuleiro, linhaPeca, colunaPeca, tamanhoPortaAviao))
    {
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

            Console.Clear();
            if (ValidaEspacoVazioHorizontal(tabuleiro, linhaPeca, colunaPeca, tamanhoPortaAviao))
            {
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
    }
    else
        Console.WriteLine("Posição já está sendo utilizada");
    Console.Clear();
}
void ValidacaoPortaAviaoVertical(int tamanhoPeca)
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

    ValidaEspacoVazioVertical(tabuleiro, linhaPeca, colunaPeca, tamanhoPortaAviao);
    int limiteCima = linhaPeca + tamanhoPeca;
    int limiteBaixo = linhaPeca - tamanhoPeca;

    if (ValidaEspacoVazioVertical(tabuleiro, linhaPeca, colunaPeca, tamanhoPortaAviao))
    {
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
    else
    Console.WriteLine("Posição já está sendo utilizada");
    Console.Clear();
}
bool ValidaEspacoVazioHorizontal(string[,] tabuleiro, int linhaPeca, int colunaPeca, int tamanhoPeca)
{
    int limiteDireita = colunaPeca - tamanhoPeca;
    int limiteEsquerda = colunaPeca + tamanhoPeca;

    if (colunaPeca > 5)
    {
        for (; colunaPeca > limiteDireita; colunaPeca--)
        {
            if (tabuleiro[linhaPeca, colunaPeca] == "x")
            {
                return false;
            }
        }
    }
    else if (colunaPeca < 4)
    {
        for (; colunaPeca < limiteEsquerda; colunaPeca++)
        {
            if (tabuleiro[linhaPeca, colunaPeca] == "x")
            {
                return false; ;
            }
        }
    }
    else if (colunaPeca == 5)
    {
        switch (menu)
        {
            case 1:
                for (; colunaPeca > limiteDireita; colunaPeca--)
                {
                    if (tabuleiro[linhaPeca, colunaPeca] == "x")
                    {
                        return false;
                    }
                }
                break;

            case 2:
                for (; colunaPeca < limiteEsquerda; colunaPeca++)
                {
                    if (tabuleiro[linhaPeca, colunaPeca] == "x")
                    {
                        return false;
                    }
                }
                break;
        }
    }
    else if (colunaPeca == 4)
    {

        switch (menu)
        {
            case 1:
                for (; colunaPeca > limiteDireita; colunaPeca--)
                {
                    if (tabuleiro[linhaPeca, colunaPeca] == "x")
                    {
                        return false;
                    }
                }
                break;

            case 2:
                for (; colunaPeca < limiteEsquerda; colunaPeca++)
                {
                    if (tabuleiro[linhaPeca, colunaPeca] == "x")
                    {
                        return false;
                    }
                }
                break;

        }
    }
    return true;
}
bool ValidaEspacoVazioVertical(string[,] tabuleiro, int linhaPeca, int colunaPeca, int tamanhoPeca)
{
    int limiteCima = linhaPeca + tamanhoPeca;
    int limiteBaixo = linhaPeca - tamanhoPeca;

    if (linhaPeca > 5)
    {
        for (; linhaPeca > limiteBaixo; linhaPeca--)
        {
            if (tabuleiro[linhaPeca, colunaPeca] == "x")
            {
                return false;
            }
        }
    }
    else if (linhaPeca < 4)
    {
        for (; linhaPeca < limiteCima; linhaPeca++)
        {
            if (tabuleiro[linhaPeca, colunaPeca] == "x")
            {
                return false;
            }
        }
    }
    else if (linhaPeca == 5)
    {
        switch (menu)
        {
            case 1:
                for (; linhaPeca > limiteBaixo; linhaPeca--)
                {
                    if (tabuleiro[linhaPeca, colunaPeca] == "x")
                    {
                        return false;
                    }
                }
                break;

            case 2:
                for (; linhaPeca < limiteCima; linhaPeca++)
                {
                    if (tabuleiro[linhaPeca, colunaPeca] == "x")
                    {
                        return false;
                    }
                }
                break;

        }
    }
    else if (linhaPeca == 4)
    {
        switch (menu)
        {
            case 1:
                for (; linhaPeca > limiteBaixo; linhaPeca--)
                {
                    if (tabuleiro[linhaPeca, colunaPeca] == "x")
                    {
                        return false;
                    }
                }
                break;

            case 2:
                for (; linhaPeca < limiteCima; linhaPeca++)
                {
                    if (tabuleiro[linhaPeca, colunaPeca] == "x")
                    {
                        return false;
                    }
                }
                break;

        }
    }
    return true;
}


PopulaTabuleiro(tabuleiro);
PosicionaPecas();



