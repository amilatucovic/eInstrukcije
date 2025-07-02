import { Component, OnInit } from '@angular/core';
import { MyAuthService } from '../../../services/auth-services/my-auth.service';
import { ChatService } from '../../../services/auth-services/services/chat.service';
import { ConversationDto } from '../../../models/conversation-dto';
import { MessageDto } from '../../../models/message-dto';
import {AvailableUsersDto} from '../../../models/available-users-dto';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit {
  conversations: ConversationDto[] = [];
  selectedConversation: ConversationDto | null = null;
  messages: MessageDto[] = [];
  newMessage: string = '';
  currentUserId!: number;
  availableUsers: AvailableUsersDto[] = [];
  showAvailableUsers: boolean = false;

  constructor(
    private chatService: ChatService,
    private authService: MyAuthService
  ) {}

  ngOnInit(): void {
    const userId = this.authService.getUserId();
    console.log('Current user ID:', userId);
    if (!userId) return;
    this.currentUserId = userId;

    const token = this.authService.getLoginToken()?.token;
    if (token) {
      this.chatService.startConnection(token);
    } else {
      console.error("Token not found!");
    }

    this.chatService.messageReceived$.subscribe(data => {
      if (!data) return;

      const { sender, message } = data;

      if (this.selectedConversation?.userId.toString() === sender) {
        this.messages.push({
          id: 0,
          senderId: +sender,
          receiverId: this.currentUserId,
          content: message,
          sentAt: new Date(),
          isRead: false,
          senderUsername: '',
          senderFullName: ''
        });
      }
    });






    this.chatService.getConversations(userId).subscribe({
      next: (data) => {
        this.conversations = data.map(c => ({
          ...c,
          lastMessageTime: new Date(c.lastMessageTime)
        }));

        if (this.conversations.length > 0) {
          this.selectConversation(this.conversations[0]);
        }
      },
      error: (err) => console.error('Error fetching conversations', err)
    });

  }

  selectConversation(convo: ConversationDto): void {
    this.selectedConversation = convo;
    this.messages = []; // clear previous messages

    this.chatService.getMessageHistory(this.currentUserId, convo.userId).subscribe({
      next: (data) => {
        this.messages = data.map(m => ({
          ...m,
          sentAt: new Date(m.sentAt)
        }));
      },
      error: (err) => console.error('Error fetching message history', err)
    });
  }

  sendMessage() {
    if (!this.newMessage.trim() || !this.selectedConversation) return;
    const currentTime = new Date();
    this.messages.push({
      id: 0,
      senderId: this.currentUserId,
      receiverId: this.selectedConversation.userId,
      content: this.newMessage,
      sentAt: currentTime,
      isRead: false,
      senderUsername: this.authService.getLoggedInUser()?.username || 'Ja',
      senderFullName: `${this.authService.getLoggedInUser()?.firstName} ${this.authService.getLoggedInUser()?.lastName}`
    });


    this.chatService.sendMessage(this.selectedConversation.userId.toString(), this.newMessage);

    this.newMessage = '';
    this.chatService.getConversations(this.currentUserId).subscribe(data => {
      this.conversations = data;
      const newConvo = data.find(c => c.userId === this.selectedConversation?.userId);
      if (newConvo) {
        this.selectConversation(newConvo);
      }
    });
  }



  openUserModal(): void {
    this.showAvailableUsers = true;
    this.chatService.getAvailableUsers(this.currentUserId).subscribe({
      next: (users) => this.availableUsers = users,
      error: (err) => console.error('Greška pri dohvaćanju korisnika:', err)
    });
  }

  closeUserModal(): void {
    this.showAvailableUsers = false;
  }

  startConversationWith(user: AvailableUsersDto): void {
    this.selectedConversation = {
      userId: user.id,
      fullName: user.fullName,
      username: user.username,
      lastMessage: '',
      lastMessageTime: new Date()
    };

    this.messages = [];
    this.closeUserModal();
  }

}
