﻿using ChargingApp.DTOs;
using ChargingApp.Extentions;
using ChargingApp.Entity;
using ChargingApp.Errors;
using ChargingApp.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChargingApp.Controllers;
//[Authorize (Policy = "RequiredAdminRole")]

public class AdminController :BaseApiController
{
}