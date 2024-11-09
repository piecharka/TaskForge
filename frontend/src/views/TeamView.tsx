import { useEffect, useState } from "react";
import { Link, useParams } from "react-router-dom";
import apiHandler from "../api/apiHandler";
import { Team } from "../models/team";
import { User } from "../models/user";
import { observer } from "mobx-react-lite";
import "../style/Team.css"
import { ProjectTask } from "../models/projectTask";
import TaskTable from "../components/TaskTable";
import Backlog from "../components/Backlog";
import NewTaskForm from "../components/NewTaskForm";

const TeamView = observer(() => {
    const { teamId } = useParams<{ teamId: string }>();
    const [team, setTeam] = useState<Team | null>(null);
    const [usersInTeam, setUsersInTeam] = useState<User[]>([]);
    const [tasks, setTasks] = useState<ProjectTask[]>([]);
    const [activeLink, setActiveLink] = useState<string>("Summary");
    const tableHeaders: string[] = ["ID", "Title", "Deadline", "Status"]

    useEffect(() => {
        apiHandler.Teams.first(Number(teamId)).then(response => {
            setTeam(response);
        });

        apiHandler.Users.teamUsers(Number(teamId)).then(response => {
            setUsersInTeam(response);
        })

        apiHandler.ProjectTasks.projectTaskListInTeam(Number(teamId)).then(response => {
            setTasks(response);
        })

    }, [teamId])

    if (team === null) {
        return <div>Loading...</div>;
    }

    return (
        <div>
            <h1>{team.teamName}</h1>
            <div className="horizontal-navbar">
                {["Summary", "Backlog", "List","New Task", "Sprint", "Team settings"].map(link => (
                    <Link
                        to="#"
                        key={link}
                        onClick={() => setActiveLink(link)} 
                        style={{ color: activeLink === link ? "#0C7C59" : "white" }} 
                    >
                        {link}
                    </Link>
                ))}
            </div>
            {activeLink === "Summary" && <div className="">
                {usersInTeam.map(u => (
                    <Link to={"/users/" + u.userId} key={u.userId}>{u.username}</Link>))}
            </div>}

            {activeLink === "New Task" && <NewTaskForm /> }

            {activeLink === "Backlog" && <Backlog  /> }

            {activeLink === "List" &&<TaskTable taskList={tasks} tableHeaders={tableHeaders} />}
        </div>
    );
});

export default TeamView;