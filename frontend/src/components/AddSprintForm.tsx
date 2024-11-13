import { useState } from "react";
import "../style/NewTaskForm.css"
import DatePicker from "react-datepicker";
import apiHandler from "../api/apiHandler";
import { useParams } from "react-router-dom";

function AddSprintForm() {
    const [sprintName, setSprintName] = useState<string>('');
    const [sprintStart, setSprintStart] = useState<Date>(new Date());
    const [sprintEnd, setSprintEnd] = useState<Date>(new Date());
    const [goalDescription, setGoalDescription] = useState<string>('');
    const [error, setError] = useState<string>('');
    const { teamId } = useParams();

    const handleSubmit = (e) => {
        e.preventDefault();
            apiHandler.Sprints.postSprint({
                sprintName: sprintName,
                sprintStart: sprintStart,
                sprintEnd: sprintEnd,
                goalDescription: goalDescription,
                teamId: Number(teamId),
            })
                .then(() => {
                    setError('');

                }).catch((error: any) => {
                    if (error.response && error.response.data && error.response.data.message) {
                        setError(error.response.data.message);
                    } else if (error.message) {
                        setError(error.message);
                    } else {
                        setError('An unexpected error occurred');
                    }
                });

        
    }

  return (
      <div className="form-position">
          <form onSubmit={handleSubmit} className="task-form">
              <div className="task-input">
                  <label>Sprint name</label>
                  <input type="text" id="sprintName" value={sprintName} onChange={(e) => setSprintName(e.target.value)} />
              </div>
              <div className="task-input">
                  <label>Sprint start</label>
                  <DatePicker selected={sprintStart} onChange={(date: Date | null) => setSprintStart(date)} />
              </div>

              <div className="task-input">
                  <label>Sprint end</label>
                  <DatePicker selected={sprintEnd} onChange={(date: Date | null) => setSprintEnd(date)} />
              </div>
              <div className="task-input">
                  <label>Goal description</label>
                  <input type="text" id="goalDescription" value={goalDescription} onChange={(e) => setGoalDescription(e.target.value)} />
              </div>
             
              {error && <div className="error">{error}</div>} {/* Wyœwietl komunikat b³êdu */}
              <button className="submit-button" type="submit">Submit</button>
          </form>
      </div>
  );
}

export default AddSprintForm;