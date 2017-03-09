using FoodPoint_Seller.Api.Models.ViewModels;
using MvvmCross.FieldBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPoint_Seller.Core.Models
{
    public class Timer
    {
        public Guid ID { get; set; }
        //public TimeSpan WaitTime { get; set; }

        public INC<TimeSpan> WaitTime = new NC<TimeSpan>();

        public Action<long> Func { get; set; }
        private IDisposable _timer { get; set; }

        public Timer( TimeSpan waitTimer, Action<long> func)
        {
            this.WaitTime.Value = waitTimer;
            this.ID = Guid.NewGuid();
            this.Func = func;
        }

        public void StartTimer()
        {
            this._timer = Observable.Timer(new TimeSpan(0, 0, 0, 1, 0), new TimeSpan(0, 0, 0, 1, 0)).Subscribe(this.Func);
        }
        public void StopTimer()
        {
            this._timer.Dispose();
        }
    }

}
