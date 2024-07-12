using System.Diagnostics;
using System.Threading.Tasks;

public static class ProcessExtensions
{
    public static Task WaitForExitAsync(this Process process)
    {
        var tcs = new TaskCompletionSource<object>();
        process.EnableRaisingEvents = true;
        process.Exited += (sender, args) => tcs.SetResult(null);
        if (process.HasExited)
        {
            tcs.SetResult(null);
        }
        return tcs.Task;
    }
}
