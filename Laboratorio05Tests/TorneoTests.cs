using Laboratorio05;

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

        [TestMethod, Timeout(5000)]
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
                for (int j = 0; j < result[i].Length; j++)
                {
                    var expected = dictionary[nombreEsperados[i][j]];
                    Assert.AreEqual(expected, result[i][j], $"Error en la fase {i}, el equipo en la posicion {j} no es el esperado. Equipo del resultado: {result[i][j].GetNombre()}. Equipo esperado: {expected.GetNombre()}");
                }
            }
        }

        [TestMethod, Timeout(5000)]
        [ExpectedException(typeof(Exception))]
        [DataRow(1, "Si el total de equipos no es mayor a 1, se debe lanzar una excepcion")]
        [DataRow(0, "Si el total de equipos no es mayor a 1, se debe lanzar una excepcion")]
        [DataRow(3, "Si el total de equipos no es una potencia de 2, debe lanzar una excepcion")]
        [DataRow(5, "Si el total de equipos no es una potencia de 2, debe lanzar una excepcion")]
        [DataRow(7, "Si el total de equipos no es una potencia de 2, debe lanzar una excepcion")]
        [DataRow(15, "Si el total de equipos no es una potencia de 2, debe lanzar una excepcion")]
        [DataRow(13, "Si el total de equipos no es una potencia de 2, debe lanzar una excepcion")]
        [DataRow(1020, "Si el total de equipos no es una potencia de 2, debe lanzar una excepcion")]
        public void TorneosNoValidosTest(int cantidadEquipos, string message)
        {
            Equipo[] equipos = new Equipo[cantidadEquipos];
            
            Torneo.SimularTorneo(equipos);

            TestContext.WriteLine(message);
        }

        public static IEnumerable<object[]> GetSimpleTestData()
        {
            yield return new object[] {
                new Equipo[] { new Equipo("Argentina", 3, 0, 0, 1, 0), new Equipo("Brasil", 0, 3, 0, 0, 1) },
                new double[] { 1.0, -1.0 },
                new string[][] { new string[] { "Argentina", "Brasil" }, new string[] { "Argentina" } }
            };

            yield return new object[] {
                new Equipo[] { new Equipo("Bayern Munich", 3, 0, 0, 0, 0), new Equipo("PSG", 3, 0, 0, 0, 0), new Equipo("Manchester City", 3, 0, 0, 0, 0), new Equipo("Municipal", 3, 0, 0, 0, 0) },
                new double[] { 0.0, 1.0, 1.0, 0.0, 1.0, 0.0 },
                new string[][] { new string[] { "Bayern Munich", "PSG", "Manchester City", "Municipal" }, new string[] { "Municipal", "PSG" }, new string[] { "Municipal" } }
            };

            yield return new object[] {
                new Equipo[] { new Equipo("Alemania", 3, 0, 0, 0, 0), new Equipo("Japón", 3, 0, 0, 0, 0), new Equipo("Brazil", 3, 0, 0, 0, 0), new Equipo("México", 3, 0, 0, 0, 0), new Equipo("Inglaterra", 3, 0, 0, 0, 0), new Equipo("Italia", 3, 0, 0, 0, 0), new Equipo("Argentina", 3, 0, 0, 0, 0), new Equipo("España", 3, 0, 0, 0, 0), },
                new double[] { 1.0, 0.0, 1.0, 0.0, 1.0, 0.0, 0.0, 1.0, 1.0, 0.0, 0.0, 1.0, 1.0, 0.0 },
                new string[][] {
                    new string[] { "Alemania", "Japón", "Brazil", "México", "Inglaterra", "Italia", "Argentina", "España" },
                    new string[] { "Alemania", "Japón", "Brazil", "Inglaterra" },
                    new string[] { "Alemania", "Brazil" },
                    new string[] { "Alemania" }
                } };

            yield return new object[] {
                Enumerable.Range(0, 32)
                    .Select(i => new Equipo("e" + i, 3, 0, 0, 0, 0))
                    .ToArray(),
                Enumerable.Range(0, (32 - 1) * 2)
                    .Select(i => i % 2 == 0 ? 0.0d : 1.0d)
                    .ToArray(),
                BuildExpected(32)
            };

            yield return new object[] {
                Enumerable.Range(0, 64)
                    .Select(i => new Equipo("e" + i, 3, 0, 0, 0, 0))
                    .ToArray(),
                Enumerable.Range(0, (64 - 1) * 2)
                    .Select(i => i % 2 == 0 ? 0.0d : 1.0d)
                    .ToArray(),
                BuildExpected(64)
            };

            yield return new object[] {
                Enumerable.Range(0, 512)
                    .Select(i => new Equipo("e" + i, 3, 0, 0, 0, 0))
                    .ToArray(),
                Enumerable.Range(0, (512 - 1) * 2)
                    .Select(i => i % 2 == 0 ? 0.0d : 1.0d)
                    .ToArray(),
                BuildExpected(512)
            };

            yield return new object[] {
                Enumerable.Range(0, 1024)
                    .Select(i => new Equipo("e" + i, 3, 0, 0, 0, 0))
                    .ToArray(),
                Enumerable.Range(0, (1024 - 1) * 2)
                    .Select(i => i % 2 == 0 ? 0.0d : 1.0d)
                    .ToArray(),
                BuildExpected(1024)
            };

            yield return new object[] {
                Enumerable.Range(0, 4096)
                    .Select(i => new Equipo("e" + i, 3, 0, 0, 0, 0))
                    .ToArray(),
                Enumerable.Range(0, (4096 - 1) * 2)
                    .Select(i => i % 2 == 0 ? 0.0d : 1.0d)
                    .ToArray(),
                BuildExpected(4096)
            };

            yield return new object[] {
                Enumerable.Range(0, 32768)
                    .Select(i => new Equipo("e" + i, 3, 0, 0, 0, 0))
                    .ToArray(),
                Enumerable.Range(0, (32768 - 1) * 2)
                    .Select(i => i % 2 == 0 ? 0.0d : 1.0d)
                    .ToArray(),
                BuildExpected(32768)
            };
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

        private static string[][] BuildExpected(int x)
        {
            int newSize = (int)Math.Log2(x) + 1;
            string[][] result = new string[newSize][];

            int[] arr = Enumerable.Range(0, x).ToArray();

            int total = x;
            int i = 0;

            while (total > 0)
            {
                result[i] = new string[total];

                for (int j = 0; j < total; j++)
                {
                    result[i][j] = "e" + arr[j];
                }

                for (int j = 0; j < total / 2; j++)
                {
                    arr[j] = arr[total - j - 1];
                }

                i++;
                total = total / 2;
            }

            return result;
        }
    }
}
