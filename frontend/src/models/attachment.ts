import { ProjectTask } from "./projectTask";
import { User } from "./user";

export type Attachment = {
    taskId: number;
    addedBy: number;
    filePath: string;
    addedByNavigation: User;
    task: ProjectTask;
}
