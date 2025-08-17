using GoldLoan.Application.DTOs;
using GoldLoan.Application.Interfaces;
using GoldLoan.Application.Services;
using GoldLoan.Domain.Entities;
using Moq;
using Xunit;
using System.Threading.Tasks;

namespace GoldLoan.Application.Tests.Services
{
    public class ClientServiceTests
    {
        private readonly Mock<IClientRepository> _mockClientRepository;
        private readonly IClientService _clientService;

        public ClientServiceTests()
        {
            _mockClientRepository = new Mock<IClientRepository>();
            _clientService = new ClientService(_mockClientRepository.Object);
        }

        [Fact]
        public async Task GetClientByIdAsync_ShouldReturnClient_WhenClientExists()
        {
            // Arrange
            var clientId = 1;
            var client = new Client { Id = clientId, Name = "Test Client", Address = "123 Test St", ContactNumber = "555-1234", IdProofDetails = "ID123" };
            _mockClientRepository.Setup(repo => repo.GetByIdAsync(clientId)).ReturnsAsync(client);

            // Act
            var result = await _clientService.GetClientByIdAsync(clientId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(clientId, result.Id);
            Assert.Equal("Test Client", result.Name);
        }
    }
}
