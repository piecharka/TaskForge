import { NotificationStatus } from "./notificationStatus";
import { User } from "./user";

export type Notification = {
    notificationId: number;
    userId: number;
    message: string;
    sentAt: Date;
    notificationStatusId: number;
    notificationStatus: NotificationStatus;
    user: User;
};