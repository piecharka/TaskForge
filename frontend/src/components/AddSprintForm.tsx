import { useState } from "react";
import "../style/NewTaskForm.css"
import DatePicker from "react-datepicker";
import apiHandler from "../api/apiHandler";
import { useParams } from "react-router-dom";
import { useStore } from "../stores/store";

function AddSprintForm() {
    const [sprintName, setSprintName] = useState<string>('');
    const [sprintStart, setSprintStart] = useState<Date>(new Date());
    const [sprintEnd, setSprintEnd] = useState<Date>(new Date());
    const [sprintPlanningDate, setSprintPlanningDate] = useState<Date>(new Date());
    const [sprintReviewDate, setSprintReviewDate] = useState<Date>(new Date());
    const [sprintRetroDate, setSprintRetroDate] = useState<Date>(new Date());
    const [goalDescription, setGoalDescription] = useState<string>('');
    const [error, setError] = useState<string>('');
    const { teamId } = useParams();
    const { userStore } = useStore();

    const handleSubmit = (e) => {
        e.preventDefault();
            apiHandler.Sprints.postSprint({
                sprintName: sprintName,
                sprintStart: sprintStart,
                sprintEnd: sprintEnd,
                goalDescription: goalDescription,
                teamId: Number(teamId),
                sprintPlanning: sprintPlanningDate,
                sprintRetro: sprintRetroDate,
                sprintReview: sprintReviewDate,
                createdBy: userStore.user.userId,
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

              <div className="task-input">
                  <label>Sprint planning date</label>
                  <DatePicker selected={sprintPlanningDate} onChange={(date: Date | null) => setSprintPlanningDate(date)} />
              </div>

              <div className="task-input">
                  <label>Sprint review date</label>
                  <DatePicker selected={sprintReviewDate} onChange={(date: Date | null) => setSprintReviewDate(date)} />
              </div>

              <div className="task-input">
                  <label>Sprint retro date</label>
                  <DatePicker selected={sprintRetroDate} onChange={(date: Date | null) => setSprintRetroDate(date)} />
              </div>
             
              {error && <div className="error">{error}</div>} {/* Wyœwietl komunikat b³êdu */}
              <button className="submit-button" type="submit">Submit</button>
          </form>
      </div>
  );
}

export default AddSprintForm;