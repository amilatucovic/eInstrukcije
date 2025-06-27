import { Component } from '@angular/core';

interface Conversation {
  id: number;
  name: string;
  lastMessage: string;
  time: string;
}

interface Message {
  text: string;
  time: string;
  isMine: boolean;
  isNewDay?: boolean;
  dayLabel?: string;
}

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent {
  conversations: Conversation[] = [
    { id: 1, name: 'Emina Tucović', lastMessage: 'Dogovoreno, vidimo se!', time: 'just now' },
    { id: 2, name: 'Benjamin Muratović', lastMessage: 'Radujem se susretu!', time: '2 d' },
    { id: 3, name: 'Selma Alagić', lastMessage: 'Zadaću koju si poslala...', time: '1 m' }
  ];

  selectedConversation: Conversation | null = this.conversations[0];

  messages: Message[] = [
    { text: 'Dobar dan profesorice...', time: '08:24', isMine: false, isNewDay: true, dayLabel: 'Today' },
    { text: 'Jutro, Emina!', time: '08:30', isMine: true },
    { text: 'Okei, nema problema.', time: '08:31', isMine: false },
    { text: 'Da li vam slučaj...', time: '08:32', isMine: true },
    { text: 'Dogovoreno, vidimo se!', time: '09:30', isMine: false }
  ];

  newMessage: string = '';

  sendMessage() {
    if (!this.newMessage.trim()) return;

    const currentTime = new Date().toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' });
    this.messages.push({
      text: this.newMessage,
      time: currentTime,
      isMine: true
    });

    this.newMessage = '';
  }
}
