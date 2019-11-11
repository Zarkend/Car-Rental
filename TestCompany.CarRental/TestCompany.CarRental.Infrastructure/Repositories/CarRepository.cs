using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using TestCompany.CarRental.Domain.Entities;
using TestCompany.CarRental.Domain.Enums;
using TestCompany.CarRental.Domain.InfrastructureContracts;
using TestCompany.CarRental.Infrastructure.DbContexts;

namespace TestCompany.CarRental.Infrastructure.Repositories
{
    public class CarRepository : IRepository<Car>
    {
        private List<Car> _fleet = new List<Car>();
        private Dictionary<PriceType,int> _carPrices = new Dictionary<PriceType,int>();
        private CarRentalContext _context;

        public CarRepository(CarRentalContext context)
        {
            _context = context;
            _carPrices.Add(PriceType.Basic, 100);
            _carPrices.Add(PriceType.Premium, 150);

        }

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
            GC.SuppressFinalize(this);
        }

        public IQueryable<Car> Query(Expression<Func<Car, bool>> filter)
        {
            return _context.Car.Where(filter);
        }

        public Car GetById(int id)
        {
            return _context.Car.Find(id);
        }

        public IEnumerable<Car> GetAll()
        {
            return _context.Car.ToList();
        }

        public void Insert(Car car)
        {
            car.PriceType = car.Type == CarType.Convertible ? PriceType.Premium : PriceType.Basic;
            car.BonusPointsPerRental = car.Type == CarType.Convertible ? 2 : 1;
            car.PricePerDay = _carPrices[car.PriceType];
            car.CreatedDate = DateTime.Now;
            car.UpdatedDate = DateTime.Now;
            _context.Car.Add(car);
        }

        public void Update(Car car)
        {
            car.UpdatedDate = DateTime.Now;
            _context.Entry(car).State = EntityState.Modified;
        }

        public void Delete(int Id)
        {
            Car car = _context.Car.Find(Id);
            _context.Car.Remove(car);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void DeleteAll()
        {
            _context.Database.ExecuteSqlRaw("DELETE FROM Car");
        }

        public void FillTestData()
        {
            _fleet.Add(new Car() { Registration = "ESP-1234", Brand = Brand.Tesla, Model = "model 3", Type = CarType.Convertible });
            _fleet.Add(new Car() { Registration = "ASD-1234", Brand = Brand.Renault, Model = "Megane Sport", Type = CarType.Convertible });
            _fleet.Add(new Car() { Registration = "EFA-1234", Brand = Brand.Ford, Model = "Kuga", Type = CarType.MiniVan });
            _fleet.Add(new Car() { Registration = "GFE-1234", Brand = Brand.Renault, Model = "Scenic", Type = CarType.MiniVan });
            _fleet.Add(new Car() { Registration = "SDE-1234", Brand = Brand.Renault, Model = "Megane F", Type = CarType.Convertible });
            _fleet.Add(new Car() { Registration = "ASD-1234", Brand = Brand.Tesla, Model = "model S", Type = CarType.SUV });
            _fleet.Add(new Car() { Registration = "FAS-1234", Brand = Brand.Ferrari, Model = "Imprezza", Type = CarType.SUV });
            _fleet.Add(new Car() { Registration = "FEW-1234", Brand = Brand.Audi, Model = "A4 Sport", Type = CarType.Convertible });
            _fleet.Add(new Car() { Registration = "FES-1234", Brand = Brand.Audi, Model = "A4", Type = CarType.SUV });
            DeleteAll();
            _fleet.ForEach(Insert);
            _context.SaveChanges();
        }
    }
}
