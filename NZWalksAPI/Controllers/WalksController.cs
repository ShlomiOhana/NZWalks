using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO.Walk;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WalksController : ControllerBase
    {
        private readonly IWalkRepository _walkRepository;
        private readonly IMapper _mapper;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            _mapper = mapper;
            _walkRepository = walkRepository;
        }

        // /api/walks?filterOn=Name&filterQuery=track&sortBy=Name&isAscending=true&pageNumber=1&pageSize=10
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var walksDomain = await _walkRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);
            return Ok(_mapper.Map<List<WalkDTO>>(walksDomain));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetWalkById(Guid id)
        {
            var walkDomain = await _walkRepository.GetWalkByIdAsync(id);
            if (walkDomain == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<WalkDTO>(walkDomain));
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDTO addWalkRequestDTO)
        {
            var walkDomainModel = _mapper.Map<Walk>(addWalkRequestDTO);
            await _walkRepository.CreateAsync(walkDomainModel);

            return Ok(_mapper.Map<WalkDTO>(walkDomainModel));
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateWalkRequestDTO updateWalkRequestDTO)
        {
            var walkDomainModel = _mapper.Map<Walk>(updateWalkRequestDTO);
            walkDomainModel = await _walkRepository.UpdateAsync(id, walkDomainModel);

            if (walkDomainModel == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<WalkDTO>(walkDomainModel));
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deletedWalk = await _walkRepository.DeleteAsync(id);
            if (deletedWalk == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<WalkDTO>(deletedWalk));
        }
    }
}
