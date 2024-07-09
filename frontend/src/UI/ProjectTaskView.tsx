import { useEffect, useState } from "react";
import { ProjectTask } from "../models/projectTask";
import apiHandler from "../api/apiHandler";
import { useParams } from "react-router-dom";


function ProjectTaskView() {
    const { teamId } = useParams<{ teamId: string }>();
    const [projectTaskList, setProjectTaskList] = useState<ProjectTask[]>([]);

    useEffect(() => {
        apiHandler.ProjectTasks.projectTaskListInTeam(Number(teamId)).then(response => {
            setProjectTaskList(response)
        })
    }, [teamId])

    return (<div>
        {projectTaskList.map(t => (
            <p key={t.taskId }>{t.taskName}</p>))}
    </div>);
}

export default ProjectTaskView;