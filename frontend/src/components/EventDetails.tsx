import { useEffect, useState } from "react";
import { SprintEvent } from "../models/sprintEvent";
import { useNavigate, useParams } from "react-router-dom";
import apiHandler from "../api/apiHandler";

function EventDetails() {
    const [event, setEvent] = useState<SprintEvent>();
    const { eventId } = useParams();
    const navigate = useNavigate();

    useEffect(() => {
        apiHandler.SprintEvents.getSprintEventById(Number(eventId))
            .then(response => setEvent(response));
    }, [eventId]);

    const deleteButtonHandle = () => {
        apiHandler.SprintEvents.deleteSprintEvent(Number(eventId));
        navigate(-1);
    }

  return (
      <div>
          <h1>{event?.sprintEventName}</h1>
          <p>{event?.sprintEventDate}</p>
          <button onClick={deleteButtonHandle}> Delete task</button>
    </div>
  );
}

export default EventDetails;