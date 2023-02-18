﻿using ChargingApp.Errors;
using ChargingApp.Extentions;
using ChargingApp.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChargingApp.Controllers;

//[Authorize]
public class RechargeCodeController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;

    public RechargeCodeController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [Authorize(Policy = "Required_VIP_Role")]
    [HttpPost]
    public async Task<ActionResult<double>> Recharge([FromBody] MyClass obj)
    {
        try
        {
            var email = User.GetEmail();
            var user = await _unitOfWork.UserRepository.GetUserByEmailAsync(email);

            if (user is null)
                return Unauthorized(new ApiResponse(404));

            var tmpCode = await _unitOfWork.RechargeCodeRepository.GetCodeAsync(obj.Code);
            if (tmpCode is null || tmpCode.Istaked)
                return BadRequest(new ApiResponse(401, "Invalid Code"));

            tmpCode.Istaked = true;
            tmpCode.User = user;
            user.Balance += tmpCode.Value;
            tmpCode.TakedTime = DateTime.Now;
            
            _unitOfWork.UserRepository.UpdateUserInfo(user);

            if (await _unitOfWork.Complete())
                return Ok(new ApiOkResponse("Recharged successfully. your balance is " + user.Balance));

            return BadRequest(new ApiResponse(400, "something went wrong"));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    [Authorize(Policy = "Required_Admins_Role")]
    [HttpGet("generate-codes")]
    public async Task<ActionResult<IEnumerable<string>>> GetCodes(int codeValue, int codeNumber)
    {
        try
        {
            var codes = await _unitOfWork.RechargeCodeRepository.GenerateCodesWithValue(codeNumber, codeValue);

            if (codes is null)
                return BadRequest(new ApiResponse(400, "something happened"));

            return Ok(new ApiOkResponse(codes));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public class MyClass
    {
        public string Code { get; set; }
    }
}