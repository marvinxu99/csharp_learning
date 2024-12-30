using CommercialRegistration;
using ConsumerVehicleRegistration;
using LiveryRegistration;

namespace TollCalculators;

public static class TollCalculator
{
    // const decimal CarFare = 2.00m, TaxiFare = 3.50m, BusFare = 5.00m, DeliveryTruckFare = 10.00m;
    const decimal CarFare = 2.00m;
    const decimal TaxiFare = 3.50m;
    const decimal BusFare = 5.00m;
    const decimal DeliveryTruckFare = 10.00m;

    /// <summary>
    /// simple pure function to return toll based on crude vehicle type
    /// </summary>
    /// <param name="vehicle">specify subject vehicle instance</param>
    /// <returns>decimal toll fare based on primitive vehicle type</returns>
    /// <remarks>so far c,t,b,t could be _ discard, but keep reading the docs!</remarks>
    /// <exception cref="ArgumentException"></exception>
    public static decimal CalculateToll(object vehicle) =>
        vehicle switch
        {
            //Car { Passengers: 0 } => CarFare + 0.50m,
            //Car { Passengers: 1 } => CarFare,
            //Car { Passengers: 2 } => CarFare - 0.50m,
            //Car => CarFare - 1.0m,

            //Taxi { Fares: 0 } => TaxiFare + 0.5.m,
            //Taxi { Fares: 0 } => TaxiFare,
            //Taxi { Fares: 0 } => TaxiFare - 0.5m,
            //Taxi => TaxiFare - 1.0m,
            Car c => c.Passengers switch
            {
                0 => CarFare + 0.5m,
                1 => CarFare,
                2 => CarFare - 0.5m,
                _ => CarFare - 1.0m
            },

            Taxi t => t.Fares switch
            {
                0 => TaxiFare + 1.00m,
                1 => TaxiFare,
                2 => TaxiFare - 0.50m,
                _ => TaxiFare - 1.00m
            },

            Bus b when ((double)b.Riders / (double)b.Capacity) < 0.50 => BusFare + 2.00m,
            Bus b when ((double)b.Riders / (double)b.Capacity) > 0.90 => BusFare - 1.00m,
            Bus => BusFare,

            DeliveryTruck t when (t.GrossWeightClass > 5000) => DeliveryTruckFare + 5.00m,
            DeliveryTruck t when (t.GrossWeightClass < 3000) => DeliveryTruckFare - 2.00m,
            DeliveryTruck => DeliveryTruckFare,

            { } => throw new ArgumentException(message: "Not a known vehicle type", paramName: nameof(vehicle)),
            null => throw new ArgumentNullException(nameof(vehicle))
        };

}