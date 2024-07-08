import { ProjectTask } from "./projectTask";
import { User } from "./user";

export type Comment = {
    commentId: number;
    taskId: number;
    writtenBy: number;
    commentText: string;
    writtenAt: Date;
    task: ProjectTask;
    writtenByNavigation: User;
};