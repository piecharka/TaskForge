import { useEffect, useState } from "react";
import { ProjectTask } from "../models/projectTask";
import apiHandler from "../api/apiHandler";
import { useParams } from "react-router-dom";
import { ProjectTaskStatus } from "../models/projectTaskStatus";
import TaskList from "../components/TaskList";
import '../style/TaskList.css';
import { observer } from "mobx-react-lite";


const ProjectTaskView = observer(() => {
    const { teamId } = useParams<{ teamId: string }>();
    const [projectTaskList, setProjectTaskList] = useState<ProjectTask[]>([]);
    const [projectTaskStatusList, setProjectTaskStatusList] = useState<ProjectTaskStatus[]>([]);

    useEffect(() => {
        apiHandler.ProjectTasks.projectTaskListInTeam(Number(teamId)).then(response => {
            setProjectTaskList(response)
        })

        apiHandler.ProjectTaskStatuses.projectTaskStatusesList().then(response => {
            setProjectTaskStatusList(response)
        })

    }, [teamId])

    return (<div className="table">
        {projectTaskStatusList.map(pts => (
            <TaskList key={pts.statusId} taskList={projectTaskList} statusName={pts.statusName} />
        ))}
    </div>);
});

export default ProjectTaskView;