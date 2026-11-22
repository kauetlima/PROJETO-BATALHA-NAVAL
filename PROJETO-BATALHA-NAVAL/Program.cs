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


//Função respona
void Jogo(Tabuleiro tabuleiroJogo, Tabuleiro tabuleiroMaquina)
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

populaTabuleiroMaquina();
Jogo(tabuleiroJogo, tabuleiroMaquina);