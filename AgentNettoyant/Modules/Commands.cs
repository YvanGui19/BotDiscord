using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AgentNettoyant.Modules
{
    public class Commands : ModuleBase<SocketCommandContext>
    {
        [Command("ping")]
        public async Task PingAsync()
        {
            await ReplyAsync("pong!");
        }

        [Command("serveurON")]
        public async Task RunServerOnScriptAsync()
        {
            await ReplyAsync("Démarrage du serveur...");
            string scriptPath = @"D:\19\Documents\Enshrouded_Server\CheckUpdateAndRunServer.bat";
            _ = Task.Run(() => ExecuteScriptAsync(scriptPath));
        }

        [Command("serveurOFF")]
        public async Task RunServerOffScriptAsync()
        {
            await ReplyAsync("Fermeture du serveur en cours...");
            string scriptPath = @"D:\19\Documents\Enshrouded_Server\StopServer.bat";
            _ = Task.Run(() => ExecuteScriptAsync(scriptPath));
        }

        [Command("pcOFF")]
        public async Task ShutdownAsync()
        {
            await ReplyAsync("Démarrage du processus d'arrêt...");
            _ = Task.Run(RunShutdownTask);
            await ReplyAsync("Le PC va s'éteindre sous peu.");
        }

        private void ExecuteScriptAsync(string scriptPath)
        {
            string workingDirectory = Path.GetDirectoryName(scriptPath);

            using (var process = new Process())
            {
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.Arguments = $"/C \"{scriptPath}\"";
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.WorkingDirectory = workingDirectory;
                process.StartInfo.StandardOutputEncoding = Encoding.UTF8;

                process.Start();
                process.WaitForExit();
            }
        }

        private void RunShutdownTask()
        {
            using (var process = new Process())
            {
                process.StartInfo.FileName = "schtasks";
                process.StartInfo.Arguments = "/run /tn \"Shutdown Script\"";
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.StandardOutputEncoding = Encoding.UTF8;

                process.Start();
                process.WaitForExit();
            }
        }
    }
}