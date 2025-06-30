import { Component, OnInit } from '@angular/core';
import { MyAuthService } from '../../../services/auth-services/my-auth.service';
import { ChatService } from '../../../services/auth-services/services/chat.service';
import { ConversationDto } from '../../../models/conversation-dto';
import { MessageDto } from '../../../models/message-dto';

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

  constructor(
    private chatService: ChatService,
    private authService: MyAuthService
  ) {}

  ngOnInit(): void {
    const userId = this.authService.getUserId();
    console.log('Current user ID:', userId);
    if (!userId) return;
    this.currentUserId = userId;



    this.chatService.getConversations(userId).subscribe({
      next: (data) => {
        this.conversations = data;
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
        this.messages = data;
      },
      error: (err) => console.error('Error fetching message history', err)
    });
  }

  sendMessage() {
    if (!this.newMessage.trim() || !this.selectedConversation) return;

    const currentTime = new Date().toISOString();

    // Add locally (optional for instant feedback)
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

    // Send over SignalR
    this.chatService.sendMessage(this.selectedConversation.userId.toString(), this.newMessage);

    this.newMessage = '';
  }
}
