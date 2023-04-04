using Laboratorio05;
using System;
using System.Reflection;

namespace Laboratorio05Tests
{
    [TestClass]
    public class PartidoTests
    {
        private Equipo equipo1Mock = new Equipo("e", 3, 0, 0, 0, 0);
        private Equipo equipo2Mock = new Equipo("e", 3, 0, 0, 0, 0);
        private Mock<IRandomGenerator> randomMock = new Mock<IRandomGenerator>();

        [TestInitialize]
        public void init()
        {
            IRandomGenerator.RandomGenerator = randomMock.Object;
        }

        [TestMethod, Timeout(5000)]
        public void AsignarEquiposTest() 
        {
            Equipo e1 = new Equipo("e1", 3, 0, 0, 0, 0);
            Equipo e2 = new Equipo("e2", 3, 0, 0, 0, 0);

            Partido partido = new Partido(e1, e2);

            Assert.AreSame(e1, partido.GetEquipo1(), "El equipo 1 no coincide con el equipo esperado");
            Assert.AreSame(e2, partido.GetEquipo2(), "El equipo 2 no coincide con el equipo esperado");
        }

        [TestMethod, Timeout(5000)]
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

            Partido partido = new Partido(equipo1Mock, equipo2Mock);
            var ganador = partido.SeleccionarEquipoGanador();

            Assert.AreSame(GetEquipo(ganadorEsperado), ganador, "El equipo ganador no es el esperado");

            //VerifyEquipo(equipo1Mock);
            //VerifyEquipo(equipo2Mock);

            randomMock.Verify(r => r.Next(), Times.Exactly(2), "El numero aleatorio debio haberse obtenido una vez por cada equipo");
        }

        [TestMethod, Timeout(5000)]
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

            Partido partido = new Partido(equipo1Mock, equipo2Mock);
            var ganador = partido.SeleccionarEquipoGanador();

            Assert.AreSame(equipo1Mock, ganador, "El equipo ganador no es el esperado");

            //VerifyEquipo(equipo1Mock);
            //VerifyEquipo(equipo2Mock);

            randomMock.Verify(r => r.Next(), Times.Exactly(8), "El numero aleatorio no se llamo la cantidad de veces esperada. Quizas no se resolvieron los empates");
        }

        [TestMethod, Timeout(5000)]
        public void MismoGanadorTest()
        {
            IRandomGenerator.RandomGenerator = (IRandomGenerator)typeof(RandomGenerator).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, new Type[] { }).Invoke(null);

            Partido partido = new Partido(new Equipo("", 3, 0, 0, 0, 0), new Equipo("", 0, 0, 3, 0, 0));
            var expected = partido.SeleccionarEquipoGanador();

            for (int i = 0; i < 100; i++)
            {
                Assert.AreSame(expected, partido.SeleccionarEquipoGanador(), $"El partido retorno un ganador distinto luego de ${i} llamadas a SeleccionarEquipoGanador()");
            }

        }

        private void MockEquipo(Equipo equipoMock, int pg, int pp, int pe, int gf, int gc)
        {
            equipoMock.GetType()
                .GetField("PartidosGanados", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?
                .SetValue(equipoMock, pg);

            equipoMock.GetType()
                .GetField("PartidosEmpatados", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?
                .SetValue(equipoMock, pe);

            equipoMock.GetType().GetField("PartidosPerdidos", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?
                .SetValue(equipoMock, pp);

            equipoMock.GetType().GetField("GolesFavor", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?
                .SetValue(equipoMock, gf);

            equipoMock.GetType().GetField("GolesContra", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?
                .SetValue(equipoMock, gc);
        }

        private Equipo GetEquipo(string equipo)
        {
            if ("equipo1" == equipo)
                return equipo1Mock;

            return equipo2Mock;
        }
    }
}
