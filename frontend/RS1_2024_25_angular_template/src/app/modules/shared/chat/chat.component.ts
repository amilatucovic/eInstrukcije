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
  typingUserId: number | null = null;
  typingTimeoutHandle: any;
  searchTerm: string = '';
  groupedMessages: { date: string, messages: MessageDto[] }[] = [];



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
        const newMsg: MessageDto = {
          id: 0,
          senderId: +sender,
          receiverId: this.currentUserId,
          content: message,
          sentAt: new Date(),
          isRead: false,
          senderUsername: '',
          senderFullName: ''
        };
        this.messages.push(newMsg);
        this.groupedMessages = this.groupMessagesByDate(this.messages);
      }

    });

    this.chatService.hubConnection.on("UserTyping", (senderId: string) => {
      const numericSenderId = +senderId;
      if (this.selectedConversation?.userId === numericSenderId) {
        this.typingUserId = numericSenderId;

        clearTimeout(this.typingTimeoutHandle);
        this.typingTimeoutHandle = setTimeout(() => {
          this.typingUserId = null;
        }, 4000);
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
    this.messages = [];

    this.chatService.getMessageHistory(this.currentUserId, convo.userId).subscribe({
      next: (data) => {
        const transformed = data.map(m => ({
          ...m,
          sentAt: new Date(m.sentAt)
        }));

        this.messages = transformed;
        this.groupedMessages = this.groupMessagesByDate(transformed);
      },
      error: (err) => console.error('Error fetching message history', err)
    });
  }

  sendMessage() {
    if (!this.newMessage.trim() || !this.selectedConversation) return;

    const currentTime = new Date();

    // Dodajemo novu poruku lokalno
    const messageToAdd = {
      id: 0,
      senderId: this.currentUserId,
      receiverId: this.selectedConversation.userId,
      content: this.newMessage,
      sentAt: currentTime,
      isRead: false,
      senderUsername: this.authService.getLoggedInUser()?.username || 'Ja',
      senderFullName: `${this.authService.getLoggedInUser()?.firstName} ${this.authService.getLoggedInUser()?.lastName}`
    };
    this.messages.push(messageToAdd);
    this.groupedMessages = this.groupMessagesByDate(this.messages);


    this.chatService.sendMessage(this.selectedConversation.userId.toString(), this.newMessage);


    const justSent = this.newMessage;
    this.newMessage = '';


    const existing = this.conversations.find(c => c.userId === this.selectedConversation?.userId);
    if (!existing) {
      const newConvo = {
        userId: this.selectedConversation.userId,
        fullName: this.selectedConversation.fullName,
        username: this.selectedConversation.username,
        lastMessage: justSent,
        lastMessageTime: currentTime,
        lastMessageSenderId: this.currentUserId
      };
      this.conversations.unshift(newConvo);
    }


    this.chatService.getConversations(this.currentUserId).subscribe(data => {
      this.conversations = data;
      const newConvo = data.find(c => c.userId === this.selectedConversation?.userId);
      if (newConvo) {
        this.selectConversation(newConvo);
      }
    });
  }


  onTyping() {
    if (this.selectedConversation) {
      this.chatService.sendTypingEvent(this.selectedConversation.userId);
    }

    clearTimeout(this.typingTimeoutHandle);
    this.typingTimeoutHandle = setTimeout(() => {}, 3000); // debounce
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
    this.closeUserModal();

    this.chatService.getMessageHistory(this.currentUserId, user.id).subscribe({
      next: (data) => {
        const transformedMessages = data.map(m => ({
          ...m,
          sentAt: new Date(m.sentAt)
        }));

        this.messages = transformedMessages;
        this.groupedMessages = this.groupMessagesByDate(transformedMessages);

        const newConvo: ConversationDto = {
          userId: user.id,
          fullName: user.fullName,
          username: user.username,
          lastMessage: transformedMessages.length > 0 ? transformedMessages[transformedMessages.length - 1].content : '',
          lastMessageTime: transformedMessages.length > 0 ? transformedMessages[transformedMessages.length - 1].sentAt : new Date(),
          lastMessageSenderId: transformedMessages.length > 0
            ? transformedMessages[transformedMessages.length - 1].senderId
            : this.currentUserId
        };

        this.selectedConversation = newConvo;


        const exists = this.conversations.some(c => c.userId === user.id);
        if (!exists && transformedMessages.length > 0) {
          this.conversations.unshift(newConvo);
        }
      },
      error: (err) => console.error('Greška pri dohvaćanju poruka:', err)
    });
  }



  handleKeyDown(event: KeyboardEvent): void {
    if (event.key === 'Enter' && !event.shiftKey) {
      event.preventDefault();
      this.sendMessage();
    }
  }

  private groupMessagesByDate(messages: MessageDto[]): { date: string, messages: MessageDto[] }[] {
    const grouped: { [key: string]: MessageDto[] } = {};
    const today = new Date();
    const yesterday = new Date();
    yesterday.setDate(today.getDate() - 1);

    messages.forEach(msg => {
      const msgDate = new Date(msg.sentAt);
      let label: string;

      if (this.isSameDate(msgDate, today)) {
        label = 'Danas';
      } else if (this.isSameDate(msgDate, yesterday)) {
        label = 'Jučer';
      } else {
        label = msgDate.toLocaleDateString('en-GB'); // 05/07/2025
      }

      if (!grouped[label]) {
        grouped[label] = [];
      }
      grouped[label].push(msg);
    });

    return Object.entries(grouped).map(([date, messages]) => ({ date, messages }));
  }

  private isSameDate(d1: Date, d2: Date): boolean {
    return d1.getFullYear() === d2.getFullYear() &&
      d1.getMonth() === d2.getMonth() &&
      d1.getDate() === d2.getDate();
  }

  get filteredConversations(): ConversationDto[] {
    if (!this.searchTerm.trim()) return this.conversations;

    const term = this.searchTerm.toLowerCase();

    return this.conversations.filter(convo => {
      const [firstName, lastName] = convo.fullName.toLowerCase().split(' ');

      return firstName.startsWith(term) || lastName.startsWith(term);
    });
  }

  getLastMessagePreview(convo: ConversationDto): string {
    return convo.lastMessageSenderId === this.currentUserId
      ? `Ti: ${convo.lastMessage}`
      : `${convo.fullName.split(' ')[0]}: ${convo.lastMessage}`;
  }

}
