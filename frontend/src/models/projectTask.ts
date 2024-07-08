import { Attachment } from "./attachment";
import { ProjectTaskStatus } from "./projectTaskStatus";
import { ProjectTaskType } from "./projectTaskType";
import { Team } from "./team";
import { User } from "./user";
import { UsersTask } from "./usersTask";

export type ProjectTask = {
    taskId: number;
    taskName: string;
    taskStatusId: number;
    teamId: number;
    createdBy: number;
    createdAt: Date;
    taskDeadline: Date;
    taskTypeId: number;
    taskDescription: string;
    updatedAt: Date;
    attachments: Attachment[];
    comments: Comment[];
    createdByNavigation: User;
    taskStatus: ProjectTaskStatus;
    taskType: ProjectTaskType;
    team: Team;
    usersTasks: UsersTask[];
};