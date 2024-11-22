import { useEffect, useState } from "react";
import apiHandler from "../api/apiHandler";
import { useParams } from "react-router-dom";
import { Sprint } from "../models/sprint";
import { SprintEvent } from "../models/sprintEvent";
import { Bar, BarChart, CartesianGrid, Legend, Tooltip, XAxis, YAxis } from "recharts";

function TeamSummary() {
    const [currentSprint, setCurrentSprint] = useState<Sprint>();
    const [todoCount, setTodoCount] = useState<number>(0);
    const [inProgressCount, setInProgressCount] = useState<number>(0);
    const [doneCount, setDoneCount] = useState<number>(0);
    const [closestThreeEvents, setClosestThreeEvents] = useState<SprintEvent[]>([])
    const { teamId } = useParams();

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
                }
            })

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

  return (
    <div>
          <h3>{inProgressCount}/{doneCount}</h3>
          <h3>{sprintDaysLeft !== null
              ? `${sprintDaysLeft} days left`
              : "No active sprint"}</h3>
          {closestThreeEvents && closestThreeEvents.map(e => (<div>
              <p>{e.sprintEventName}</p>
              <p>{e.sprintEventDate}</p>
          </div>))}
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

export default TeamSummary;