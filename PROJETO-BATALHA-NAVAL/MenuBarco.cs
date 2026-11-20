public class MenuBarco(int opcaoMenu) : Menu(opcaoMenu)
{
    private int[] Verificador = new int[4];
    private int Contador = 0;

    public Barco? BarcoMenu(int opcaoMenu)
    {
        
        if (opcaoMenu < 1 || opcaoMenu > 4)
        {
            Console.WriteLine("Opção inválida!");
            return null;
        }

         
        for (int i = 0; i < Contador; i++)
        {
            if (Verificador[i] == opcaoMenu)
            {
                Console.WriteLine("Você já escolheu esse barco!");
                return null;
            }
        }

        // Cria o barco escolhido
        Barco barco;

        switch (opcaoMenu)
        {
            case 1:
                this.OpcaoMenu = opcaoMenu;
                barco = new Barco(5, "Porta Aviões");
                break;

            case 2:
                this.OpcaoMenu = opcaoMenu;
                barco = new Barco(4, "Navio Tanque");
                break;

            case 3:
                this.OpcaoMenu = opcaoMenu;
                barco = new Barco(3, "Submarino");
                break;

            case 4:
                this.OpcaoMenu = opcaoMenu;
                barco = new Barco(2, "Contra-Torpedeiro");
                break;

            default:
                return null;
        }

        return barco;
    }

    public void RegistrarBarco(int opcaoMenu)
    {
        Verificador[Contador] = opcaoMenu;
        Contador++;
    }
}