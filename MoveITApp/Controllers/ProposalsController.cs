﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoveITApp.Services.Interfaces;
using MoveITApp.Shared.CustomExceptions;
using MovieITApp.Dtos.Proposals;
using System.Net;
using System.Security.Claims;

namespace MoveITApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProposalsController : ControllerBase
    {
        private readonly IProposalService _proposalService;

        public ProposalsController(IProposalService proposalService)
        {
            _proposalService = proposalService;
        }

        [Authorize]
        [HttpPost("initiateProposal")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> InitiateProposal(InitiateProposalDto initiateProposalDto)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    var username = claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value.ToString();

                    if(string.IsNullOrEmpty(username))
                        return Unauthorized();

                    var proposal = await _proposalService.GetProposal(initiateProposalDto, username);
                    return StatusCode(StatusCodes.Status201Created, proposal);

                }
                return Unauthorized();
            }
            catch (BadDataException e)
            {
                return BadRequest(e.Message);
            }
            catch (UserNotFoundException e)
            {
                return Unauthorized();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred!");
            }
        }
    }
}