import { Link, useParams } from "react-router-dom";
import { ProjectTask } from "../models/projectTask";
import "../style/Backlog.css";
import { useEffect, useState } from "react";
import apiHandler from "../api/apiHandler";
import { Sprint } from "../models/sprint";

function Backlog() {
    const [teamSprints, setTeamSprints] = useState<Sprint[]>([]);
    const [currentSprint, setCurrentSprint] = useState<Sprint>()
    const [taskList, setTaskList] = useState<ProjectTask[]>([]);
    const { teamId } = useParams();

    useEffect(() => {
        apiHandler.Sprints.getTeamsSprints(Number(teamId))
            .then(sprintList => setTeamSprints(sprintList));
        apiHandler.Sprints.getCurrentTeamSprint(Number(teamId))
            .then(sprint => {
                setCurrentSprint(sprint);
                console.log(sprint);
                if (sprint) {
                    apiHandler.ProjectTasks.getSprintTasks(sprint.sprintId)
                    .then(taskList => setTaskList(taskList))
                }
            })
    }, [teamId]);

    const handleSprintDropdownChange = (e) => {
        e.preventDefault();
        apiHandler.Sprints.getSprintById(Number(e.target.value))
            .then((sprint) => {
                setCurrentSprint(sprint);
                if (sprint) {
                    apiHandler.ProjectTasks.getSprintTasks(sprint.sprintId)
                        .then(taskList => setTaskList(taskList))
                }
            })
            
    }

    return (
        <div>
         <select id="sprint" name="sprint" value={currentSprint?.sprintId} onChange={handleSprintDropdownChange}>
                {teamSprints.map(s => {
                    return <option key={s.sprintId} value={s.sprintId}>{s.sprintName}</option>
                }) }
            </select>
        <div className="backlog">
              <div className="task-list">
                  <h2>To-do tasks</h2>
                {taskList.map(t => {
                    if (t.taskStatus.statusId === 1)
                      return <Link key={t.taskId} to={"/tasks/" + t.taskId} className="task">
                          <p>{t.taskName}</p>
                          <span>{t.taskType.typeName}</span>
                      </Link>
                  }) }
            </div>
              <div className="task-list">
                <h2>In progress tasks</h2>
                {taskList.map(t => {
                    if (t.taskStatus.statusId === 3)
                      return <Link key={ t.taskId} to={"/tasks/" + t.taskId} className="task">
                          <p>{t.taskName}</p>
                          <span>{t.taskType.typeName}</span>
                      </Link>
                  })}
            </div>
              <div className="task-list">
                <h2>Done tasks</h2>
                  {taskList.map(t => {
                      if (t.taskStatus.statusId === 2)
                          return <Link key={t.taskId} to={"/tasks/" + t.taskId} className="task">
                              <p>{t.taskName}</p>
                              <span>{t.taskType.typeName}</span>
                          </Link>
                  })}
            </div>
            <div className="space">
            </div>
            </div>
        </div>

  );
}

export default Backlog;