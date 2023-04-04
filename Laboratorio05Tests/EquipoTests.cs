namespace Laboratorio05Tests
{
    [TestClass]
    public class EquipoTests
    {
        [TestMethod, Timeout(5000)]
        [DataRow("Equipo1", 0, 0, 3, 4, 5)]
        [DataRow("Equipo2", 1, 1, 1, 0, 0)]
        [DataRow("", 1, 2, 0, 1, 0)]
        [DataRow("A", 2, 0, 1, 2, 1)]
        [DataRow("b", 0, 3, 0, 2, 2)]
        [DataRow("1", 3, 0, 0, int.MaxValue, 0)]
        [DataRow("1", 3, 0, 0, 0, int.MaxValue)]
        public void CrearEquipoTest(string nombre, int partidosGanados, int partidosEmpatados, int partidosPerdidos, int golesFavor, int golesContra) 
        {
            Equipo equipo = new Equipo(nombre, partidosGanados, partidosEmpatados, partidosPerdidos, golesFavor, golesContra);

            Assert.AreEqual(nombre, equipo.GetNombre(), "El nombre del equipo no coincide");
            Assert.AreEqual(partidosGanados, equipo.GetPartidosGanados(), "Los partidos ganados por el equipo no coinciden");
            Assert.AreEqual(partidosEmpatados, equipo.GetPartidosEmpatados(), "Los partidos empatados por el equipo no coinciden");
            Assert.AreEqual(partidosPerdidos, equipo.GetPartidosPerdidos(), "Los partidos perdidos por el equipo no coinciden");
            Assert.AreEqual(golesFavor, equipo.GetGolesFavor(), "Los goles a favor del equipo no coinciden");
            Assert.AreEqual(golesContra, equipo.GetGolesContra(), "Los goles en contra del equipo no coinciden");
        }

        [TestMethod, Timeout(5000)]
        [ExpectedException(typeof(Exception))]
        [DataRow("", 10, 0, 0, 0, 0)]
        [DataRow("", -10, 0, 0, 0, 0)]
        [DataRow("", 0, 4, 0, 0, 0)]
        [DataRow("", 0, -4, 0, 0, 0)]
        [DataRow("", 0, 0, 5, 0, 0)]
        [DataRow("", 0, 0, -5, 0, 0)]
        [DataRow("", 0, 0, 0, 0, 0)]
        [DataRow("", 0, 2, 2, 0, 0)]
        [DataRow("", 2, 1, 1, 0, 0)]
        [DataRow("", -1, -1, -1, 0, 0)]
        [DataRow("", 0, 3, 0, 1, -5)]
        [DataRow("", 0, 3, 0, int.MinValue, 1)]
        public void CrearEquipoConExcepcionTest(string nombre, int partidosGanados, int partidosEmpatados, int partidosPerdidos, int golesFavor, int golesContra)
        {
            new Equipo(nombre, partidosGanados, partidosEmpatados, partidosPerdidos, golesFavor, golesContra);
        }
    }
}
