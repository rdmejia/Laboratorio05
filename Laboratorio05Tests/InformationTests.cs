namespace Laboratorio05Tests
{
    [TestClass]
    public class InformationTests
    {
        private TestContext testContextInstance;

        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        [TestMethod, Timeout(5000)]
        public void TestInformation()
        {
            var information = Information.GetInformation();
            
            Assert.IsNotNull(information, "La informacion es NULL!");

            string name = information.GetName();
            string carnet = information.GetCarnet();
            string section = information.GetSection();

            Assert.IsFalse(string.IsNullOrWhiteSpace(name), "El nombre no ha sido especificado");
            Assert.IsFalse(string.IsNullOrWhiteSpace(carnet), "El carnet no ha sido especificado");
            Assert.IsFalse(string.IsNullOrWhiteSpace(section), "La seccion no ha sido especificada");

            Assert.IsTrue(name.Split(" ").Length >= 2, $"Al menos un nombre y un apellido debe ser especificado: {name}");

            TestContext.WriteLine($"Nombre: {name}");
            TestContext.WriteLine($"Carnet: {carnet}");
            TestContext.WriteLine($"Seccion: {section}");
        }
    }
}