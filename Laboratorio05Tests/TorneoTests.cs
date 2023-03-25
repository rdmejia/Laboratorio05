namespace Laboratorio05Tests
{
    [TestClass]
    public class TorneoTests
    {
        private TestContext testContextInstance;
        private Mock<IRandomGenerator> randomMock = new Mock<IRandomGenerator>();

        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        [TestInitialize] public void Initialize()
        {
            IRandomGenerator.RandomGenerator = randomMock.Object;
        }

        [TestMethod]
        [DynamicData(nameof(GetSimpleTestData), DynamicDataSourceType.Method)]
        public void GanadorSimpleTest(Equipo[] equipos, double[] randomNumbers, string[][] nombreEsperados)
        {
            var dictionary = GetDictionary(equipos);
            MockRandomNumbers(randomNumbers);

            var result = Torneo.SimularTorneo(equipos);

            Assert.IsNotNull(result, "El resultado del torneo es null");
            Assert.AreEqual(nombreEsperados.GetLength(0), result.GetLength(0), "El resultado del no tiene la cantidad de fases esperadas");

            for (int i = 0; i < result.GetLength(0); i++)
            {
                for (int j = 0; j < result[i].Length; i++)
                {
                    Assert.AreEqual(dictionary[nombreEsperados[i][j]], result[i][j], $"Error en la fase {i}, el equipo en la posicion {j} no es el esperado.");
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        [DataRow(1, "Si el total de equipos no es mayor a 1, se debe lanzar una excepcion")]
        [DataRow(0, "Si el total de equipos no es mayor a 1, se debe lanzar una excepcion")]
        [DataRow(3, "Si el total de equipos no es una potencia de 2, debe lanzar una excepcion")]
        [DataRow(5, "Si el total de equipos no es una potencia de 2, debe lanzar una excepcion")]
        [DataRow(7, "Si el total de equipos no es una potencia de 2, debe lanzar una excepcion")]
        [DataRow(15, "Si el total de equipos no es una potencia de 2, debe lanzar una excepcion")]
        [DataRow(13, "Si el total de equipos no es una potencia de 2, debe lanzar una excepcion")]
        [DataRow(int.MaxValue, "Si el total de equipos no es una potencia de 2, debe lanzar una excepcion")]
        public void TorneosNoValidosTest(int cantidadEquipos, string message)
        {
            Equipo[] equipos = new Equipo[cantidadEquipos];
            
            Torneo.SimularTorneo(equipos);

            TestContext.WriteLine(message);
        }

        public static IEnumerable<object[]> GetSimpleTestData()
        {
            yield return new object[] { new Equipo[] { new Equipo("Argentina", 3, 0, 0, 1, 0), new Equipo("Brasil", 0, 3, 0, 0, 1) }, new double[] { 1.0, -1.0 }, new string[][] { new string[] { "Argentina", "Brasil" }, new string[] { "Argentina" } } };
        }

        private void MockRandomNumbers(double[] randomNumbers)
        {
            var sequence = randomMock.SetupSequence(r => r.Next());

            foreach (var number in randomNumbers)
            {
                sequence.Returns(number);
            }
        }
        private static Dictionary<string, Equipo> GetDictionary(Equipo[] equipos)
        {
            Dictionary<string, Equipo> dictionary = new Dictionary<string, Equipo>();

            foreach (Equipo equip in equipos)
            {
                dictionary.Add(equip.GetNombre(), equip);
            }

            return dictionary;
        }
    }
}
