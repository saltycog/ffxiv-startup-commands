namespace FfxivStartupCommands
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;


    public class ThreadLoop
    {
        private Task task;
        private CancellationTokenSource cancellationTokenSource;


        public static ThreadLoop Start(Action<ThreadLoop> action, int interval)
        {
            ThreadLoop threadLoop = new ThreadLoop
                                        {
                                            cancellationTokenSource = new CancellationTokenSource()
                                        };

            threadLoop.task = Task.Factory.StartNew(action: () =>
                {
                    while (!threadLoop.cancellationTokenSource.IsCancellationRequested)
                    {
                        try
                        {
                            if (threadLoop.cancellationTokenSource.IsCancellationRequested)
                                break;
                            action(threadLoop);
                            threadLoop.cancellationTokenSource.Token.WaitHandle.WaitOne(interval);
                        }
                        catch
                        {
                            threadLoop.cancellationTokenSource.Token.WaitHandle.WaitOne(interval);
                        }
                    }
                }, threadLoop.cancellationTokenSource.Token);

            return threadLoop;
        }


        public void Stop()
        {
            this.cancellationTokenSource.Cancel();
        }
    }
}