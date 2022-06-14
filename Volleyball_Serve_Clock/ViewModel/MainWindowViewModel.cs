using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using Volleyball_Serve_Clock.View;

namespace Volleyball_Serve_Clock.ViewModel
{
    internal class MainWindowViewModel
    {
        private int _seconds { get; set; }
        public int Seconds
        {   
            get { return _seconds; }
            set
            {
                _seconds = value;
                if(value == 0)
                {
                    Timer.Instance.Display.Value = "";
                }
                else if(value < 60)
                {
                    Timer.Instance.Display.Value = value.ToString();
                }
                else if(value < 3600)
                {
                    Timer.Instance.Display.Value = value / 60 +":" + (value % 60).ToString("00");
                }
            }
        }
        public ReactiveProperty<string?> Display { get; set; }
        public ReactiveProperty<string?> BackGround { get; set; }
        public MainWindowViewModel()
        {
            new TimerWindow().Show();

            Display = Timer.Instance.Display.ObserveProperty(x => x.Value).ToReactiveProperty();
            BackGround = Timer.Instance.BackGround.ObserveProperty(x => x.Value).ToReactiveProperty();


            TimeOutCommand.Subscribe(_ => TimeOut());
            BetweenSetsCommand.Subscribe(_ => BetweenSets());
            ServeClock15Command.Subscribe(_ => ServeClock15());
            ServeClock8Command.Subscribe(_ => ServeClock8());
            CancelCommand.Subscribe(_ => Cancel());
        }
        public ReactiveCommand TimeOutCommand { get; } = new();
        public ReactiveCommand BetweenSetsCommand { get; } = new();
        public ReactiveCommand ServeClock15Command { get; } = new();
        public ReactiveCommand ServeClock8Command { get; } = new();
        public ReactiveCommand CancelCommand { get; } = new();

        private IDisposable? observableTimer;
        private void TimeOut()
        {
            Cancel();

            Seconds = 30;
            observableTimer =
                Observable.Timer(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1))
                .TakeWhile(x => x < 30)
                .Subscribe(x => Seconds--);
        }
        private void BetweenSets()
        {
            Cancel();

            Seconds = 180;
            observableTimer =
                Observable.Timer(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1))
                .TakeWhile(x => x < 180)
                .Subscribe(x => Seconds--);
        }
        private void ServeClock15()
        {
            Cancel();
            Seconds = 15;
            Timer.Instance.BackGround.Value = "#1546bc";
            observableTimer =
                Observable.Timer(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1))
                .TakeWhile(x => x < 15)
                .Subscribe(x => ServeClock());
        }
        private void ServeClock8()
        {
            Cancel();
            Seconds = 8;
            Timer.Instance.BackGround.Value = "#c34335";
            observableTimer =
                Observable.Timer(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1))
                .TakeWhile(x => x < 8)
                .Subscribe(x => ServeClock());
        }
        private void ServeClock()
        {
            Seconds--;
            if (Seconds <=8)
            {
                Timer.Instance.BackGround.Value = "#c34335";
            }
            if(Seconds == 0)
            {
                Timer.Instance.BackGround.Value = "#1546bc";
            }
        }
        private void Cancel()
        {
            if (observableTimer is not null)
            {
                observableTimer.Dispose();
                Seconds = 0;
                Timer.Instance.BackGround.Value = "#1546bc";
            }
        }
    }
}
