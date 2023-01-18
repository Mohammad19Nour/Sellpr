﻿using AutoMapper;
using ChargingApp.Entity;
using ChargingApp.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ChargingApp.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;


    public UnitOfWork(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public ICategoryRepository CategoryRepository => new CategoryRepository(_context, _mapper);
    public IUserRepository UserRepository => new UserRepository(_context, mapper: _mapper);

    public IOrdersRepository OrdersRepository => new OrdersRepository(_context, _mapper);
    public IPaymentRepository PaymentRepository => new PaymentRepository(_context, _mapper);
    public IPaymentGatewayRepository PaymentGatewayRepository => new PaymentGatewayRepository(_context);
    public IProductRepository ProductRepository => new ProductRepository(_context);
    public IRechargeCodeRepository RechargeCodeRepository => new RechargeCodeRepository(_context);
    public IRechargeMethodeRepository RechargeMethodeRepository => new RechargeMethodRepository(_context,mapper:_mapper);
    public IVipLevelRepository VipLevelRepository => new VipLevelRepository(_context);
    public ICurrencyRepository CurrencyRepository => new CurrencyRepository(_context , _mapper);
    public IOurAgentsRepository OurAgentsRepository => new OurAgentsRepository(_context, _mapper);
    public ISpecificPriceForUserRepository SpecificPriceForUserRepository =>
        new SpecificPriceForUserRepository(_context);
    public IBenefitPercentInSpecificVipLevelRepository BenefitPercentInSpecificVipLevelRepository =>
        new BenefitPercentInSpecificVipLevelRepository(_context);
    public IFavoriteRepository FavoriteRepository => new FavoriteRepository(_context, _mapper);
    public ISliderRepository SliderRepository => new SliderRepository(_context, _mapper);
    public IPhotoRepository PhotoRepository => new PhotoRepository(_context);
    public IDebitRepository DebitRepository => new DebitRepository(_context,_mapper);

    public async Task<bool> Complete()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public bool HasChanges()
    {
        return _context.ChangeTracker.HasChanges(); 
    }

    public void UpdateEntity(BaseEntity entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
    }
}