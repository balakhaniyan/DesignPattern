using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
namespace DesignPatterns
{
    public interface ISubject
    {
        public int RegisterObserver(IObserver observer);
        public bool RemoveObserver(IObserver observer);
        public void NotifyObservers();
    }

    public interface IObserver
    {
        public void Update(float temperature, float humidity, float pressure);
    }

    public interface IDisplay
    {
        public void Display();
    }

    public class WeatherData : ISubject
    {
        private readonly IList<IObserver> _observers;
        private float _temperature;
        private float _humidity;
        private float _pressure;

        public WeatherData()
        {
            _observers = new List<IObserver>();
        }

        public int RegisterObserver(IObserver observer)
        {
            _observers.Add(observer);
            return _observers.IndexOf(observer);
        }

        public bool RemoveObserver(IObserver observer)
        {
            return _observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (var observer in _observers)
            {
                observer.Update(_temperature, _humidity, _pressure);
            }
        }

        private void MeasurementsChanged()
        {
            NotifyObservers();
        }

        public void SetMeasurements(float temperature, float humidity, float pressure)
        {
            _temperature = temperature;
            _humidity = humidity;
            _pressure = pressure;
            MeasurementsChanged();
        }
    }

    public class CurrentConditionsDisplay : IObserver, IDisplay
    {
        private float _temperature;
        private float _humidity;

        public CurrentConditionsDisplay(WeatherData weatherData)
        {
            weatherData.RegisterObserver(this);
        }

        public void Update(float temperature, float humidity, float pressure)
        {
            _temperature = temperature;
            _humidity = humidity;
            Display();
        }

        public void Display()
        {
            Console.WriteLine($"Current conditions: {_temperature}F degrees and {_humidity}% humidity");
        }
    }

    public class StatisticsDisplay : IObserver, IDisplay
    {
        private float _maxTemperature;
        private float _minTemperature;
        private float _avgTemperature;
        private int _numOfChanges;

        public StatisticsDisplay(WeatherData weatherData)
        {
            weatherData.RegisterObserver(this);
            _maxTemperature = 0;
            _minTemperature = float.MaxValue;
            _avgTemperature = 0;
            _numOfChanges = 0;
        }

        public void Update(float temperature, float humidity, float pressure)
        {
            if (temperature > _maxTemperature)
                _maxTemperature = temperature;
            if (temperature < _minTemperature)
                _minTemperature = temperature;
            _numOfChanges += 1;
            _avgTemperature = (_avgTemperature * (_numOfChanges - 1) + temperature) / _numOfChanges;
            Display();
        }

        public void Display()
        {
            Console.WriteLine($"Avg/Max/Min temperature = {_avgTemperature}/{_maxTemperature}/{_minTemperature}");
        }
    }

    public class HeatIndexDisplay : IObserver, IDisplay
    {
        private float _temperature;
        private float _humidity;
        private double _heatIndex;

        public HeatIndexDisplay(WeatherData weatherData)
        {
            weatherData.RegisterObserver(this);
        }

        private double HeatIndex()
        {
            return 16.923 + 1.85212 * Math.Pow(10, -1) * _temperature + 5.37941 * _humidity - 1.00254 *
                Math.Pow(10, -1) *
                _temperature * _humidity + 9.41695 * Math.Pow(10, -3) * Math.Pow(_temperature, 2) +
                7.28898 * Math.Pow(10, -3) * Math.Pow(_humidity, 2) + 3.45372 *
                Math.Pow(10, -4) * Math.Pow(_temperature, 2) * _humidity - 8.14971 * Math.Pow(10, -4) * _temperature *
                Math.Pow(_humidity, 2) + 1.02102 * Math.Pow(10, -5) * Math.Pow(_temperature, 2) *
                Math.Pow(_humidity, 2) - 3.8646 * Math.Pow(10, -5) * Math.Pow(_temperature, 3) + 2.91583 *
                Math.Pow(10, -5) *
                Math.Pow(_humidity, 3) + 1.42721 * Math.Pow(10, -6)
                                                 * Math.Pow(_temperature, 3) * _humidity +
                1.97483 * Math.Pow(10, -7) * _temperature * Math.Pow(_humidity, 3) - 2.18429 *
                Math.Pow(10, -8) * Math.Pow(_temperature, 3) * Math.Pow(_humidity, 2)
                + 8.43296 * Math.Pow(10, -10) * Math.Pow(_temperature, 2) * Math.Pow(_humidity, 3) - 4.81975 *
                Math.Pow(10, -11) * Math.Pow(_temperature, 3) *
                Math.Pow(_humidity, 3);
        }

        public void Update(float temperature, float humidity, float pressure)
        {
            _temperature = temperature;
            _humidity = humidity;
            _heatIndex = HeatIndex();
            Display();
        }

        public void Display()
        {
            Console.WriteLine($"HeatIndex: {_heatIndex}");
        }
    }

    public static class Observer
    {
        public static bool Run()
        {
            WeatherData weatherData = new();
            var __ = new CurrentConditionsDisplay(weatherData);
            var ___ = new StatisticsDisplay(weatherData);
            var ____ = new HeatIndexDisplay(weatherData);
            foreach (var _ in Enumerable.Range(1,10))
            {
                weatherData.SetMeasurements(RandomNumberGenerator.GetInt32(1, 100),
                    RandomNumberGenerator.GetInt32(1, 100),
                    RandomNumberGenerator.GetInt32(1, 100));
            }

            return true;
        }
    }
}