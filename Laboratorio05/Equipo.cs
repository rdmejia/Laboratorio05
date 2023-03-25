namespace Laboratorio05
{
    public class Equipo
    {
        private string Nombre;
        private int PartidosGanados;
        private int PartidosEmpatados;
        private int PartidosPerdidos;
        private int GolesFavor;
        private int GolesContra;

        public Equipo(string nombre, int partidosGanados, int partidosEmpatados, int partidosPerdidos, int golesFavor, int golesContra)
        {
            if (partidosEmpatados + partidosGanados + partidosPerdidos != 3 || golesContra < 0 || golesFavor < 0)
                throw new Exception("Revise el equipo. La sumatoria de partidos debe ser igual a 3 y los goles anotados y a favor deben ser mayores o iguales a cero.");

            this.Nombre = nombre;
            this.PartidosGanados = partidosGanados;
            this.PartidosEmpatados = partidosEmpatados;
            this.PartidosPerdidos = partidosPerdidos;
            this.GolesFavor= golesFavor;
            this.GolesContra = golesContra;
        }

        public string GetNombre()
        {
            return Nombre;
        }

        public int GetPartidosGanados()
        {
            return PartidosGanados;
        }

        public int GetPartidosEmpatados()
        {
            return PartidosEmpatados;
        }

        public int GetPartidosPerdidos()
        {
            return PartidosPerdidos;
        }

        public int GetGolesFavor()
        {
            return GolesFavor;
        }

        public int GetGolesContra()
        {
            return GolesContra;
        }
    }
}
