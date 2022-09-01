using Microsoft.Extensions.Options;
using MoveITApp.DataAccess.Interfaces;
using MoveITApp.Domain.Models;
using MoveITApp.Mappers;
using MoveITApp.Services.Interfaces;
using MoveITApp.Shared.AppSettings;
using MoveITApp.Shared.CustomExceptions;
using MovieITApp.Dtos.Proposals;

namespace MoveITApp.Services.Implementations
{
    public class ProposalService : IProposalService
    {
        private readonly IDistanceRuleRepository _distanceRuleRepository;
        private readonly IMovingObjectRuleRepository _movingObjectRuleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRepository<Proposal> _proposalRepository;
        private IOptions<AppSettings> _options;

        public ProposalService(IDistanceRuleRepository distanceRuleRepository, IMovingObjectRuleRepository movingObjectRuleRepository, 
            IOptions<AppSettings> options, IUserRepository userRepository, IRepository<Proposal> proposalRepository)
        {
            _distanceRuleRepository = distanceRuleRepository;
            _movingObjectRuleRepository = movingObjectRuleRepository;
            _options = options;
            _userRepository = userRepository;
            _proposalRepository = proposalRepository;
        }

        public async Task<ProposalDto> GetProposal(InitiateProposalDto initiateProposalDto, string username)
        {
            ValidateProposalInformation(initiateProposalDto);
            var userDb = await _userRepository.GetUserByUsernameAsync(username);
            if (userDb == null)
            {
                throw new UserNotFoundException();
            }

            var distanceRule = await _distanceRuleRepository.GetDistanceRuleByRangeAsync(initiateProposalDto.Distance);
            if(distanceRule == null)
            {
                throw new BadDataException($"No rule found for distance {initiateProposalDto.Distance}");
            }
            var distancePrice = distanceRule.FixedPrice + initiateProposalDto.Distance * distanceRule.PricePerKm;

            var price = await CalculateVolumePrice(initiateProposalDto, distancePrice);

            var proposalDto  = new ProposalDto
            {
                AtticAreaVolume = initiateProposalDto.AtticAreaVolume,
                CalculatedPrice = price,
                MovingObjectType = initiateProposalDto.MovingObjectType,
                LivingAreaVolume = initiateProposalDto.LivingAreaVolume,
                Distance = initiateProposalDto.Distance,
                UserId = userDb.Id
            };
            await _proposalRepository.AddAsync(proposalDto.ToProposal());

            return proposalDto;
        }

        private async Task<int> CalculateVolumePrice(InitiateProposalDto initiateProposalDto, int distancePrice)
        {
            var price = 0;

            if (initiateProposalDto.LivingAreaVolume > 0)
            {
                var numberOfCars = initiateProposalDto.LivingAreaVolume % _options.Value.ExtraCarLimit + 1;
                price += numberOfCars * distancePrice;
            }

            //add comment why
            if (initiateProposalDto.AtticAreaVolume > 0)
            {
                var numberOfCars = initiateProposalDto.AtticAreaVolume % _options.Value.ExtraCarLimit + 1;
                price += numberOfCars * distancePrice;
            }

            if (initiateProposalDto.MovingObjectType.HasValue)
            {
                var movingObjectRule = await _movingObjectRuleRepository.GetMovingObjectRuleByTypeAsync(initiateProposalDto.MovingObjectType.Value);
                price += movingObjectRule.FixedPrice;
            }
            return price;
        }

        private void ValidateProposalInformation(InitiateProposalDto initiateProposalDto)
        {
            if (initiateProposalDto.Distance <= 0)
            {
                throw new BadDataException("Distance must be greater than zero");
            }
            if (initiateProposalDto.LivingAreaVolume == 0 && initiateProposalDto.AtticAreaVolume == 0)
            {
                throw new BadDataException("Volume can not be zero");
            }
        }
    }
}
