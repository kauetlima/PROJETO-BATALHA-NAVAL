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

string tipoPeca = string.Empty;


MenuBarco menuBarco = new MenuBarco(0);
Tabuleiro tabuleiroJogo = new Tabuleiro();
Tabuleiro tabuleiroMaquina = new Tabuleiro();
Tabuleiro marcadorJogador1 = new Tabuleiro();

void PopulaTabuleiro(Tabuleiro tabuleiro)
{
    tabuleiro.PopularTabuleiro();
}
void PopulaMarcador(Tabuleiro marcador)
{
    marcador.PopularMarcador();
}
void ImprimeTabuleiro(Tabuleiro tabuleiro)
{
    tabuleiro.ImprimirTabuleiro();
}
void ImprimeMarcador(Tabuleiro marcador)
{
    marcador.ImprimirMarcador();
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
void PosicionarBarcoJogador()
{

    PopulaMarcador(marcadorJogador1);
    PopulaTabuleiro(tabuleiroJogo);

    for (int j = 0; j < 4; j++)
    {
        Console.Clear();

        ImprimeTabuleiro(tabuleiroJogo);

        Barco barco = MenuEscolha();

        if (barco == null)
        {
            Console.WriteLine("ERRO: O barco está nulo após MenuEscolha()");
            return;
        }

        Console.WriteLine($"Barco criado com tamanho: {barco.Tamanho}");

        int opcaoMenuSentido = 0;
        bool posicionou = false;

        do
        {
            int opcaoMenu;
            do
            {
                Console.WriteLine("Deseja posicionar o barco na 1-horizontal | 2-vertical ?");
                if (!int.TryParse(Console.ReadLine(), out opcaoMenu))
                {
                    Console.WriteLine("Digite um número válido!");
                }

            } while (opcaoMenu != 1 && opcaoMenu != 2);

            MenuPosicionar menuPosicionar = new MenuPosicionar(opcaoMenu);

            Console.WriteLine("Digite a linha que deseja posicionar:");
            menuPosicionar.Linha = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite a coluna que deseja posicionar:");
            menuPosicionar.Coluna = int.Parse(Console.ReadLine());

            if (opcaoMenu == 1)
            {
                Console.WriteLine("Deseja posicionar a peça para 1-esquerda | 2-direita?");
            }
            else if (opcaoMenu == 2)
            {
                Console.WriteLine("Deseja posicionar a peça 1-para cima | 2-para baixo?");
            }
            do
            {
                if (!int.TryParse(Console.ReadLine(), out opcaoMenuSentido) || (opcaoMenuSentido != 1 && opcaoMenuSentido != 2))
                {
                    Console.WriteLine("Opção inválida! Digite 1 ou 2:");
                }

            } while (opcaoMenuSentido != 1 && opcaoMenuSentido != 2);

            posicionou = menuPosicionar.CoordenadaPeca(barco, tabuleiroJogo, menuPosicionar, opcaoMenuSentido);
            if (posicionou)
            {
                Console.WriteLine("Barco posicionado com sucesso!");
                menuBarco.RegistrarBarco(menuBarco.OpcaoMenu);
            }
            else
            {
                Console.WriteLine("Não foi possível posicionar o barco. Tente novamente.");
            }

            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
            Console.Clear();

        } while (posicionou == false);

        Console.Clear();
    }
}
void populaTabuleiroMaquina()
{
    tabuleiroMaquina.PopularTabuleiro();
    tabuleiroMaquina.PopularMarcador();

    MenuBarco menuBarcoMaquina = new MenuBarco(0);

    int i = 1;

    while (i <= 4)
    {
        Barco? b = menuBarcoMaquina.BarcoMenu(i);

        if (b == null)
        {
            return;
        }

        
        int linha = RandomNumberGenerator.GetInt32(1, 11);
        int coluna = RandomNumberGenerator.GetInt32(1, 11);
        
        int orientacao = RandomNumberGenerator.GetInt32(1, 3);

        int variacao = RandomNumberGenerator.GetInt32(1, 3);

        MenuPosicionar p = new MenuPosicionar(orientacao);

        p.Linha = linha;
        p.Coluna = coluna;

        if (p.CoordenadaPeca(b, tabuleiroMaquina, p, variacao))
        {
            i++;
        }
    }
    Console.Clear();
    Console.WriteLine("Barcos da máquina posicionados!\n");
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
        Console.Clear();
        
        ImprimeTabuleiro(tabuleiroJogo);
        ImprimeTabuleiro(tabuleiroMaquina);

        Console.WriteLine("\n====================================");
        Console.WriteLine("           SEUS ATAQUES");
        Console.WriteLine("====================================");

        // mostra os ataques feitos contra a máquina
        tabuleiroMaquina.ImprimirMarcador();

        Console.WriteLine("X = Acerto | O = Erro | ~ = Não atacado");

        Console.WriteLine("\nDigite a posição que deseja atacar.");

        Console.WriteLine("LINHA:");
        if (int.TryParse(Console.ReadLine(), out int posicaoLinha))
        {
            if (posicaoLinha - 1 >= tabuleiroJogo.Linhas ||
                posicaoLinha - 1 < 0)
            {
                Console.WriteLine("Posição inválida!");
                Console.ReadKey();
                continue;
            }

            ataqueLinha = posicaoLinha - 1;
        }
        else
        {
            Console.WriteLine("Digite uma posição válida!");
            Console.ReadKey();
            continue;
        }

        Console.WriteLine("COLUNA:");
        if (int.TryParse(Console.ReadLine(), out int posicaoColuna))
        {
            if (posicaoColuna - 1 >= tabuleiroJogo.Colunas ||
                posicaoColuna - 1 < 0)
            {
                Console.WriteLine("Posição inválida!");
                Console.ReadKey();
                continue;
            }

            ataqueColuna = posicaoColuna - 1;
        }
        else
        {
            Console.WriteLine("Digite uma posição válida!");
            Console.ReadKey();
            continue;
        }

   
        if (tabuleiroMaquina.Marcador[ataqueLinha, ataqueColuna] == "X" ||
            tabuleiroMaquina.Marcador[ataqueLinha, ataqueColuna] == "O")
        {
            Console.WriteLine("Você já atacou essa posição!");
            Console.ReadKey();
            continue;
        }


        if (tabuleiroMaquina.VerificarAtaque(ataqueLinha, ataqueColuna))
        {
            Console.WriteLine("Você acertou um barco!");
            pontos++;
        }
        else
        {
            Console.WriteLine("Que pena, você errou!");
        }

      
        if (pontos == 14)
        {
            break;
        }

        Console.WriteLine("\nVez da máquina!");

        // evita que a máquina ataque uma posição repetida
        do
        {
            ataqueLinha = RandomNumberGenerator.GetInt32(0, 10);
            ataqueColuna = RandomNumberGenerator.GetInt32(0, 10);

        } while (
            tabuleiroJogo.Marcador[ataqueLinha, ataqueColuna] == "X" ||
            tabuleiroJogo.Marcador[ataqueLinha, ataqueColuna] == "O"
        );

        // ataque da máquina
        if (tabuleiroJogo.VerificarAtaque(ataqueLinha, ataqueColuna))
        {
            Console.WriteLine($"A máquina acertou na linha {ataqueLinha + 1}, coluna {ataqueColuna + 1}");
            pontosMaquina++;
        }
        else
        {
            Console.WriteLine($"A máquina errou na linha {ataqueLinha + 1}, coluna {ataqueColuna + 1}");
        }

        Console.WriteLine("\nPressione qualquer tecla para continuar...");
        Console.ReadKey();
    }

    Console.Clear();

    ImprimeTabuleiro(tabuleiroJogo);


    tabuleiroMaquina.ImprimirMarcador();

    if (pontos >= 14)
    {
        Console.WriteLine("\nParabéns, você ganhou!");
    }
    else if (pontosMaquina >= 14)
    {
        Console.WriteLine("\nQue pena, você perdeu!");
    }
}


PosicionarBarcoJogador();
populaTabuleiroMaquina();
Jogo(tabuleiroJogo, tabuleiroMaquina);