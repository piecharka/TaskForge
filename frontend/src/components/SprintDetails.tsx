import { useEffect, useState } from "react";
import apiHandler from "../api/apiHandler";
import { Sprint } from "../models/sprint";
import { useParams } from "react-router-dom";

function SprintDetails() {
    const [currentSprint, setCurrentSprint] = useState<Sprint>()
    const [sprintList, setSprintList] = useState<Sprint[]>()
    const { teamId } = useParams();

    useEffect(() => {
        apiHandler.Sprints.getCurrentTeamSprint(Number(teamId))
            .then(response => setCurrentSprint(response));

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
      </div>
  );
}

export default SprintDetails;