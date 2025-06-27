import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ChatService {
  private hubConnection!: signalR.HubConnection;

  private messageReceivedSubject = new BehaviorSubject<{ sender: string, message: string } | null>(null);
  public messageReceived$ = this.messageReceivedSubject.asObservable();

  constructor() {}

  public startConnection(token: string): void {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('http://localhost:7000/chathub', {
        accessTokenFactory: () => token
      })
      .withAutomaticReconnect()
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('SignalR connection started'))
      .catch(err => console.error('SignalR error while starting:', err));

    this.hubConnection.on('ReceiveMessage', (sender: string, message: string) => {
      this.messageReceivedSubject.next({ sender, message });
    });
  }

  public sendMessage(receiverId: string, message: string): void {
    this.hubConnection.invoke('SendMessage', receiverId, message)
      .catch(err => console.error('Error sending message:', err));
  }

  public stopConnection(): void {
    if (this.hubConnection) {
      this.hubConnection.stop().then(() => console.log('SignalR connection stopped.'));
    }
  }
}
