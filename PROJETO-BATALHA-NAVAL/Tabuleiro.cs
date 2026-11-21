
public class Tabuleiro
{
    public string[,] TabuleiroJogo { get; set; }
    public string[,] Marcador { get; set; }
    public int Linhas { get; } = 10;
    public int Colunas { get; } = 10;

    public Tabuleiro()
    {
        TabuleiroJogo = new string[Linhas, Colunas];
        Marcador = new string[Linhas, Colunas];
    }

    public void PopularTabuleiro()
    {
        for (int linha = 0; linha < Linhas; linha++)
        {
            for (int coluna = 0; coluna < Colunas; coluna++)
            {
                TabuleiroJogo[linha, coluna] = "~";
            }
        }
    }

    public void ImprimirTabuleiro()
    {
        Console.Write("  ");
        for (int i = 0; i < Colunas; i++)
        {
            Console.Write(i + " ");
        }
        Console.WriteLine();

        for (int linha = 0; linha < Linhas; linha++)
        {
            Console.Write(linha + " ");

            for (int coluna = 0; coluna < Colunas; coluna++)
            {
                Console.Write(TabuleiroJogo[linha, coluna] + " ");
            }
            Console.WriteLine();
        }
    }

    public void PopularMarcador()
    {
        for (int linha = 0; linha < Linhas; linha++)
        {
            for (int coluna = 0; coluna < Colunas; coluna++)
            {
                Marcador[linha, coluna] = "~";
            }
        }
    }

    public void ImprimirMarcador()
    {
        Console.Write("   ");
        for (int coluna = 0; coluna < Colunas; coluna++)
        {
            Console.Write(" ");
            Console.Write(coluna + " ");
        }
        Console.WriteLine();
        for (int linha = 0; linha < Linhas; linha++)
        {
            Console.Write(linha + "  ");
            for (int coluna = 0; coluna < Colunas; coluna++)
            {
                Console.Write($"[{Marcador[linha, coluna]}]");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    public bool VerificarAtaque(int ataquelinha, int ataquecoluna)
    {
        if (ataquelinha < 0 || ataquelinha >= TabuleiroJogo.GetLength(0) ||
            ataquecoluna < 0 || ataquecoluna >= TabuleiroJogo.GetLength(1))
        {
            return false;
        }

        if (Marcador[ataquelinha, ataquecoluna] == "X" ||
            Marcador[ataquelinha, ataquecoluna] == "O")
        {
            return false;
        }

        if (TabuleiroJogo[ataquelinha, ataquecoluna] == "X")
        {
            Marcador[ataquelinha, ataquecoluna] = "X";
            return true;
        }
        else
        {
            Marcador[ataquelinha, ataquecoluna] = "O";
            return false;
        }
    }
    public bool ValidaHorizontal(int tamanhoBarco, int linha, int coluna, int opcaoMenu)
    {
        if (linha < 0 || linha >= TabuleiroJogo.GetLength(0))
        {
            return false;
        }
        if (coluna < 0 || coluna >= TabuleiroJogo.GetLength(1))
        {
            return false;
        }

        if (opcaoMenu == 1) //esquerda
        {
            int limiteEsquerda = coluna - tamanhoBarco;
            if (limiteEsquerda >= 0)
            {
                return false;
            }
            for (int i = coluna; i > limiteEsquerda; i--)
            {
                if (i >= TabuleiroJogo.GetLength(1) || i < 0)
                {
                    return false;
                }
                else if (TabuleiroJogo[linha, i] == "X")
                {
                    return false;
                }
            }
        }
        else //direita
        {
            int limiteDireita = coluna + tamanhoBarco;
            if (limiteDireita > TabuleiroJogo.GetLength(1))
            {
                return false;
            }
            for (int i = coluna; i < limiteDireita; i++)
            {
                if (i >= TabuleiroJogo.GetLength(1) || i < 0)
                {
                    return false;
                }
                else if (TabuleiroJogo[linha, i] == "X")
                {
                    return false;
                }

            }
        }
        return true;
    }
    public bool PosicionaBarcoHorizontal(int tamanhoBarco, int linha, int coluna, int opcaoMenuSentido)
    {
        if (ValidaHorizontal(tamanhoBarco, linha, coluna, opcaoMenuSentido))
        {
            if (opcaoMenuSentido == 1)
            {
                int limiteEsquerda = coluna - tamanhoBarco;
                for (int i = coluna; i > limiteEsquerda; i--)
                {
                    TabuleiroJogo[linha, i] = "X";
                }

            }
            else
            {
                int limiteDireita = coluna + tamanhoBarco;
                for (int i = coluna; i < limiteDireita; i++)
                {
                    TabuleiroJogo[linha, i] = "X";
                }
            }
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool ValidaVertical(int tamanhoBarco, int linha, int coluna, int opcaoMenuSentido)
    {

        if (linha < 0 || linha >= TabuleiroJogo.GetLength(0))
        {
            return false;
        }
        if (coluna < 0 || coluna >= TabuleiroJogo.GetLength(1))
        {
            return false;
        }

        if (opcaoMenuSentido == 1) //cima
        {
            int limiteCima = linha - tamanhoBarco;
            for (int i = linha; i > limiteCima; i--)
            {
                if (i >= TabuleiroJogo.GetLength(0) || i < 0)
                {
                    return false;
                }
                else if (TabuleiroJogo[i, coluna] == "X")
                {
                    return false;
                }
            }
        }
        else //baixo
        {
            int limiteBaixo = linha + tamanhoBarco;
            for (int i = linha; i < limiteBaixo; i++)
            {
                if (i >= TabuleiroJogo.GetLength(0) || i < 0)
                {
                    return false;
                }
                else if (TabuleiroJogo[i, coluna] == "X")
                {
                    return false;
                }
            }
        }
        return true;
    }
    public bool PosicionaBarcoVertical(int tamanhoBarco, int linha, int coluna, int opcaoMenuSentido)
    {
        if (ValidaVertical(tamanhoBarco, linha, coluna, opcaoMenuSentido))
        {
            if (opcaoMenuSentido == 1)
            {
                int limiteCima = linha - tamanhoBarco;
                for (int i = linha; i > limiteCima; i--)
                {
                    TabuleiroJogo[i, coluna] = "X";
                }
            }
            else
            {
                int limiteBaixo = linha + tamanhoBarco;
                for (int i = linha; i < limiteBaixo; i++)
                {
                    TabuleiroJogo[i, coluna] = "X";
                }
            }
            return true;
        }
        else
        {
            return false;
        }
    }
}

