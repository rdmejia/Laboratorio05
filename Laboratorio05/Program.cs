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

            Equipo[] equipos = {
                new Equipo("Alemania", 3, 0, 0, 1, 0),
                new Equipo("España", 3, 0, 0, 1, 0),
                new Equipo("Japon", 3, 0, 0, 1, 0),
                new Equipo("Argentina", 3, 0, 0, 1, 0),
                new Equipo("Brazil", 3, 0, 0, 1, 0),
                new Equipo("Italia", 3, 0, 0, 1, 0),
                new Equipo("México", 3, 0, 0, 1, 0),
                new Equipo("Inglaterra", 3, 0, 0, 1, 0)
            };

            Equipo[][] resultados = Torneo.SimularTorneo(equipos);

            for (int i = 0; i < resultados.Length; i++)
            {
                Console.WriteLine("Fase " + (i + 1));
                Console.WriteLine("------------------");
                for (int j = 0; j < resultados[i].Length; j++)
                {
                    Console.WriteLine(resultados[i][j].Nombre);
                }
                Console.WriteLine();
            }

            Console.ReadKey();
        }
    }
}