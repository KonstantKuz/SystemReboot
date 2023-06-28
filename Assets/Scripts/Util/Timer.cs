using System;
using UniRx;

namespace Util
{
    public class Timer : IDisposable
    {
        private IDisposable _disposable;
        private Timer(int seconds, Action callback, bool interval)
        {
            _disposable = interval ?
                Observable.Interval(TimeSpan.FromSeconds(seconds)).Subscribe(it => callback()) :
                Observable.Timer(TimeSpan.FromSeconds(seconds)).Subscribe(it => callback());
        }

        public static Timer ExpiringTimer(int seconds, Action callback)
        {
            return new Timer(seconds, callback, false);
        }

        public static Timer IntervalTimer(int seconds, Action callback)
        {
            return new Timer(seconds, callback, true);
        }
        
        public void Dispose()
        {
            _disposable?.Dispose();
            _disposable = null;
        }
    }
}