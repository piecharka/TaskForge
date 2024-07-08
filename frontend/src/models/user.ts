import { Attachment } from "./attachment";
import { ProjectTask } from "./projectTask";
import { Team } from "./team";
import { UsersTask } from "./usersTask";

export type User = {
    userId: number;
    username: string;
    email: string;
    passwordHash: string;
    birthday: Date;
    createdAt: Date;
    updatedAt: Date;
    lastLogin: Date;
    isActive: boolean;
    attachments: Attachment[];
    comments: Comment[];
    notifications: Notification[];
    projectTasks: ProjectTask[];
    usersTasks: UsersTask[];
    teams: Team[];
};