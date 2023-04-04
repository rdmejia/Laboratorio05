using System;
using System.Collections.Generic;
using System.Linq;

namespace Laboratorio05
{
    public class Torneo
    {

        public static Equipo[][] SimularTorneo(Equipo[] equipos)
        {
            // Se verifica si la cantidad de equipos es una potencia de dos y hay al menos dos equipos
            if (equipos.Length < 2 || !IsPowerOf2(equipos.Length))
                throw new Exception("La cantidad de equipos debe ser una potencia de dos y deben existir al menos dos equipos");

            // Se calcula la cantidad de fases que tendrá el torneo
            int fases = (int)Math.Log2(equipos.Length) + 1;


            Equipo[][] resultados = new Equipo[fases][];

           
            resultados[0] = equipos;

            for (int fase = 1; fase < fases; fase++)
            {
                // Se calcula la cantidad de equipos que avanzarán a la siguiente fase
                int equiposRestantes = equipos.Length / (1 << fase);

                // Se inicializa el arreglo de resultados para la fase actual con el tamaño correspondiente
                resultados[fase] = new Equipo[equiposRestantes];

                for (int i = 0; i < equiposRestantes; i++)
                {
                    Partido partido = new Partido(resultados[fase - 1][i], resultados[fase - 1][equipos.Length / (1 << (fase - 1)) - i - 1]);

                    Equipo ganador = partido.SeleccionarEquipoGanador();

                    // Se agrega al equipo ganador al arreglo de resultados para la fase actual
                    resultados[fase][i] = ganador;
                }
            }

            return resultados;
        }

        // Función que verifica si un número es una potencia de dos
        private static bool IsPowerOf2(int x)
        {
            if (x <= 0)
            {
                return false;
            }

            return (x & (x - 1)) == 0;
        }
    }
}
