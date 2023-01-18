﻿namespace ChargingApp.Entity;

public class DebitHistory : BaseEntity
{
    public double DebitValue { get; set; }
    public AppUser User { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
}