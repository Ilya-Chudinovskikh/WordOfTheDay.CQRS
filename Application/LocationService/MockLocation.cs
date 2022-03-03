using Application.Interfaces;
using System;

namespace Application.LocationService
{
    public class MockLocation : IMockLocation
    {
        public double Longtitude { get { return MockCoordinate(180); } }
        public double Latitude { get { return MockCoordinate(90); } }
        private readonly static Random _random = new();
        private static double MockCoordinate(int degree)
        {
            var coordinate = RandomCoordinate(degree);

            return coordinate;
        }
        private static double RandomCoordinate(int degree)
        {
            var intCoordinate = _random.Next(-degree, degree);
            var doubleCoordinate = Math.Round(_random.NextDouble(), 5);

            var coordinate = intCoordinate + doubleCoordinate;

            return coordinate;
        }
    }
}
