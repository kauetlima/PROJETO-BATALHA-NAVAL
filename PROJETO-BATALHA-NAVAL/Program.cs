using System.ComponentModel;
using System.Runtime.Intrinsics.Arm;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Text;
Console.OutputEncoding = Encoding.UTF8; // Para aceitar o caracter □

//teste

string[,] tabuleiro1 = new string[10, 10];
string[,] tabuleiro2 = new string[10, 10];
string[,] marcador1 = new string[10, 10];
string[,] marcador2 = new string[10, 10];
int[] valorMenu = new int[4];
int[,] posicaoMatriz = new int[11, 11];
int[] teste = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };



bool jogador1 = false;
bool jogador2 = false;
int linhaPeca = 0;
int colunaPeca = 0;
int tamanhoPeca = 0;
int menu = 0;
int contador = 0;
int sentidopeca = 0;
string tipoPeca = string.Empty;


MenuBarco menuBarco = new MenuBarco(0);
Tabuleiro tabuleiroJogo = new Tabuleiro();
Tabuleiro tabuleiroMaquina = new Tabuleiro();

void PopulaTabuleiro(string[,] tabuleiro)
{
    for (int linha = 0; linha < 10; linha++)
    {
        for (int coluna = 0; coluna < 10; coluna++)
        {
            tabuleiro[linha, coluna] = "□";

        }
    }
}
void PopulaMarcador(string[,] marcador)
{
    Console.WriteLine("=====================================");
    Console.WriteLine("               MARCADOR");
    Console.WriteLine("=====================================");

    for (int coluna = 0; coluna < 11; coluna++)
    {
        Console.Write($"[{teste[coluna]}]");
    }
    Console.WriteLine();
    for (int linha = 0; linha < 10; linha++)
    {

        Console.Write($"[{teste[linha]}]");

        for (int coluna = 0; coluna < 10; coluna++)
        {
            marcador[linha, coluna] = " ";
            Console.Write($"[{marcador[linha, coluna]}]");

        }
        Console.WriteLine();
    }
    Console.WriteLine("\n");
}
void ImprimeTabuleiro(string[,] tabuleiro)
{
    Console.WriteLine("=====================================");
    Console.WriteLine("             TABULEIRO");
    Console.WriteLine("=====================================");

    for (int coluna = 0; coluna < 11; coluna++)
    {
        Console.Write($"[{teste[coluna]}]");
    }
    Console.WriteLine();
    for (int linha = 0; linha < 10; linha++)
    {
        Console.Write($"[{teste[linha] + 1}]");

        for (int coluna = 0; coluna < 10; coluna++)
        {
            Console.Write($"[{tabuleiro[linha, coluna]}]");
        }
        Console.WriteLine();
    }
    Console.WriteLine();
}
void ImprimeMarcador(string[,] marcador)
{
    Console.WriteLine("=====================================");
    Console.WriteLine("               MARCADOR");
    Console.WriteLine("=====================================");
    for (int linha = 0; linha < 10; linha++)
    {
        for (int coluna = 0; coluna < 10; coluna++)
        {

            Console.Write($"[{marcador[linha, coluna]}]");
        }
        Console.WriteLine();
    }
    Console.WriteLine();
}


Barco MenuEscolha()
{
    Barco? barco = null;

    do
    {
        Console.WriteLine("===============================================================");
        Console.WriteLine("▲ ATENÇÃO: VOCÊ PODE TER APENAS 1 BARCO DE CADA TIPO EM CAMPO ▲");
        Console.WriteLine("===============================================================");

        Console.WriteLine("ESCOLHA O BARCO QUE IRÁ POSICIONAR:");
        Console.WriteLine("1 - PORTA AVIÕES (TAMANHO 5)");
        Console.WriteLine("2 - NAVIO TANQUE (TAMANHO 4)");
        Console.WriteLine("3 - SUBMARINO (TAMANHO 3)");
        Console.WriteLine("4 - CONTRA-TORPEDEIRO (TAMANHO 2)");

        Console.Write("-> ");

        if (int.TryParse(Console.ReadLine(), out int opcaoMenu))
        {
            barco = menuBarco.BarcoMenu(opcaoMenu);
        }
        else
        {
            Console.WriteLine("Digite um número válido!");
        }

    } while (barco == null);

    return barco;
}


for (int j = 0; j < 4; j++)
{
    Barco barco = MenuEscolha();

    bool posicionou = false;

    do
    {
        Console.WriteLine("Deseja posicionar o barco na horizontal ou vertical ?");
        if (int.TryParse(Console.ReadLine(), out int opcaoMenu))
        {
            barco = menuBarco.BarcoMenu(opcaoMenu);
        }
        else
        {
            Console.WriteLine("Digite um número válido!");
        }

        MenuPosicionar menuPosicionar = new MenuPosicionar(opcaoMenu);

        Console.WriteLine("Digite a linha que deseja posicionar:");
        menuPosicionar.Linha = int.Parse(Console.ReadLine());
        Console.WriteLine("Digite a coluna que deseja posicionar:");
        menuPosicionar.Coluna = int.Parse(Console.ReadLine());
        Console.WriteLine("Deseja Posicionar a peça para direita ou esquerda?");
        int opcaoMenuSentido = int.Parse(Console.ReadLine());

        posicionou = menuPosicionar.CoordenadaPeca(barco, tabuleiroJogo, menuPosicionar, opcaoMenuSentido);


        if (posicionou)
        {
            Console.WriteLine("Barco posicionado com sucesso!");
        }
        else
        {
            Console.WriteLine("Não foi possível posicionar o barco. Tente novamente.");
        }
    } while (posicionou == false);

    menuBarco.RegistrarBarco(menuBarco.OpcaoMenu);
}

void populaTabuleiroMaquina()
{
    tabuleiroMaquina.PopularTabuleiro();
    tabuleiroMaquina.PopularMarcador();
    int i = 1;
    while(i < 5)
    {
        int barco = i;
        int linha = RandomNumberGenerator.GetInt32(0, 9);
        int coluna = RandomNumberGenerator.GetInt32(0, 9);
        int orientacao = RandomNumberGenerator.GetInt32(1, 2);
        int variacao = RandomNumberGenerator.GetInt32(1, 2);
        MenuPosicionar p = new MenuPosicionar(orientacao);
        Barco? b = null;
        b = menuBarco.BarcoMenu(barco);
        if(p.CoordenadaPeca(b, tabuleiroMaquina, p, variacao))
        {
            i++;
        }
        else
        {
            continue;
        }
    }
}


//Função responsável pelo jogo
void Jogo(Tabuleiro tabuleiroMaquina)
{
    int ataqueLinha = 0;
    int ataqueColuna = 0;
    int pontos = 0;
    int pontosMaquina = 0;

    while (pontos < 14 && pontosMaquina < 14)
    {
        Console.WriteLine("Digite a posição que você deseja atacar.");
        Console.WriteLine("LINHA: ");
        if(int.TryParse(Console.ReadLine(), out int posicaoLinha))
        {
            if (posicaoLinha-1 > tabuleiroJogo.Linhas || posicaoLinha-1 < 0)
            {
                Console.WriteLine("Posição sobressai do escopo, digite uma posição válida");
                continue;
            }
            else
            {
                ataqueLinha = posicaoLinha-1;
            }
        }

        else {
            Console.WriteLine("Digite uma posição valida");
            continue;
        }

        Console.WriteLine("COLUNA: ");
        if (int.TryParse(Console.ReadLine(), out int posicaoColuna))
        {
            if (posicaoColuna-1 > tabuleiroJogo.Colunas || posicaoColuna-1 < 0)
            {
                Console.WriteLine("Posição sobressai do escopo, digite uma posição válida");
                continue;
            }
            else
            {
                ataqueColuna = posicaoColuna-1;
            }
        }
        else
        {
            Console.WriteLine("Digite uma opção valida");
            continue;
        }
        if(tabuleiroMaquina.VerificarAtaque(ataqueLinha, ataqueColuna))
        {
            Console.WriteLine($"Você acertou um barco na posição {ataqueLinha} {ataqueColuna}");
            pontos++;
        }
        else
        {
            Console.WriteLine("Que pena, errou.");
        }
        Console.WriteLine("Vez da maquina");

        tabuleiroJogo.ImprimirTabuleiro();
        ataqueLinha = RandomNumberGenerator.GetInt32(1, 10);
        ataqueColuna = RandomNumberGenerator.GetInt32(1, 10);

        if (tabuleiroJogo.VerificarAtaque(ataqueLinha, ataqueColuna))
        {
            Console.WriteLine($"A maquina acertou nas coordenadas {ataqueLinha} {ataqueColuna}");
            pontosMaquina++;
        }
        else
        {
            Console.WriteLine($"A maquina errou nas coordenadas {ataqueLinha} {ataqueColuna}");
        }
    }
    if(pontos==14)
    {
        Console.WriteLine("Parabéns você ganhou!");
    }
    if(pontosMaquina==14)
    {
        Console.WriteLine("Que pena, você perdeu.");
    }
}

//    void LugarPecaPosicaoPeca(string[,] tabuleiro)
//{
//    //FUNÇÃO IMPRESSÃO ESCOLHA POSIÇÃO ↓ (PROPRIA FUNÇÃO, NÃO ESTÁ CHAMANDO)
//    void ImpressaoEscolhePosicao(string tipoPeca)
//    {
//        Console.Clear();
//        Console.WriteLine("====================================");
//        Console.WriteLine($" POSICIONAR {tipoPeca}");
//        Console.WriteLine("====================================");
//        Console.WriteLine();
//        Console.WriteLine("Informe a posição inicial do navio.");
//        Console.WriteLine();
//        Console.WriteLine("Exemplo:");
//        Console.WriteLine("LINHA: 9");
//        Console.WriteLine("COLUNA: 10");
//        Console.Write("Digite as coordenadas (Linha | Coluna): ");
//        Console.WriteLine();
//    }



//for (int j = 0; j < 4; j++)
//{


//    Console.Clear();

//    bool posicionou = false;

//    do
//    {
//        Console.Clear();
//        ImprimeTabuleiro(tabuleiro);
//        Console.WriteLine();
//        Console.Write($"\nVocê irá posicionar o Posicionar o {tipoPeca} colocar na \n Digite 1 - HORIZONTAL \n Digite 2 - VERTICAL  \n-> ");
//        sentidopeca = int.Parse(Console.ReadLine());


//        if (sentidopeca == 1)
//        {
//            ImpressaoEscolhePosicao(tipoPeca);
//            ImprimeTabuleiro(tabuleiro);
//            Console.WriteLine("\n");
//            Console.Write("LINHA: ");
//            int linha = int.Parse(Console.ReadLine());

//            Console.Write("COLUNA: ");
//            int coluna = int.Parse(Console.ReadLine());


//            int[] armazenaPosicao = { linha, coluna };

//            linhaPeca = linha;
//            colunaPeca = coluna;

//            colunaPeca = colunaPeca - 1;
//            linhaPeca = linhaPeca - 1;

//            posicionou = ValidacaoPecaHorizontal(tabuleiro, colunaPeca, linhaPeca, tamanhoPeca, menu);
//            ImprimeTabuleiro(tabuleiro);
//        }
//        else
//        {
//            ImpressaoEscolhePosicao(tipoPeca);
//            ImprimeTabuleiro(tabuleiro);


//            Console.Write("LINHA: ");
//            int linha = int.Parse(Console.ReadLine());

//            Console.Write("COLUNA: ");
//            int coluna = int.Parse(Console.ReadLine());

//            ImprimeTabuleiro(tabuleiro);


//            int[] armazenaPosicao = { linha, coluna };

//            linhaPeca = linha;
//            colunaPeca = coluna;

//            colunaPeca = colunaPeca - 1;
//            linhaPeca = linhaPeca - 1;

//            posicionou = ValidacaoPecaVertical(tabuleiro, colunaPeca, linhaPeca, tamanhoPeca, menu);
//            ImprimeTabuleiro(tabuleiro);
//        }
//    } while (posicionou == false);

//    valorMenu[contador] = menu;
//    contador++;
//}

//}
bool ValidacaoPecaHorizontal(string[,] tabuleiro, int colunaPeca, int linhaPeca, int tamanhoPeca, int menu)
{
    do
    {
        menu = 0;
        Console.WriteLine($"Você deseja colocar seu barco na linha {colunaPeca + 1} e qual sentido para ESQUERDA ou DIREITA ?");
        Console.Write("DIGITE 1 PARA ESQUERDA OU 2 PARA DIREITA :");
        menu = int.Parse(Console.ReadLine());
        Console.Clear();
    } while (menu != 1 && menu != 2);

    if (ValidaEspacoVazioHorizontal(tabuleiro, linhaPeca, colunaPeca, tamanhoPeca, menu))
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
        Console.WriteLine("Posição Inválida, pressione ENTER para continuar");
        Console.ReadKey();
        Console.Clear();
        return false;
    }
}
bool ValidaEspacoVazioHorizontal(string[,] tabuleiro, int linhaPeca, int colunaPeca, int tamanhoPeca, int menu)
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
bool ValidacaoPecaVertical(string[,] tabuleiro, int colunaPeca, int linhaPeca, int tamanhoPeca, int menu)
{
    do
    {
        Console.Clear();
        menu = 0;
        Console.WriteLine($"Você deseja colocar seu barco na coluna {colunaPeca + 1} e qual sentido para CIMA ou BAIXO ?");
        Console.Write("DIGITE 1 PARA CIMA OU 2 PARA BAIXO :");
        menu = int.Parse(Console.ReadLine());
        Console.Clear();
    } while (menu != 1 && menu != 2);

    if (ValidaEspacoVazioVertical(tabuleiro, linhaPeca, colunaPeca, tamanhoPeca, menu))
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
        Console.WriteLine("Posição Inválida, pressione ENTER para continuar");
        Console.ReadKey();
        Console.Clear();
        return false;
    }
}
bool ValidaEspacoVazioVertical(string[,] tabuleiro, int linhaPeca, int colunaPeca, int tamanhoPeca, int menu)
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

string MenuEscolhaComputador(int[] valorMenu)
{
    tamanhoPeca = 0;
    menu = 0;
    menu = RandomNumberGenerator.GetInt32(1, 5);


    for (int i = 0; i < contador; i++)
    {
        if (valorMenu[i] == menu)
        {

            return MenuEscolhaComputador(valorMenu);

        }
    }

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
            return MenuEscolhaComputador(valorMenu);

    }
}
void LugarPecaPosicaoPecaComputador(string[,] tabuleiro)
{
    contador = 0;
    for (int j = 0; j < 4; j++)
    {

        MenuEscolhaComputador(valorMenu);

        bool posicionou = false;
        do
        {
            sentidopeca = RandomNumberGenerator.GetInt32(1, 3);
            linhaPeca = RandomNumberGenerator.GetInt32(1, 10);
            colunaPeca = RandomNumberGenerator.GetInt32(1, 10);


            if (sentidopeca == 1)
            {

                posicionou = ValidacaoPecaHorizontalComputador(tabuleiro, colunaPeca, linhaPeca, tamanhoPeca, menu);

            }
            else
            {

                posicionou = ValidacaoPecaVerticalComputador(tabuleiro, colunaPeca, linhaPeca, tamanhoPeca, menu);

            }
        } while (posicionou == false); //Enquanto Posicionou for false ele continua rodando 

        valorMenu[contador] = menu;
        contador++;
    }
}
bool ValidacaoPecaHorizontalComputador(string[,] tabuleiro, int colunaPeca, int linhaPeca, int tamanhoPeca, int menu)
{
    menu = RandomNumberGenerator.GetInt32(1, 3);

    if (ValidaEspacoVazioHorizontal(tabuleiro, linhaPeca, colunaPeca, tamanhoPeca, menu))
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

bool VerificaBarcos(string[,] tabuleiro)
{
    for (int i = 0; i < tabuleiro.GetLength(0); i++)
    {
        for (int j = 0; j < tabuleiro.GetLength(1); j++)
        {
            if (tabuleiro[i, j] == "x")
            {
                return true;
            }

        }
    }
    return false;
}
bool ValidacaoPecaVerticalComputador(string[,] tabuleiro, int colunaPeca, int linhaPeca, int tamanhoPeca, int menu)
{
    menu = RandomNumberGenerator.GetInt32(1, 3);

    if (ValidaEspacoVazioVertical(tabuleiro, linhaPeca, colunaPeca, tamanhoPeca, menu))
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


bool VerificaAtaque(string[,] tabuleiro, string[,] marcador, int ataqueLinha, int ataqueColuna)
{

    if (tabuleiro[ataqueLinha, ataqueColuna] == "x")
    {
        return true;
    }
    else
        return false;

}
bool AtaqueJogador1(string[,] tabuleiro, string[,] marcador)
{
    int ataqueLinha = 0;
    int ataqueColuna = 0;


    if (VerificaBarcos(tabuleiro))
    {
        ImprimeTabuleiro(tabuleiro1);
        ImprimeMarcador(marcador);
        Console.WriteLine("Qual Posição deseja atacar ?");
        Console.Write("LINHA: ");

        ataqueLinha = int.Parse(Console.ReadLine());
        Console.Write("COLUNA: ");
        ataqueColuna = int.Parse(Console.ReadLine());
        Console.Clear();

        ataqueLinha -= 1;
        ataqueColuna -= 1;

        if (ataqueLinha >= tabuleiro.GetLength(0) || ataqueColuna >= tabuleiro.GetLength(1) || ataqueLinha < 0 || ataqueColuna < 0)
        {
            return AtaqueJogador1(tabuleiro, marcador);
        }
        else if (marcador[ataqueLinha, ataqueColuna] == "B" || marcador[ataqueLinha, ataqueColuna] == "A")
        {
            Console.WriteLine("Você já atacou essa posição");
            Console.ReadKey();
            return AtaqueJogador1(tabuleiro, marcador);
        }
        else if (VerificaAtaque(tabuleiro, marcador, ataqueLinha, ataqueColuna))
        {
            Console.WriteLine("BOMBA");
            marcador[ataqueLinha, ataqueColuna] = "B";
            tabuleiro[ataqueLinha, ataqueColuna] = "B";
            ImprimeTabuleiro(tabuleiro1);
            ImprimeMarcador(marcador);
        }
        else
        {
            Console.WriteLine("ÁGUA");
            marcador[ataqueLinha, ataqueColuna] = "A";
            ImprimeTabuleiro(tabuleiro1);
            ImprimeMarcador(marcador);
        }

        if (!VerificaBarcos(tabuleiro))
        {
            Console.WriteLine("VOCÊ VENCEU");
            return true;
        }
    }

    return false;
}
bool AtaqueJogador2(string[,] tabuleiro, string[,] marcador)
{

    int ataqueLinha;
    int ataqueColuna;

    if (VerificaBarcos(tabuleiro))
    {
        do
        {
            ataqueLinha = RandomNumberGenerator.GetInt32(0, 10);
            ataqueColuna = RandomNumberGenerator.GetInt32(0, 10);

        } while (marcador[ataqueLinha, ataqueColuna] == "B" || marcador[ataqueLinha, ataqueColuna] == "A");


        if (VerificaAtaque(tabuleiro, marcador, ataqueLinha, ataqueColuna))
        {
            marcador[ataqueLinha, ataqueColuna] = "B";
            tabuleiro[ataqueLinha, ataqueColuna] = "B";

        }
        else
        {
            marcador[ataqueLinha, ataqueColuna] = "A";
        }
        if (!VerificaBarcos(tabuleiro))
        {
            return true;
        }
    }
    return false;
}

//void TabuleiroComputador()
//{
//    PopulaMarcador(marcador2);
//    PopulaTabuleiro(tabuleiro2);
//    ImprimeTabuleiro(tabuleiro2);
//    LugarPecaPosicaoPecaComputador(tabuleiro2);

//}
//void TabuleiroJogador()
//{
//    PopulaMarcador(marcador1);
//    PopulaTabuleiro(tabuleiro1);
//    ImprimeTabuleiro(tabuleiro1);
//    LugarPecaPosicaoPeca(tabuleiro1);
//}




//TabuleiroJogador();
//TabuleiroComputador();

//do
//{
//    Console.Clear();
//    jogador1 = false;
//    jogador2 = false;

//    jogador1 = AtaqueJogador1(tabuleiro2, marcador1);


//    if (jogador1 == true)
//    {
//        break;
//    }

//    jogador2 = AtaqueJogador2(tabuleiro1, marcador2);
//}
//while (jogador1 == false && jogador2 == false);