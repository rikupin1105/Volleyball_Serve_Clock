using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volleyball_Serve_Clock
{
    class Timer
    {
        public static Timer Instance { get; } = new();
        public ReactiveProperty<string?> Display { get; set; } = new();
        public ReactiveProperty<string?> BackGround { get; set; } = new("#1546bc");
    }
}
