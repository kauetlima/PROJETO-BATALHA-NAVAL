
public class Tabuleiro
{
    public string[,] TabuleiroJogo { get; set; } = new string[10, 10];
    public bool ValidaHorizontal(int tamanhoBarco,int linha,int coluna,int opcaoMenu)
    {

        if (opcaoMenu == 1) //esquerda
        {
            int limiteEsquerda = coluna - tamanhoBarco;
            for (int i = coluna; i > limiteEsquerda; i--)
            {
                if (i >= TabuleiroJogo.GetLength(1) || i < 0)
                {
                    return false;
                }
                else if (TabuleiroJogo[linha, i] == "x")
                {
                    return false;
                }
            }
        }
        else //direita
        {
            int limiteDireita = coluna + tamanhoBarco;
            for (int i = coluna; i < limiteDireita; i++)
            {
                if (i >= TabuleiroJogo.GetLength(1) || i < 0)
                {
                    return false;
                }
                else if (TabuleiroJogo[linha, i] == "x")
                {
                    return false;
                }

            }
        }
        return true;
    }
    public bool PosicionaBarcoHorizontal(int tamanhoBarco, int linha, int coluna, int opcaoMenuSentido)
    {
        if (ValidaHorizontal( tamanhoBarco,  linha,  coluna, opcaoMenuSentido))
        {
            if (opcaoMenuSentido == 1)
            {
                int limiteEsquerda = coluna - tamanhoBarco;
                for (int i = coluna; i > limiteEsquerda; i--)
                {
                    TabuleiroJogo[linha, i] = "x";
                }

            }
            else
            {
                int limiteDireita = coluna + tamanhoBarco;
                for (int i = coluna; i < limiteDireita; i++)
                {
                    TabuleiroJogo[linha , i] = "x";
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
        if (opcaoMenuSentido == 1) //cima
        {
            int limiteCima = linha - tamanhoBarco;
            for (int i = linha; i > limiteCima; i--)
            {
                if (i >= TabuleiroJogo.GetLength(0) || i < 0)
                {
                    return false;
                }
                else if (TabuleiroJogo[i, coluna] == "x")
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
                else if (TabuleiroJogo[i, coluna] == "x")
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
                    TabuleiroJogo[i, coluna] = "x";
                }
            }
            else
            {
                int limiteBaixo = linha + tamanhoBarco;
                for (int i = linha; i < limiteBaixo; i++)
                {
                    TabuleiroJogo[i, coluna] = "x";
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

