﻿using ChargingApp.DTOs;
using ChargingApp.Errors;
using ChargingApp.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChargingApp.Controllers;

public class OurAgentsController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;

    public OurAgentsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet("our-agents")]
    public async Task<ActionResult<List<OurAgentsDto>>> GetOurAgents()
    {
        try
        {
            return Ok(new ApiOkResponse(await _unitOfWork.OurAgentsRepository.GetOurAgentsAsync()));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}