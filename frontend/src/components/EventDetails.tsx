import { useEffect, useState } from "react";
import { SprintEvent } from "../models/sprintEvent";
import { useNavigate, useParams } from "react-router-dom";
import apiHandler from "../api/apiHandler";
import "../style/EventDetails.css";

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
    };

    return (
        <div className="event-details">
            {event ? (
                <>
                    <h1 className="event-title">{event.sprintEventName}</h1>
                    <p className="event-date">Date: {new Date(event.sprintEventDate).toLocaleString()}</p>
                    <p className="event-description">Description: {event.sprintEventDescription}</p>
                    <p className="event-sprint">Sprint: {event.sprintName} (ID: {event.sprintId})</p>
                    <p className="event-type">Event Type: {event.eventTypeName} (ID: {event.sprintEventTypeId})</p>
                    <p className="event-team">Team ID: {event.team_id}</p>
                    <p className="event-creator">Created by User ID: {event.created_by}</p>
                    <button className="delete-button" onClick={deleteButtonHandle}>Delete Event</button>
                </>
            ) : (
                <p>Loading event details...</p>
            )}
        </div>
    );
}

export default EventDetails;