namespace Laboratorio05
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Equipo equipo1 = new Equipo("e1", 3, 0, 0, 1, 0);
            Equipo equipo2 = new Equipo("e2", 3, 0, 0, 1, 0);

            Partido partido = new Partido(equipo1, equipo2);

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(partido.SeleccionarEquipoGanador().GetNombre());
            }
        }
    }
}