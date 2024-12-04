import { useEffect, useState } from "react";
import { Notification } from "../models/notification";
import apiHandler from "../api/apiHandler";
import { useStore } from "../stores/store";
import { RiDeleteBin5Line } from "react-icons/ri";
import "../style/NotificationList.css";

function NotificationList() {
    const [notifications, setNotifications] = useState<Notification[]>([]);
    const { userStore } = useStore();

    useEffect(() => {
        if (userStore.user) {
            apiHandler.Notifications.getUsersNotifications(userStore.user.userId)
                .then(response => setNotifications(response));
        }
    }, [userStore]);

    const dateOptions = {
        year: 'numeric',
        month: 'long',
        day: 'numeric',
        hour: '2-digit',
        minute: '2-digit',
        second: '2-digit',
    };

    const handleDelete = async (id : number) => {
        if (userStore.user) {
            await apiHandler.Notifications.deleteNotification(id);
            const updatedNotifications = await apiHandler.Notifications.getUsersNotifications(userStore.user.userId);
            setNotifications(updatedNotifications);
        }
    }

    const handleReading = async (id: number) => {
        if (userStore.user) {
            await apiHandler.Notifications.updateNotificiationStatus(id, 1);
            const updatedNotifications = await apiHandler.Notifications.getUsersNotifications(userStore.user.userId);
            setNotifications(updatedNotifications);
        }
    }

    return (
        <div className="notification-list">
            {notifications.length === 0 ? (
                <p>No new notification</p>
            ) : (
                notifications.map((n) => 
                    { 
                    return <div onClick={() => handleReading(n.notificationId) } key={n.notificationId}
                        className={"notification-item " + (n.notificationStatus.statusId === 2 ? "not-read" : "")} >
                        <div>
                            <p className="notification-message">{n.message}</p>
                            <p className="notification-time">{new Date(n.sentAt).toLocaleDateString('en-EN', dateOptions)}</p>
                        </div>
                        <RiDeleteBin5Line onClick={() => handleDelete(n.notificationId)} />
                    </div>
                    }
                )
            )}
        </div>
  );
}

export default NotificationList;