
using System.Net.Http.Headers;

public class MenuPosicionar(int opcaoMenu) : Menu(opcaoMenu)
{
    public int Linha { get; set; }
    public int Coluna { get; set; }

    public bool CoordenadaPeca(Barco barco, Tabuleiro tabuleiro, MenuPosicionar menuPosicionar, int opcaoMenuSentido)
    {
        int linha = menuPosicionar.Linha - 1;
        int coluna = menuPosicionar.Coluna - 1;

        if (opcaoMenu == 1) // Horizontal
        {
            return tabuleiro.PosicionaBarcoHorizontal(barco.Tamanho, linha, coluna, opcaoMenuSentido);
        }
        else if (opcaoMenu == 2) // Vertical
        {
            return tabuleiro.PosicionaBarcoVertical(barco.Tamanho, linha, coluna, opcaoMenuSentido);
        }

        return false;
    }
}

