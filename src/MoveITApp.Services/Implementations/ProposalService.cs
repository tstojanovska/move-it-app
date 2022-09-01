using Microsoft.Extensions.Options;
using MoveITApp.DataAccess.Interfaces;
using MoveITApp.Mappers;
using MoveITApp.Services.Interfaces;
using MoveITApp.Shared.AppSettings;
using MoveITApp.Shared.CustomExceptions;
using MovieITApp.Dtos.Proposals;

namespace MoveITApp.Services.Implementations
{
    /// <summary>
    /// Class that contains business logic for managing Proposals
    /// </summary>
    public class ProposalService : IProposalService
    {
        private readonly IDistanceRuleRepository _distanceRuleRepository;
        private readonly IMovingObjectRuleRepository _movingObjectRuleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProposalRepository _proposalRepository;
        private IOptions<AppSettings> _options;

        public ProposalService(IDistanceRuleRepository distanceRuleRepository, IMovingObjectRuleRepository movingObjectRuleRepository, 
            IOptions<AppSettings> options, IUserRepository userRepository, IProposalRepository proposalRepository)
        {
            _distanceRuleRepository = distanceRuleRepository;
            _movingObjectRuleRepository = movingObjectRuleRepository;
            _options = options;
            _userRepository = userRepository;
            _proposalRepository = proposalRepository;
        }

        /// <inheritdoc />
        public async Task<List<ProposalDto>> GetUserProposalsAsync(string username)
        {
            var userDb = await _userRepository.GetUserByUsernameAsync(username);
            if (userDb == null)
            {
                throw new UserNotFoundException();
            }

            var userProposalsDb = await _proposalRepository.GetUserProposalsAsync(userDb.Id);
            return userProposalsDb.Select(x => x.ToProposalDto()).ToList();
        }

        /// <inheritdoc />
        public async Task<ProposalDto> InitiateProposalAsync(InitiateProposalDto initiateProposalDto, string username)
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

        /// <summary>
        /// Calculates price for moving a given volume (attic and living)
        /// </summary>
        /// <param name="initiateProposalDto">Data needed for making a proposal</param>
        /// <param name="distancePrice">Price needed for one car to cover the given distance</param>
        private async Task<int> CalculateVolumePrice(InitiateProposalDto initiateProposalDto, int distancePrice)
        {
            var price = 0;

            if (initiateProposalDto.LivingAreaVolume > 0)
            {
                var numberOfCars = (initiateProposalDto.LivingAreaVolume / _options.Value.ExtraCarLimit) + 1;
                price += numberOfCars * distancePrice;
            }

            //add comment why
            if (initiateProposalDto.AtticAreaVolume > 0)
            {
                var numberOfCars = (initiateProposalDto.AtticAreaVolume / _options.Value.ExtraCarLimit) + 1;
                price += numberOfCars * distancePrice;
            }

            if (initiateProposalDto.MovingObjectType.HasValue)
            {
                var movingObjectRule = await _movingObjectRuleRepository.GetMovingObjectRuleByTypeAsync(initiateProposalDto.MovingObjectType.Value);
                price += movingObjectRule.FixedPrice;
            }
            return price;
        }

        /// <summary>
        /// Validates proposal data
        /// </summary>
        /// <param name="initiateProposalDto">Data needed for making a proposal</param>
        /// <exception cref="BadDataException"></exception>
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
