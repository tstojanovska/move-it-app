using Microsoft.Extensions.Options;
using Moq;
using MoveITApp.DataAccess.Interfaces;
using MoveITApp.Domain.Enums;
using MoveITApp.Domain.Models;
using MoveITApp.Services.Implementations;
using MoveITApp.Services.Interfaces;
using MoveITApp.Shared.AppSettings;
using MoveITApp.Shared.CustomExceptions;
using MovieITApp.Dtos.Proposals;
using Xunit;

namespace MoveITApp.Tests
{
    public class ProposalServiceTests
    {
        private readonly Mock<IProposalRepository> _proposalRepositoryMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IDistanceRuleRepository> _distanceRuleRepositoryMock;
        private readonly Mock<IMovingObjectRuleRepository> _movingObjectRepositoryMock;
        private readonly Mock<IOptions<AppSettings>> _optionsMock;
        private readonly IProposalService _proposalService;


        public ProposalServiceTests()
        {
            _proposalRepositoryMock = new Mock<IProposalRepository>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _distanceRuleRepositoryMock = new Mock<IDistanceRuleRepository>();
            _movingObjectRepositoryMock = new Mock<IMovingObjectRuleRepository>();
            _optionsMock = new Mock<IOptions<AppSettings>>();

            _proposalService = new ProposalService(_distanceRuleRepositoryMock.Object, _movingObjectRepositoryMock.Object,
                _optionsMock.Object, _userRepositoryMock.Object, _proposalRepositoryMock.Object);

        }

        [Fact]
        public void Confirm_subsidie_should_throw_exception()
        {
            SetupGetUserByUsernameToReturnNull();
            Assert.ThrowsAsync<UserNotFoundException>(() => _proposalService.InitiateProposal(new InitiateProposalDto
            {
                AtticAreaVolume =10,
                Distance = 10,
                LivingAreaVolume = 30,
                MovingObjectType = MovingObjectType.Piano
            }, "tstojanovska"));
            VerifyGetUserByUserNameWasCalledOnce();
        }


        private void SetupGetUserByUsernameToReturnNull() => _userRepositoryMock.Setup(x =>
        x.GetUserByUsernameAsync(It.IsAny<string>()))
        .ReturnsAsync(null as User);
        private void VerifyGetUserByUserNameWasCalledOnce() => _userRepositoryMock.Verify(x =>
        x.GetUserByUsernameAsync(It.IsAny<string>()), Times.Once);
      
    }
}
