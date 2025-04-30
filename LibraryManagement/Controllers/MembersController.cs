using AutoMapper;
using LibraryManagement.Data;
using LibraryManagement.DTOs;
using LibraryManagement.Helpers;
using LibraryManagement.Interfaces;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MembersController(IMemberRepository memberRepository, IMapper mapper, ILogger<MembersController> logger) : ControllerBase
    {
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllMembers([FromQuery] MemberQuery query)
        {
            try
            {
                var members = await memberRepository.GetAllMembersAsync(query);
                return Ok(members);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong in the {nameof(GetAllMembers)}: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMemberById(int id)
        {
            try
            {
                var member = await memberRepository.GetMemberByIdAsync(id);
                if (member == null) return NotFound("Member Not Found");
                return Ok(member);
            }
            catch(Exception ex)
            {
                logger.LogError($"Something went wrong in the name of {nameof(GetMemberById)}: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
           
        }
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddMember([FromBody] AddMemberDto memberDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    logger.LogError($"Invalid Model State in the {nameof(AddMemberDto)}");
                    return BadRequest(ModelState);
                }
                var member = mapper.Map<Member>(memberDto);
                var newMember = await memberRepository.AddMemberAsync(member);
                return CreatedAtAction(nameof(GetMemberById), new { id = newMember.Id }, newMember);
                
            }
            catch(Exception ex)
            {
                logger.LogError($"Something went wrong in the {nameof(AddMember)}:{ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
           
        }
        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType (StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateMember(int id, [FromBody]UpdateMemberDto update)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    logger.LogError($"Invalid Model State in the {nameof(UpdateMemberDto)}.");
                    BadRequest(ModelState);
                }
                var member = mapper.Map<Member>(update);
                var result = await memberRepository.UpdateMemberAsync(id, member);
                if (result is null) return NotFound("Member Not Found");

                return Ok();
            }
            catch(Exception ex)
            {
                logger.LogError($"Something went wrong in the {nameof(UpdateMember)}:{ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteMember(int id)
        {
            try
            {
                var member = await memberRepository.DeleteMemberAsync(id);
                if (member is null) return NotFound("Member Not Found");
                return NoContent();
            }
            catch(Exception ex )
            {
                logger.LogError($"Something went wrong in the {nameof(DeleteMember)}:{ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
