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

export type UserLoginData = {
    username: string;
    password: string;
}

export type UserRegisterData = {
    username: string;
    password: string;
    email: string;
    birthday: Date | null;
}

export type UserStoredData = {
    userId: number;
    username: string;
    token: string;
    lastLogin: Date;
    teams: Team[];
}