import FullCalendar from "@fullcalendar/react";
import dayGridPlugin from "@fullcalendar/daygrid";
import timeGridPlugin from "@fullcalendar/timegrid";
import interactionPlugin from "@fullcalendar/interaction";
import { SprintEvent } from "../models/sprintEvent";
import { useEffect, useState } from "react";
import apiHandler from "../api/apiHandler";
import { useStore } from "../stores/store";
function UserCalendar() {
    const [events, setEvent] = useState<SprintEvent[]>([]);
    const { userStore } = useStore();

    useEffect(() => {
        apiHandler.SprintEvents.getSprintEventsByUserId(userStore.user.userId)
            .then(response => setEvent(response));
    }, [userStore])

    const calendarEventMap = events.map(e => ({
        title: e.sprintEventName,
        start: e.sprintEventDate,
        end: e.sprintEventDate,
    }));

  return (
      <FullCalendar
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
                      console.log('baddie')
                  },
              },
          }}
          initialView="dayGridMonth"
      />
  );
}

export default UserCalendar;