using Laboratorio05;
using System;

namespace Laboratorio05Tests
{
    [TestClass]
    public class PartidoTests
    {
        private Mock<Equipo> equipo1Mock = new Mock<Equipo>();
        private Mock<Equipo> equipo2Mock = new Mock<Equipo>();
        private Mock<IRandomGenerator> randomMock = new Mock<IRandomGenerator>();

        [TestInitialize]
        public void init()
        {
            IRandomGenerator.RandomGenerator = randomMock.Object;
        }

        [TestMethod]
        public void AsignarEquiposTest() 
        {
            Partido partido = new Partido(equipo1Mock.Object, equipo2Mock.Object);

            Assert.AreSame(equipo1Mock.Object, partido.GetEquipo1(), "El equipo 1 no coincide con el equipo esperado");
            Assert.AreSame(equipo2Mock.Object, partido.GetEquipo2(), "El equipo 2 no coincide con el equipo esperado");
        }

        [TestMethod]
        [DataRow(3, 0, 0, 1, 0, 0, 0, 0, 1, 0, 1.0, "equipo1")]
        [DataRow(3, 0, 0, 2, 3, 0, 0, 0, 2, 3, -1.0, "equipo1")]
        [DataRow(3, 0, 0, 2, 3, 0, 0, 0, 2, 3, -0.798132, "equipo1")]
        [DataRow(0, 0, 0, 0, 0, 3, 0, 0, 2, 3, -1.0, "equipo2")]
        [DataRow(0, 0, 0, 1, 0, 3, 0, 0, 1, 0, 1.0, "equipo2")]
        [DataRow(0, 0, 0, 1, 0, 3, 0, 0, 1, 0, 0.9999999, "equipo2")]
        [DataRow(0, 0, 0, 1, 0, 3, 0, 0, 1, 0, 0.000001, "equipo2")]
        [DataRow(0, 0, 0, 1, 0, 3, 0, 0, 1, 0, 0.03, "equipo2")]
        [DataRow(3, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0.15, "equipo1")]
        public void SeleccionarEquipoTest(int e1PG, int e1PP, int e1PE, int e1GF, int e1GC, int e2PG, int e2PP, int e2PE, int e2GF, int e2GC, double rand, string ganadorEsperado)
        {
            MockEquipo(equipo1Mock, e1PG, e1PP, e1PE, e1GF, e1GC);
            MockEquipo(equipo2Mock, e2PG, e2PP, e2PE, e2GF, e2GC);
            randomMock.Setup(r => r.Next()).Returns(rand);

            Partido partido = new Partido(equipo1Mock.Object, equipo2Mock.Object);
            var ganador = partido.SeleccionarEquipoGanador();

            Assert.AreSame(GetEquipo(ganadorEsperado), ganador, "El equipo ganador no es el esperado");

            VerifyEquipo(equipo1Mock);
            VerifyEquipo(equipo2Mock);

            randomMock.Verify(r => r.Next(), Times.Exactly(2), "El numero aleatorio debio haberse obtenido una vez por cada equipo");
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(0)]
        [DataRow(0)]
        [DataRow(0)]
        [DataRow(0)]
        [DataRow(0)]
        [DataRow(0)]
        [DataRow(0)]
        [DataRow(0)]
        [DataRow(0)]
        public void EmpateYGanadorTest(int random)
        {
            MockEquipo(equipo1Mock, 3, 0, 0, 0, 0);
            MockEquipo(equipo2Mock, 1, 0, 0, 0, 0);

            randomMock.SetupSequence(r => r.Next())
                .Returns(random)
                .Returns(random)
                .Returns(random)
                .Returns(random)
                .Returns(random)
                .Returns(random)
                .Returns(1)
                .Returns(1);

            Partido partido = new Partido(equipo1Mock.Object, equipo2Mock.Object);
            var ganador = partido.SeleccionarEquipoGanador();

            Assert.AreSame(equipo1Mock.Object, ganador, "El equipo ganador no es el esperado");

            VerifyEquipo(equipo1Mock);
            VerifyEquipo(equipo2Mock);

            randomMock.Verify(r => r.Next(), Times.Exactly(8), "El numero aleatorio no se llamo la cantidad de veces esperada. Quizas no se resolvieron los empates");
        }

        private void MockEquipo(Mock<Equipo> equipoMock, int pg, int pp, int pe, int gf, int gc)
        {
            equipoMock.Setup(e => e.GetPartidosGanados()).Returns(pg);
            equipoMock.Setup(e => e.GetPartidosPerdidos()).Returns(pp);
            equipoMock.Setup(e => e.GetPartidosEmpatados()).Returns(pe);
            equipoMock.Setup(e => e.GetGolesFavor()).Returns(gf);
            equipoMock.Setup(e => e.GetGolesContra()).Returns(gc);
        }

        private void VerifyEquipo(Mock<Equipo> equipoMock)
        {
            equipoMock.Verify(e => e.GetPartidosGanados(), Times.AtLeastOnce(), "No se consideraron los partidos ganados");
            equipoMock.Verify(e => e.GetPartidosPerdidos(), Times.AtLeastOnce(), "No se consideraron los partidos perdidos");
            equipoMock.Verify(e => e.GetPartidosEmpatados(), Times.AtLeastOnce(), "No se consideraron los partidos empatados");
            equipoMock.Verify(e => e.GetGolesFavor(), Times.AtLeastOnce(), "No se consideraron los goles a favor");
            equipoMock.Verify(e => e.GetGolesContra(), Times.AtLeastOnce(), "No se consideraron los goles en contra");
        }

        private Equipo GetEquipo(string equipo)
        {
            if ("equipo1" == equipo)
                return equipo1Mock.Object;

            return equipo2Mock.Object;
        }
    }
}
