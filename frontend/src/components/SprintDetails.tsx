import { useEffect, useState } from "react";
import apiHandler from "../api/apiHandler";
import { Sprint } from "../models/sprint";
import { useParams } from "react-router-dom";
import { Bar, BarChart, CartesianGrid, Legend, Tooltip, XAxis, YAxis } from "recharts";

function SprintDetails() {
    const [currentSprint, setCurrentSprint] = useState<Sprint>();
    const [sprintList, setSprintList] = useState<Sprint[]>();
    const [sprintOverdueLogsCount, setSprintOverdueLogsCount] = useState<number>(0);
    const [tasksCount, setTasksCount] = useState<number>(0);
    const { teamId } = useParams();

    useEffect(() => {
        apiHandler.Sprints.getCurrentTeamSprint(Number(teamId))
            .then(response => {
                setCurrentSprint(response)
                if (response) {
                    apiHandler.TimeLogs.sprintOverdueLogsCount(response.sprintId)
                        .then(c => setSprintOverdueLogsCount(c));

                    apiHandler.ProjectTasks.getTasksCount(response.sprintId)
                        .then(c => setTasksCount(c));
                }
            });

        apiHandler.Sprints.getTeamsSprints(Number(teamId))
            .then((response) => setSprintList(response));

    }, [teamId])

    const handleSprintDropdownChange = (e) => {
        e.preventDefault();
        apiHandler.Sprints.getSprintById(Number(e.target.value))
            .then((sprint) => {
                setCurrentSprint(sprint);
                //if (sprint) {
                //    apiHandler.ProjectTasks.getSprintTasks(sprint.sprintId)
                //        .then(taskList => setTaskList(taskList))
                //}
            })
    }

    const taskCountMap = [
        { name: 'on time', value: tasksCount - sprintOverdueLogsCount },
        { name: 'overdue deadline', value: sprintOverdueLogsCount },
    ]

  return (
      <div>
          <select id="sprint" name="sprint" value={currentSprint?.sprintId} onChange={handleSprintDropdownChange}>
              {sprintList && sprintList.map(s => {
                  return <option key={s.sprintId} value={s.sprintId}>{s.sprintName}</option>
              })}
          </select>
          {currentSprint &&
              <div>
                  <h1>{currentSprint.sprintName}</h1>
              </div>
          }

          <BarChart data={taskCountMap} width={730} height={250}>
              <CartesianGrid strokeDasharray="3 3" />
              <XAxis dataKey="name" />
              <YAxis />
              <Tooltip />
              <Legend />
              <Bar dataKey="value" fill="#8884d8" />
          </BarChart>
      </div>
  );
}

export default SprintDetails;