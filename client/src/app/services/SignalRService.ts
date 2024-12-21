import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';

@Injectable({
  providedIn: 'root',
})
export class SignalRService {
  private hubConnection: signalR.HubConnection;

  constructor() {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:7061/workorderhub', {
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets,
      })
      .build();
  }

  startConnection() {
    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch((err) => console.log('Error while starting connection: ' + err));
  }

  addWorkOrderStatusListener(callback: (message: string) => void) {
    this.hubConnection.on('ReceiveWorkOrderUpdate', callback);
  }

  stopConnection() {
    this.hubConnection.stop().then(() => console.log('Connection stopped'));
  }
}
