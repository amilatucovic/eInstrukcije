export interface MessageDto {
  id: number;
  senderId: number;
  senderUsername: string;
  senderFullName: string;
  receiverId: number;
  content: string;
  sentAt: Date;
  isRead: boolean;
}
