import { useEffect, useState } from "react";
import apiHandler from "../api/apiHandler";
import { useParams } from "react-router-dom";
import { Sprint } from "../models/sprint";
import { SprintEvent } from "../models/sprintEvent";
import { Bar, BarChart, CartesianGrid, Legend, Tooltip, XAxis, YAxis } from "recharts";
import { UserTaskCountDto } from "../DTOs/UserTaskCountDto";
import "../style/TeamSummary.css"

function TeamSummary() {
    const [currentSprint, setCurrentSprint] = useState<Sprint>();
    const [todoCount, setTodoCount] = useState<number>(0);
    const [inProgressCount, setInProgressCount] = useState<number>(0);
    const [doneCount, setDoneCount] = useState<number>(0);
    const [averageTaskCountPerSprint, setAverageTaskCountPerSprint] = useState<number>(0);
    const [averageTaskCountPerUser, setAverageTaskCountPerUser] = useState<number>(0);
    const [averageTimeForTask, setAverageTimeForTask] = useState<number>(0);
    const [userTaskCount, setUserTaskCount] = useState<UserTaskCountDto[]>([]);
    const [closestThreeEvents, setClosestThreeEvents] = useState<SprintEvent[]>([])
    const { teamId } = useParams();

    const dateOptions: Intl.DateTimeFormatOptions = {
        year: 'numeric',
        month: 'long',
        day: 'numeric',
        hour: '2-digit',
        minute: '2-digit',
        second: '2-digit',
    };

    useEffect(() => {
        apiHandler.Sprints.getCurrentTeamSprint(Number(teamId))
            .then(sprint => {
                setCurrentSprint(sprint);
                if (sprint) {
                    console.log(sprint);
                    apiHandler.ProjectTasks.getInProgressTasksCount(sprint.sprintId)
                        .then(count => setInProgressCount(count));
                    apiHandler.ProjectTasks.getDoneTasksCount(sprint.sprintId)
                        .then(count => setDoneCount(count));
                    apiHandler.ProjectTasks.getTodoTasksCount(sprint.sprintId)
                        .then(count => setTodoCount(count));
                    apiHandler.ProjectTasks.getUserTasksInSprintCount(Number(teamId), sprint.sprintId)
                        .then(count => setUserTaskCount(count));
                }
            })

        apiHandler.Analytics.averageTaskCountPerSprint(Number(teamId))
            .then((response) => setAverageTaskCountPerSprint(response));

        apiHandler.Analytics.averageTaskCountPerUser(Number(teamId))
            .then((response) => setAverageTaskCountPerUser(response));

        apiHandler.Analytics.averageTimeForTask(Number(teamId))
            .then((response) => setAverageTimeForTask(response));

        apiHandler.SprintEvents.getClosestThreeEventsByTeamId(Number(teamId))
            .then(events => {
                setClosestThreeEvents(events);
            })

    }, [teamId])

    const sprintDaysLeft = currentSprint
        ? Math.ceil((new Date(currentSprint.sprintEnd).getTime() - Date.now()) / (1000 * 60 * 60 * 24))
        : null;

    const taskCountMap = [
        {name: 'to-do', value: todoCount},
        { name: 'in-progress', value: inProgressCount },
        { name: 'done', value: doneCount}
    ]

    const userTaskCountMap = userTaskCount.map((userCount) => ({
        name: userCount.username,
        value: userCount.taskCount,
    }));
        


  return (
      <div className="team-summary">
          <div className="team-events">
              <h3>{sprintDaysLeft !== null
                  ? `${sprintDaysLeft} days of sprint left`
                  : "No active sprint"}</h3>

              {closestThreeEvents && <h2>Three events soon</h2> }
              {closestThreeEvents && closestThreeEvents.map(e =>
              (<div>
                  <p>{e.sprintEventName}</p>
                  <p>{new Date(e.sprintEventDate).toLocaleDateString('en-EN', dateOptions)}</p>
              </div>))}
          </div>

          <div className="team-stats">
              <p>Average task count per sprint: { averageTaskCountPerSprint}</p>
              <p>Average task count per user: { averageTaskCountPerUser}</p>
              <p>Average time for task: { averageTimeForTask} days</p>
          </div>

          <BarChart data={taskCountMap} width={730} height={250} className="chart-1">
              <CartesianGrid strokeDasharray="3 3" />
              <XAxis dataKey="name" />
              <YAxis />
              <Tooltip />
              <Legend />
              <Bar dataKey="value" fill="#58A4B0" />
          </BarChart>

          <BarChart data={userTaskCountMap} width={730} height={250} className="chart-2">
              <CartesianGrid strokeDasharray="3 3" />
              <XAxis dataKey="name" />
              <YAxis />
              <Tooltip />
              <Legend />
              <Bar dataKey="value" fill="#58A4B0" />
          </BarChart>
    </div>
  );
}

export default TeamSummary;