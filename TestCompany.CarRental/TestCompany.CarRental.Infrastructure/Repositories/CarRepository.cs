using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TestCompany.CarRental.Domain.Entities;
using TestCompany.CarRental.Domain.Enums;
using TestCompany.CarRental.Domain.InfrastructureContracts;
using TestCompany.CarRental.Infrastructure.DbContexts;

namespace TestCompany.CarRental.Infrastructure.Repositories
{
    public class CarRepository : BaseRepository<Car>
    {
        private List<Car> _fleet = new List<Car>();
        private Dictionary<PriceType,int> _carPrices = new Dictionary<PriceType,int>();
        private CarRentalContext _context;

        public CarRepository(CarRentalContext context) : base(context)
        {
            _context = context;
            _carPrices.Add(PriceType.Basic, 100);
            _carPrices.Add(PriceType.Premium, 150);

        }
      

        
        public async override Task InsertAsync(Car car)
        {
            car.PriceType = car.Type == CarType.Convertible ? PriceType.Premium : PriceType.Basic;
            car.BonusPointsPerRental = car.Type == CarType.Convertible ? 2 : 1;
            car.PricePerDay = _carPrices[car.PriceType];
            car.CreatedDate = DateTime.Now;
            car.UpdatedDate = DateTime.Now;
            await base.InsertAsync(car);
        }

        public override void Update(Car car)
        {
            car.UpdatedDate = DateTime.Now;
            base.Update(car);
        }

        public void DeleteAll()
        {
            _context.Database.ExecuteSqlRaw("DELETE FROM Car");
        }

        public async override Task FillTestDataAsync()
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
            _fleet.ForEach(async x => await InsertAsync(x));
            _context.SaveChanges();
        }
    }
}
