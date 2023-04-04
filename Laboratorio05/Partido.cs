using System;

namespace Laboratorio05
{
    public class Partido
    {
        private Equipo equipo1;
        private Equipo equipo2;
        private Equipo? ganador;

        public Partido(Equipo equipo1, Equipo equipo2)
        {
            this.equipo1 = equipo1;
            this.equipo2 = equipo2;
            this.ganador = null;
        }

        public Equipo GetEquipo1()
        {
            return equipo1;
        }

        public Equipo GetEquipo2()
        {
            return equipo2;
        }

        public Equipo SeleccionarEquipoGanador()
        {
            if (ganador != null)
            {
                return ganador;
            }

            double puntajeEquipo1, puntajeEquipo2;
            do
            {
                puntajeEquipo1 = CalcularPuntaje(equipo1);
                puntajeEquipo2 = CalcularPuntaje(equipo2);
            } while (puntajeEquipo1 == puntajeEquipo2);

            //while (Math.Abs(puntajeEquipo1 - puntajeEquipo2) < 0.000001);

            ganador = puntajeEquipo1 > puntajeEquipo2 ? equipo1 : equipo2;

            return ganador;
        }

        private double CalcularPuntaje(Equipo equipo)
        {
            double x = IRandomGenerator.RandomGenerator.Next();
            double PG = equipo.GetPartidosGanados() * 0.7;
            double PP = equipo.GetPartidosPerdidos() * 0.1;
            double PE = equipo.GetPartidosEmpatados() * 0.2;
            double OM = equipo.GetGolesFavor() - equipo.GetGolesContra() + 0.001;

            if (OM == 0)
            {
                OM = 0.001;
            }

            return x * (PG + PP + PE + OM) / OM;
        }
    }
}