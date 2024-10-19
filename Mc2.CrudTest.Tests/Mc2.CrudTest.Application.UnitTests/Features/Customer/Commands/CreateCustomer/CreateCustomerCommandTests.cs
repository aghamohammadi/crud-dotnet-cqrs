using NUnit.Framework;
using Moq;
using Mc2.CrudTest.Application.Common.Interfaces;
using Mc2.CrudTest.Application.Features.Customer.Commands.CreateCustomer;
using Mc2.CrudTest.Domain.Entities; 
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.TestHelper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using IbanNet;

namespace Mc2.CrudTest.Application.UnitTests.Features.Customer.Commands.CreateCustomer
{
    [TestFixture]
    public class CreateCustomerCommandHandlerTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private CreateCustomerCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _handler = new CreateCustomerCommandHandler(_unitOfWorkMock.Object);
        }

        [Test]
        public async Task Handle_ValidRequest_ShouldCreateCustomer()
        {
            // Arrange
            var command = new CreateCustomerCommand
            {
                FirstName = "Ahmad",
                LastName = "Aghamohammadi",
                Email = "ahmad.aghamohammadi@gmail.com",
                PhoneNumber = "+12124567890",
                DateOfBirth = new DateTime(1990, 1, 1),
                BankAccountNumber = "GB29NWBK60161331926819" 
            };

            _unitOfWorkMock.Setup(uow => uow.Customers.Add(It.IsAny<Domain.Entities.Customer>()));


            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.EqualTo(Guid.Empty)); 
            _unitOfWorkMock.Verify(uow => uow.Customers.Add(It.IsAny<Domain.Entities.Customer>()), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public void Handle_InvalidRequest_ShouldThrowException()
        {
            // Arrange
            var command = new CreateCustomerCommand
            {
                FirstName = "", 
                LastName = "Aghamohammadi",
                Email = "ahmad.aghamohammadi@gmail.com",
                PhoneNumber = "+12124567890",
                DateOfBirth = new DateTime(1990, 1, 1),
                BankAccountNumber = "GB29NWBK60161331926819"
            };

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () =>
                await _handler.Handle(command, CancellationToken.None));
        }
    }
}


