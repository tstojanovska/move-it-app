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
        private readonly IOptions<AppSettings> _optionsMock;
        private readonly IProposalService _proposalService;


        public ProposalServiceTests()
        {
            _proposalRepositoryMock = new Mock<IProposalRepository>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _distanceRuleRepositoryMock = new Mock<IDistanceRuleRepository>();
            _movingObjectRepositoryMock = new Mock<IMovingObjectRuleRepository>();
            _optionsMock = Options.Create<AppSettings>(new AppSettings
            {
                ExtraCarLimit = 50
            }); ;

            _proposalService = new ProposalService(_distanceRuleRepositoryMock.Object, _movingObjectRepositoryMock.Object,
                _optionsMock, _userRepositoryMock.Object, _proposalRepositoryMock.Object);

        }

        [Fact]
        public void Initiate_proposal_should_throw_exception()
        {
            SetupGetUserByUsernameToReturnNull();
            Assert.ThrowsAsync<InvalidUserException>(() => _proposalService.InitiateProposalAsync(new InitiateProposalDto
            {
                AtticAreaVolume =10,
                Distance = 10,
                LivingAreaVolume = 30,
                MovingObjectType = MovingObjectType.Piano
            }, "tstojanovska"));
            VerifyGetUserByUserNameWasCalledOnce();
        }

        [Theory]
        [InlineData(10, 20, 30, 7200, MovingObjectType.Piano)]
        [InlineData(10, 20, 30, 2200, null)]
        public async Task Initiate_proposal_should_return_result(int distance, int lArea, int aArea, int calculatedPrice, MovingObjectType? type)
        {
            SetupGetUserByUsernameToReturnUser();
            SetupGetDistanceRule(1000, 10);
            SetupGetObjectMovingRule(MovingObjectType.Piano,5000);

            var result = await _proposalService.InitiateProposalAsync(new InitiateProposalDto
            {
                AtticAreaVolume = aArea,
                Distance = distance,
                LivingAreaVolume = lArea,
                MovingObjectType = type
            }, "tstojanovska");

            Assert.NotNull(result);
            Assert.Equal(calculatedPrice, result.CalculatedPrice);

            VerifyGetUserByUserNameWasCalledOnce();
            VerifyGetDistanceRuleWasCalledOnce();
            if (type.HasValue)
            {
                VerifyGetMovingObjectRuleWasCalledTimes(Times.Once());
            }
            else
            {
                VerifyGetMovingObjectRuleWasCalledTimes(Times.Never());
            }
        }

        [Fact]
        public void GetUserProposals_should_throw_exception()
        {
            SetupGetUserByUsernameToReturnNull();
            Assert.ThrowsAsync<InvalidUserException>(() => _proposalService.GetUserProposalsAsync("tstojanovska"));
            VerifyGetUserByUserNameWasCalledOnce();
        }

        [Fact]
        public async Task GetUserProposals_should_return_result()
        {
            SetupGetUserProposals();
            SetupGetUserByUsernameToReturnUser();
            var result = await _proposalService.GetUserProposalsAsync("tstojanovska");

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.IsType<ProposalDto>(result.First());
            VerifyGetUserByUserNameWasCalledOnce();
        }

        [Fact]
        public void GetUserProposalDetails_should_throw_not_found_exception()
        {
            SetupGetUserByUsernameToReturnUser();
            SetupGetProposalByIdToReturnNull();
            Assert.ThrowsAsync<ResourceNotFoundException>(() => _proposalService.GetUserProposalDetailsAsync(1,"tstojanovska"));
            VerifyGetUserByUserNameWasCalledOnce();
            VerifyGetProposalByIdWasCalledOnce();
        }

        [Fact]
        public void GetUserProposalDetails_should_throw_invalid_user_exception()
        {
            SetupGetUserByUsernameToReturnUser();
            SetupGetProposalByIdToReturnProposal(2);
            Assert.ThrowsAsync<InvalidUserException>(() => _proposalService.GetUserProposalDetailsAsync(1, "tstojanovska"));
            VerifyGetUserByUserNameWasCalledOnce();
            VerifyGetProposalByIdWasCalledOnce();
        }


        private void SetupGetUserByUsernameToReturnNull() => _userRepositoryMock.Setup(x =>
        x.GetUserByUsernameAsync(It.IsAny<string>()))
        .ReturnsAsync(null as User);
        private void SetupGetUserByUsernameToReturnUser() => _userRepositoryMock.Setup(x =>
        x.GetUserByUsernameAsync(It.IsAny<string>()))
        .ReturnsAsync(new User
        {
            Id = 1,
            FirstName = "Tanja",
            LastName  = "Stojanovska",
            Username = "tstojanovska",
            Password = "Test123"
        });
        private void SetupGetProposalByIdToReturnNull() => _proposalRepositoryMock.Setup(x =>
        x.GetByIdAsync(It.IsAny<int>()))
        .ReturnsAsync(null as Proposal);
        private void SetupGetProposalByIdToReturnProposal(int id) => _proposalRepositoryMock.Setup(x =>
        x.GetByIdAsync(It.IsAny<int>()))
        .ReturnsAsync(new Proposal
        {
            Id = id,
            AtticAreaVolume = 10,
            Distance = 10,
            LivingAreaVolume = 30,
            MovingObjectType = MovingObjectType.Piano,
            CalculatedPrice = 7200

        });
        private void SetupGetDistanceRule(int fixedPrice, int pricePerKm) => _distanceRuleRepositoryMock.Setup(x =>
        x.GetDistanceRuleByRangeAsync(It.IsAny<int>()))
        .ReturnsAsync(new DistanceRule
        {
            FixedPrice = fixedPrice,
            PricePerKm = pricePerKm,
            From = 1,
            To = 100,
            Id =1
        });
        private void SetupGetUserProposals() => _proposalRepositoryMock.Setup(x =>
        x.GetUserProposalsAsync(It.IsAny<int>()))
        .ReturnsAsync(new List<Proposal>
        {
            new Proposal
            {
                Id = 1,
                AtticAreaVolume =10,
                Distance = 10,
                LivingAreaVolume = 30,
                MovingObjectType = MovingObjectType.Piano,
                CalculatedPrice = 7200
            },
            new Proposal
            {
                Id = 2,
                AtticAreaVolume =10,
                Distance = 10,
                LivingAreaVolume = 30,
                MovingObjectType = null,
                CalculatedPrice = 2200
            }
        });
        private void SetupGetObjectMovingRule(MovingObjectType movingObjectType, int fixedPrice) => _movingObjectRepositoryMock.Setup(x =>
        x.GetMovingObjectRuleByTypeAsync(It.IsAny<MovingObjectType>()))
        .ReturnsAsync(new MovingObjectRule
        {
            FixedPrice = fixedPrice,
            MovingObjectType = movingObjectType,
            Id = 1
        });
        private void VerifyGetUserByUserNameWasCalledOnce() => _userRepositoryMock.Verify(x =>
        x.GetUserByUsernameAsync(It.IsAny<string>()), Times.Once);
        private void VerifyGetDistanceRuleWasCalledOnce() => _distanceRuleRepositoryMock.Verify(x =>
        x.GetDistanceRuleByRangeAsync(It.IsAny<int>()), Times.Once);
        private void VerifyGetMovingObjectRuleWasCalledTimes(Times times) => _movingObjectRepositoryMock.Verify(x =>
        x.GetMovingObjectRuleByTypeAsync(It.IsAny<MovingObjectType>()), times);
        private void VerifyGetUserProposalsWasCalledOnce() => _proposalRepositoryMock.Verify(x =>
        x.GetUserProposalsAsync(It.IsAny<int>()), Times.Once);
        private void VerifyGetProposalByIdWasCalledOnce() => _proposalRepositoryMock.Verify(x =>
        x.GetByIdAsync(It.IsAny<int>()), Times.Once);

    }
}
