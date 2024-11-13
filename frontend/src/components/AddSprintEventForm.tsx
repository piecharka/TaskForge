import { useEffect, useState } from "react";
import "../style/NewTaskForm.css"
import DatePicker from "react-datepicker";
import apiHandler from "../api/apiHandler";
import { useParams } from "react-router-dom";
import { useStore } from "../stores/store";
import Select from "react-select";
import { Sprint } from "../models/sprint";
import { SprintEventType } from "../models/sprintEventType";

function AddSprintEventForm() {
    const [sprintEventName, setSprintEventName] = useState<string>('');
    const [sprintId, setSprintId] = useState<number>(-1);
    const [sprintEventDate, setSprintEventDate] = useState<Date>(new Date());
    const [sprintEventTypeId, setSprintEventTypeId] = useState<number>(-1);
    const [error, setError] = useState<string>('');

    const [sprints, setSprints] = useState<Sprint[]>([]);
    const [sprintEventTypes, setSprintEventTypes] = useState<SprintEventType[]>([]);
    const { teamId } = useParams();
    const { userStore } = useStore();

    useEffect(() => {
        apiHandler.Sprints.getTeamsSprints(Number(teamId))
            .then((response) => setSprints(response));

        apiHandler.SprintEventTypes.getSprintEventTypes()
            .then(response => setSprintEventTypes(response));
    }, [teamId])

    const handleSubmit = (e) => {
        e.preventDefault();
        apiHandler.SprintEvents.postSprintEvent({
            sprintEventName: sprintEventName,
            sprintId: sprintId,
            sprintEventDate: sprintEventDate,
            createdBy: userStore.user?.userId,
            teamId: Number(teamId),
            sprintEventTypeId: sprintEventTypeId,
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

    const sprintOptions = sprints.map(s => ({
        value: s.sprintId,
        label: s.sprintName
    }))

    const sprintEventTypeOptions = sprintEventTypes.map(t => ({
        value: t.eventTypeId,
        label: t.eventTypeName
    }))

    return (
        <div className="form-position">
            <form onSubmit={handleSubmit} className="task-form">
                <div className="task-input">
                    <label>Sprint event name</label>
                    <input type="text" id="sprintName" value={sprintEventName} onChange={(e) => setSprintEventName(e.target.value)} />
                </div>
                <div className="task-input">
                    <label>Sprint start</label>
                    <DatePicker selected={sprintEventDate} onChange={(date: Date | null) => setSprintEventDate(date)} />
                </div>

                <div className="task-input">
                    <label>Sprint</label>
                    <Select
                        options={sprintOptions}
                        onChange={(opt) => setSprintId(opt.value)}
                        //onChange={handleTypeDropdown}
                        placeholder="Select sprint"
                    //styles={customSelectStyles}
                    />
                </div>

                <div className="task-input">
                    <label>Sprint</label>
                    <Select
                        options={sprintEventTypeOptions}
                        onChange={(opt) => setSprintEventTypeId(opt.value)}
                        //onChange={handleTypeDropdown}
                        placeholder="Select event type"
                    //styles={customSelectStyles}
                    />
                </div>
                

                {error && <div className="error">{error}</div>} {/* Wyœwietl komunikat b³êdu */}
                <button className="submit-button" type="submit">Submit</button>
            </form>
        </div>
    );
}

export default AddSprintEventForm;