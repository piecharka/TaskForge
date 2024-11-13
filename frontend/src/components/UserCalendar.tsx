import FullCalendar from "@fullcalendar/react";
import dayGridPlugin from "@fullcalendar/daygrid";
import timeGridPlugin from "@fullcalendar/timegrid";
import interactionPlugin from "@fullcalendar/interaction";
import { SprintEvent } from "../models/sprintEvent";
import { useEffect, useState } from "react";
import apiHandler from "../api/apiHandler";
import { useStore } from "../stores/store";
import { ProjectTask } from "../models/projectTask";
function UserCalendar() {
    const [events, setEvent] = useState<SprintEvent[]>([]);
    const [tasks, setTasks] = useState<ProjectTask[]>([]);
    const [isEventCalendar, setIsEventCalendar] = useState<boolean>(true);
    const { userStore } = useStore();

    useEffect(() => {
        apiHandler.SprintEvents.getSprintEventsByUserId(userStore.user.userId)
            .then(response => setEvent(response));

        apiHandler.ProjectTasks.todoTasks(userStore.user.username)
            .then(response => { console.log(response); setTasks(response); });
    }, [userStore])

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

    console.log(calendarTaskMap);

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
          />
                }
            {!isEventCalendar && <FullCalendar
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
            />}
        </div>
  );
}

export default UserCalendar;