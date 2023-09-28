using EmployeeManagementSystem.Models.Employees;
using EmployeeManagementSystem.Repository.Employees;
using EmployeeManagementSystem.Services.Employees;
using Microsoft.Extensions.Logging;
using Moq;

namespace EmployeeManagementSystem.Services.Tests
{
    [TestFixture]
    public class EmployeeServiceTests
    {
        private EmployeeService _employeeService;
        private Mock<IEmployeeRepository> _mockEmployeeRepository;
        private Mock<ILogger<EmployeeService>> _mockILogger;

        [SetUp]
        public void Setup()
        {
            _mockEmployeeRepository = new Mock<IEmployeeRepository>();
            _mockILogger = new Mock<ILogger<EmployeeService>>();
            _employeeService = new EmployeeService(_mockEmployeeRepository.Object, _mockILogger.Object);
        }

        [Test]
        public async Task Test1()
        {
            var id = 1;

            _mockEmployeeRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(new Employee { Id = id});

           var result =  _employeeService.GetByIdAsync(id);

           _mockEmployeeRepository.Verify(x => x.GetByIdAsync(id), Times.Once);

           Assert.AreEqual(result.Id, id);
        }
    }
}