import { ProjectTask } from "../models/projectTask";
import '../style/TaskList.css';


function TaskList({ taskList, statusName }: {taskList : ProjectTask[], statusName: string} ) {
    

    return (<div className="taskList">
        <h2>{statusName}</h2>
        <div className="taskList-tasks">
        {taskList.map(t => {
            if (t.taskStatus.statusName === statusName)
                return <div key={t.taskId}>
                    <h3>{t.taskName}</h3>
                    <p>{t.taskType.typeName}</p>
                </div>
        })}
        </div>
    </div>);
}

export default TaskList;