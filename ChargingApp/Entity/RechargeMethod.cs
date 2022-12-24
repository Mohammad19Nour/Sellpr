﻿namespace ChargingApp.Entity;

public class RechargeMethod :BaseEntity
{
    public string ArabicName { get; set; }
    public string EnglishName { get; set; }
    public ICollection<ChangerAndCompany>? ChangerAndCompanies { get; set; } = new List<ChangerAndCompany>();
}