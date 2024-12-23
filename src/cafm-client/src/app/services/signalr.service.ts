import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class SignalRService {
  private hubConnection!: signalR.HubConnection;
  public workOrderUpdates$ = new BehaviorSubject<any>(null);

  startConnection(): void {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:7124/workOrderHub') // Replace with your backend URL
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('SignalR connection started'))
      .catch((err) => console.error('Error while starting SignalR connection:', err));

    this.hubConnection.on('WorkOrderUpdated', (update) => {
      this.workOrderUpdates$.next(update);
    });
  }

  stopConnection(): void {
    this.hubConnection
      .stop()
      .then(() => console.log('SignalR connection stopped'))
      .catch((err) => console.error('Error while stopping SignalR connection:', err));
  }
}
