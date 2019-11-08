using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using TestCompany.CarRental.Domain.Entities;
using TestCompany.CarRental.Domain.Enums;
using TestCompany.CarRental.Domain.InfrastructureContracts;

namespace TestCompany.CarRental.Infrastructure.Repositories
{
    public class CarRepository : ICarRepository
    {
        private List<Car> _fleet = new List<Car>();
        private Dictionary<PriceType,int> _carPrices = new Dictionary<PriceType,int>();

        public CarRepository()
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

            _carPrices.Add(PriceType.Basic, 100);
            _carPrices.Add(PriceType.Premium, 150);

        }
        public Car GetCar(int Id)
        {
            throw new NotImplementedException();
        }

        public CarConfig GetCarConfig(Car car)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Car> GetCars()
        {
            return _fleet;
        }

        private static void InsertCar(Car car)
        {
            using (SqlConnection connection = new SqlConnection(@"Server=ANTONIO-PC\SQLEXPRESS;Database=CarRental;User Id=CarRental;Password=1234;"))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;

                transaction = connection.BeginTransaction();

                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    command.CommandText =
                        "Insert into Region (RegionID, RegionDescription) VALUES (100, 'Description')";
                    command.ExecuteNonQuery();
                    command.CommandText =
                        "Insert into Region (RegionID, RegionDescription) VALUES (101, 'Description')";
                    command.ExecuteNonQuery();

                    // Attempt to commit the transaction.
                    transaction.Commit();
                    Console.WriteLine("Both records are written to database.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                    Console.WriteLine("  Message: {0}", ex.Message);

                    // Attempt to roll back the transaction.
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception ex2)
                    {
                        // This catch block will handle any errors that may have occurred
                        // on the server that would cause the rollback to fail, such as
                        // a closed connection.
                        Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                        Console.WriteLine("  Message: {0}", ex2.Message);
                    }
                }
            }
        }

        public void UpdateCar(Car car)
        {
            using (SqlConnection connection = new SqlConnection(@"Server=ANTONIO-PC\SQLEXPRESS;Database=CarRental;User Id=CarRental;Password=1234;"))
            {
                using (SqlCommand command = new SqlCommand($"UPDATE Cars SET Rented = @{nameof(car.Rented)}, @{nameof(car.RentedDate)}, @{nameof(car.RentedCompany)} where Id = @{nameof(car.Id)} ", connection))
                {
                    command.Parameters.AddWithValue(nameof(car.Rented), car.Rented);
                    command.Parameters.AddWithValue(nameof(car.RentedDate), car.RentedDate);
                    command.Parameters.AddWithValue(nameof(car.RentedCompany), car.RentedCompany);
                    command.Parameters.AddWithValue(nameof(car.Id), car.Id);

                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public int GetCarPricePerDay(Car car)
        {
            CarConfig carConfig = GetCarConfig(car);

            return _carPrices[carConfig.PriceType];
        }
    }
}
