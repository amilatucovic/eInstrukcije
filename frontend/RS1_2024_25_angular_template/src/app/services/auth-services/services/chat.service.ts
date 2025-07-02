import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { BehaviorSubject } from 'rxjs';
import {ConversationDto} from '../../../models/conversation-dto';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {MessageDto} from '../../../models/message-dto';
import {AvailableUsersDto} from '../../../models/available-users-dto';
import {MyAuthService} from '../my-auth.service';


@Injectable({
  providedIn: 'root'
})
export class ChatService {
  private hubConnection!: signalR.HubConnection;

  private messageReceivedSubject = new BehaviorSubject<{ sender: string, message: string } | null>(null);
  public messageReceived$ = this.messageReceivedSubject.asObservable();

  constructor(private http: HttpClient,  private authService: MyAuthService) {}

  public startConnection(token: string): void {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('http://localhost:7000/chathub', {
        accessTokenFactory: () => {
          const token = this.authService.getLoginToken()?.token;
          console.log('Token used for SignalR:', token);
          return token || '';
        }
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
    if (!this.hubConnection) {
      console.error('SignalR connection not initialized.');
      return;
    }
    this.hubConnection.invoke('SendMessage', receiverId, message)
      .catch(err => console.error('Error sending message:', err));
  }


  public stopConnection(): void {
    if (this.hubConnection) {
      this.hubConnection.stop().then(() => console.log('SignalR connection stopped.'));
    }
  }

  getConversations(userId: number): Observable<ConversationDto[]> {
    return this.http.get<ConversationDto[]>(`http://localhost:7000/api/Message/conversations/${userId}`);
  }

  getMessageHistory(userId1: number, userId2: number): Observable<MessageDto[]> {
    return this.http.get<MessageDto[]>(`http://localhost:7000/api/Message/history?userId1=${userId1}&userId2=${userId2}`);
  }

  getAvailableUsers(currentUserId: number): Observable<AvailableUsersDto[]> {
    return this.http.get<AvailableUsersDto[]>(`http://localhost:7000/api/Message/available-users/${currentUserId}`);
  }



}
