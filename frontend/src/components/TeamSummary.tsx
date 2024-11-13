import { useEffect, useState } from "react";
import apiHandler from "../api/apiHandler";
import { useParams } from "react-router-dom";
import { Sprint } from "../models/sprint";
import { SprintEvent } from "../models/sprintEvent";

function TeamSummary() {
    const [currentSprint, setCurrentSprint] = useState<Sprint>();
    const [inProgressCount, setInProgressCount] = useState<number>(0);
    const [doneCount, setDoneCount] = useState<number>(0);
    const [closestThreeEvents, setClosestThreeEvents] = useState<SprintEvent[]>([])
    const { teamId } = useParams();

    useEffect(() => {
        apiHandler.Sprints.getCurrentTeamSprint(Number(teamId))
            .then(sprint => {
                setCurrentSprint(sprint);
                if (sprint) {
                    apiHandler.ProjectTasks.getInProgressTasksCount(sprint.sprintId)
                        .then(count => setInProgressCount(count));
                    apiHandler.ProjectTasks.getDoneTasksCount(sprint.sprintId)
                        .then(count => setDoneCount(count));
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
    </div>
  );
}

export default TeamSummary;