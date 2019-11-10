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

        public CarRepository()
        {
            _context = new CarRentalContext();

            GetCar(67);

            _fleet.Add(new Car() { Registration = "ESP-1234", Brand = Brand.Tesla, Model = "model 3", Type = CarType.Convertible });
            _fleet.Add(new Car() { Registration = "ASD-1234", Brand = Brand.Renault, Model = "Megane Sport", Type = CarType.Convertible });
            _fleet.Add(new Car() { Registration = "EFA-1234", Brand = Brand.Ford, Model = "Kuga", Type = CarType.MiniVan });
            _fleet.Add(new Car() { Registration = "GFE-1234", Brand = Brand.Renault, Model = "Scenic", Type = CarType.MiniVan });
            _fleet.Add(new Car() { Registration = "SDE-1234", Brand = Brand.Renault, Model = "Megane F", Type = CarType.Convertible });
            _fleet.Add(new Car() { Registration = "ASD-1234", Brand = Brand.Tesla, Model = "model S", Type = CarType.SUV });
            _fleet.Add(new Car() { Registration = "FAS-1234", Brand = Brand.Ferrari, Model = "Imprezza", Type = CarType.SUV });
            _fleet.Add(new Car() { Registration = "FEW-1234", Brand = Brand.Audi, Model = "A4 Sport", Type = CarType.Convertible });
            _fleet.Add(new Car() { Registration = "FES-1234", Brand = Brand.Audi, Model = "A4", Type = CarType.SUV });

            _carPrices.Add(PriceType.Basic, 100);
            _carPrices.Add(PriceType.Premium, 150);

            DeleteAll();
            _fleet.ForEach(InsertCar);

        }

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
            GC.SuppressFinalize(this);
        }

        public Car GetCar(int Id)
        {
            var car = GetCars().FirstOrDefault(x => x.Id == Id);
            return car;
        }

        public CarConfig GetCarConfig(Car car)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Car> Query(Expression<Func<Car, bool>> filter)
        {
            return _context.Car.Where(filter);
        }


        public static void DeleteAll()
        {
            using (SqlConnection connection = new SqlConnection(@"Server=ANTONIO-PC\SQLEXPRESS;Database=CarRental;User Id=CarRental;Password=1234;"))
            {
                using (SqlCommand command = new SqlCommand($"DELETE FROM Car", connection))
                {
                    try
                    {
                        command.Connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("  Message: {0}", ex.Message);
                    }
                }

            }
        }

        public static void InsertCar(Car car)
        {
            using (SqlConnection connection = new SqlConnection(@"Server=ANTONIO-PC\SQLEXPRESS;Database=CarRental;User Id=CarRental;Password=1234;"))
            {
                using (SqlCommand command = new SqlCommand($"INSERT INTO  Car VALUES(@{nameof(car.Model)},@{nameof(car.Registration)},@{nameof(car.Brand)},@{nameof(car.Type)},@{nameof(car.Rented)},@{nameof(car.RentedDate)},@{nameof(car.CompanyId)},@{nameof(car.CreatedDate)})", connection))
                {
                    command.Parameters.Add(nameof(car.Model), SqlDbType.NVarChar).Value = car.Model;
                    command.Parameters.Add(nameof(car.Registration), SqlDbType.NVarChar).Value = car.Registration;
                    command.Parameters.Add(nameof(car.Brand), SqlDbType.Int).Value = car.Brand;
                    command.Parameters.Add(nameof(car.Type), SqlDbType.Int).Value = car.Type;
                    command.Parameters.Add(nameof(car.Rented), SqlDbType.Int).Value = car.Rented;
                    command.Parameters.Add(nameof(car.RentedDate), SqlDbType.DateTime).Value = car.RentedDate;
                    command.Parameters.Add(nameof(car.CompanyId), SqlDbType.Int).Value = car.CompanyId;
                    car.CreatedDate = DateTime.Now;
                    command.Parameters.Add(nameof(car.CreatedDate), SqlDbType.DateTime).Value = car.CreatedDate;
                    
                    try
                    {
                        command.Connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("  Message: {0}", ex.Message);
                    }
                }
                
            }
        }

        public void UpdateCar(Car car)
        {
            using (SqlConnection connection = new SqlConnection(@"Server=ANTONIO-PC\SQLEXPRESS;Database=CarRental;User Id=CarRental;Password=1234;"))
            {
                using (SqlCommand command = new SqlCommand($"UPDATE Car SET {nameof(car.Rented)} = @{nameof(car.Rented)}, {nameof(car.RentedDate)} = @{nameof(car.RentedDate)}, {nameof(car.CompanyId)} = @{nameof(car.CompanyId)} where {nameof(car.Id)} = @{nameof(car.Id)} ", connection))
                {
                    command.Parameters.Add(nameof(car.Rented), SqlDbType.Int).Value = car.Rented;
                    command.Parameters.Add(nameof(car.RentedDate), SqlDbType.DateTime).Value = car.RentedDate;
                    command.Parameters.Add(nameof(car.CompanyId), SqlDbType.Int).Value = car.CompanyId;
                    command.Parameters.Add(nameof(car.Id), SqlDbType.Int).Value = car.Id;
                    try
                    {
                        command.Connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("  Message: {0}", ex.Message);
                    }
                }
            }
        }

        public int GetCarPricePerDay(Car car)
        {
            CarConfig carConfig = GetCarConfig(car);

            return _carPrices[carConfig.PriceType];
        }

        public Car GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Car[] GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
