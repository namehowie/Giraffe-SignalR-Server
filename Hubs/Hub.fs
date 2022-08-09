namespace Hubs
open Microsoft.AspNetCore.SignalR

module Hub =

    type IClientApi = 
        abstract member Message :string -> System.Threading.Tasks.Task

    type PushHub () =
        inherit Hub<IClientApi> ()

        /// Pass along message from one client to all clients
        member this.Send (message: string) = 
            this.Clients.All.Message(message)

        override this.OnConnectedAsync () =
            base.OnConnectedAsync()
