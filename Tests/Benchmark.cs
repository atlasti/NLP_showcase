using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Tests
{
    public class Benchmark
    {
        private readonly Dictionary<string, List<long>> _Measurements;
        private          int                            _Counter;
        private          Stopwatch                      _StopWatch;

        public Benchmark(IEnumerable<string> keys)
        {
            this._Measurements = keys.ToDictionary(key => key, key => new List<long>());
            this._Counter      = 0;
        }

        public void BeginCycle()
        {
            if (this._StopWatch != null && this._StopWatch.IsRunning)
            {
                this._StopWatch.Stop();
            }

            this._Counter++;
            this._StopWatch = Stopwatch.StartNew();
        }

        public void CheckPoint(string key)
        {
            if (!this._Measurements.ContainsKey(key))
            {
                throw new Exception($"Invalid key {key}");
            }

            this._Measurements[key].Add(this._StopWatch.ElapsedTicks);
            this._StopWatch.Restart();
        }

        public Dictionary<string, double> Analyze()
        {
            double    frequency = Stopwatch.Frequency;
            const int sumIndex  = 0;
            for (var index = 1; index < this._Counter; index++)
            {
                foreach (var key in this._Measurements.Keys)
                {
                    var ticks = this._Measurements[key][index];
                    if (index == 1)
                    {
                        this._Measurements[key][sumIndex] = ticks;
                    }
                    else
                    {
                        this._Measurements[key][sumIndex] += ticks;
                    }
                }
            }

            var result = new Dictionary<string, double>();
            foreach (var key in this._Measurements.Keys)
            {
                var ticks        = this._Measurements[key][sumIndex] / (this._Counter - 1);
                var milliseconds = ticks / frequency                 * 1000.0;
                result[key] = milliseconds;
            }

            return result;
        }
    }
}
