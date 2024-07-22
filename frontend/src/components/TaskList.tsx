import { Link } from "react-router-dom";
import { ProjectTask } from "../models/projectTask";
import '../style/TaskList.css';

function TaskList({ taskList, statusName }: { taskList: ProjectTask[], statusName: string }) {
    return (
        <div className="taskList">
            <h3>{statusName}</h3>
            <div className="taskList-tasks">
                {taskList.map(t => {
                    if (t.taskStatus.statusName === statusName)
                        return (
                            <Link to={"/tasks/" + t.taskId} className="taskItem" key={t.taskId}>
                                <p>{t.taskName} </p>
                                <span>{t.taskType.typeName}</span>
                            </Link>
                        );
                })}
            </div>
        </div>
    );
}

export default TaskList;
