using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reactive.Bindings.Extensions;
using System.Reactive.Concurrency;
using System.Threading;

namespace Volleyball_Serve_Clock.ViewModel
{
    internal class TimerViewModel
    {
        public ReactiveProperty<string?> Display { get; set; }
        public ReactiveProperty<string?> BackGround { get; set; }
        public TimerViewModel()
        {
            Display = Timer.Instance.Display.ObserveProperty(x=>x.Value).ToReactiveProperty();
            BackGround = Timer.Instance.BackGround.ObserveProperty(x=>x.Value).ToReactiveProperty();
        }
    }
}
