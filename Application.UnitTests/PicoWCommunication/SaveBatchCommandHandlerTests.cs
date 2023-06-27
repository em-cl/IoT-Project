using Application.PicoWCommunication.Commands;
using Domain.Entities;
using Domain.Repositories;
using FluentAssertions;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using Moq;
using System.Diagnostics.Metrics;

namespace Application.UnitTests.PicoWCommunication
{
	public class SaveBatchCommandHandlerTests

	{
		/*
		 *
		 *
		 */
		private readonly Mock<IUnitOfWork> _unitOfWorkMock;
		private readonly Mock<IPublisher> _publisherMock;

		public SaveBatchCommandHandlerTests()
		{
			_unitOfWorkMock = new Mock<IUnitOfWork>();
			_publisherMock = new Mock<IPublisher>();
		}

		[Fact]
		public async Task Handle_Should_ReturnEmptyEnumerableMeasurements_When_inputIsNullOrEmpty()
		{
			//Arrange
			//var saveBatch = new SaveBatchCommand(string.Empty);
			//
			//var handler = new SaveBatchCommand.Handler(
			//	_unitOfWorkMock.Object,
			//	_publisherMock.Object);
			//
			//
			//
			////Act
			//var result = await handler.Handle(saveBatch,default);
			//
			////Assert
			//result.IsNullOrEmpty().Should().BeTrue();
		}
	}
}