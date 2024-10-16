import { observer } from "mobx-react-lite";
import { useEffect, useState } from "react";
import apiHandler from "../api/apiHandler";
import { useStore } from "../stores/store";
import { ProjectTask } from "../models/projectTask";
import TaskTable from "../components/TaskTable";
import { Team } from "../models/team";
import TeamCarousel from "../components/TeamCarousel";
import "../style/Dashboard.css";


const DashboardView = observer(() => {
    const [todoTasks, setTodoTasks] = useState<ProjectTask[]>([]);
    const [teams, setTeams] = useState<Team[]>([]);
    const { userStore } = useStore();
    const tableHeaders: string[] = ["ID", "Title", "Deadline", "Team", "Status"]

    useEffect(() => {
        console.log(userStore.user?.username);
        apiHandler.ProjectTasks.todoTasks(userStore.user?.username)
            .then(response => {
                console.log(response);
                setTodoTasks(response)
            });
        apiHandler.Teams.teamsByUsername(userStore.user?.username)
            .then(response => {
                setTeams(response);
            });
      }, [userStore])
        
    return (
        <div className="dashboard">
            <TeamCarousel teamList={teams} />
            <TaskTable taskList={todoTasks} tableHeaders={tableHeaders} />
        </div>
    );
});

export default DashboardView;