import { Attachment } from "./attachment";
import { ProjectTaskStatus } from "./projectTaskStatus";
import { ProjectTaskType } from "./projectTaskType";
import { Sprint } from "./sprint";
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
    sprintId: number;
    attachments: Attachment[];
    comments: Comment[];
    createdByNavigation: User;
    taskStatus: ProjectTaskStatus;
    sprint: Sprint;
    taskType: ProjectTaskType;
    team: Team;
    usersTasks: UsersTask[];
};