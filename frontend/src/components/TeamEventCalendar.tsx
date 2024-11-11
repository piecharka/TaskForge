import FullCalendar from "@fullcalendar/react";
import dayGridPlugin from "@fullcalendar/daygrid";
import timeGridPlugin from "@fullcalendar/timegrid";
import interactionPlugin from "@fullcalendar/interaction";
import { useEffect, useState } from "react";
import apiHandler from "../api/apiHandler";
import { useParams } from "react-router-dom";
import { SprintEvent } from "../models/sprintEvent";
import "../style/Calendar.css";
import { ProjectTask } from "../models/projectTask";
import { User } from "../models/user";
import Select from "react-select";

function TeamEventCalendar() {
    const [events, setEvents] = useState<SprintEvent[]>([]);
    const [teamUsers, setTeamUsers] = useState<User[]>([]);
    const [tasks, setTasks] = useState<ProjectTask[]>([]);
    const [filteredUsername, setFilteredUsername] = useState<string>('');
    const [ isEventCalendar, setIsEventCalendar ] = useState<boolean>(true);
    const { teamId } = useParams();

    useEffect(() => {
        apiHandler.SprintEvents.getSprintEventsByTeamId(Number(teamId))
            .then(response => setEvents(response));

        apiHandler.ProjectTasks.projectTaskListInTeam(Number(teamId))
            .then(response => setTasks(response));

        apiHandler.Users.teamUsers(Number(teamId))
            .then(response => setTeamUsers(response));

    }, [teamId, filteredUsername]);

    useEffect(() => {
        const fetchTasks = async () => {
            if (filteredUsername) {
                const response = await apiHandler.ProjectTasks.todoTasks(filteredUsername);
                setTasks(response);
            } else {
                const response = await apiHandler.ProjectTasks.projectTaskListInTeam(Number(teamId));
                setTasks(response);
            }
        };
        fetchTasks();
    }, [teamId, filteredUsername]);


    const calendarEventMap = events.map(e => ({
        title: e.sprintEventName,
        start: e.sprintEventDate,
        end: e.sprintEventDate,
    }));
    const calendarTaskMap = tasks.map(t => ({
        title: t.taskName,
        start: t.createdAt,
        end: t.taskDeadline,
    }));

    const userOptions = teamUsers.map(user => ({
        value: user.username,
        label: user.username,
    }));

    return (
        <div>
            {isEventCalendar && <FullCalendar
                events={calendarEventMap}
                headerToolbar={{
                    left: "today prev next eventsAndTasksToggle",
                    center: "title",
                    right: "dayGridMonth dayGridWeek dayGridDay",
                }}
                plugins={[dayGridPlugin, timeGridPlugin, interactionPlugin]}
                customButtons={{
                    eventsAndTasksToggle: {
                        text: "Tasks",
                        click: function () {
                            setIsEventCalendar(false);
                        },
                    },
                }}
                initialView="dayGridMonth"
            />}
            {!isEventCalendar && <div>
                <div>
                    <label>Users</label>
                    <Select
                        options={userOptions}
                        onChange={(opt) => {
                            setFilteredUsername(opt.value)
                        }}
                        //onChange={handleTypeDropdown}
                        placeholder="Select user"
                    //styles={customSelectStyles}
                    />
                </div>
                <FullCalendar
                events={calendarTaskMap}
                headerToolbar={{
                    left: "today prev next eventsAndTasksToggle",
                    center: "title",
                    right: "dayGridMonth dayGridWeek dayGridDay",
                }}
                plugins={[dayGridPlugin, timeGridPlugin, interactionPlugin]}
                customButtons={{
                    eventsAndTasksToggle: {
                        text: "Events",
                        click: function () {
                            setIsEventCalendar(true);
                        },
                    },
                }}
                initialView="dayGridMonth"
            /> </div>}
        </div>
        
    );
}

export default TeamEventCalendar;
