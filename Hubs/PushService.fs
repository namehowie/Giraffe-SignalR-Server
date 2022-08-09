namespace Hubs
open Microsoft.AspNetCore.SignalR
open Microsoft.Extensions.Hosting
open System.Threading.Tasks
open Hubs
open Hubs.Hub
open System.Threading

module PushService =

    type PushService (hubContext :IHubContext<PushHub, IClientApi>) =
        inherit BackgroundService ()
      
        member this.HubContext :IHubContext<PushHub, IClientApi> = hubContext
    
        override this.ExecuteAsync (stoppingToken :CancellationToken) =
            let TurnFrequency = 1000.0
            let pingTimer = new System.Timers.Timer(TurnFrequency)
            // 定时任务
            let mutable count = 0
            pingTimer.Elapsed.Add(fun _ -> 
                count <- count+1
                this.HubContext.Clients.All.Message(count.ToString()) |> ignore)
    
            pingTimer.Start()
            Task.CompletedTask

