import { observer } from "mobx-react-lite";
import { useEffect, useState } from "react";
import apiHandler from "../api/apiHandler";
import { useStore } from "../stores/store";
import { ProjectTask } from "../models/projectTask";
import { Team } from "../models/team";
import TeamCarousel from "../components/TeamCarousel";
import "../style/Dashboard.css";
import { Link } from "react-router-dom";
import '../style/TaskTable.css';
import { RiArrowDownLine, RiArrowUpLine } from "react-icons/ri";
import { LuArrowUpDown } from "react-icons/lu";


const DashboardView = observer(() => {
    const [todoTasks, setTodoTasks] = useState<ProjectTask[]>([]);
    const [teams, setTeams] = useState<Team[]>([]);
    const { userStore } = useStore();


    const tableHeaders: string[] = ["ID", "Title", "Deadline", "Status", "Type", "Sprint"];
    const [sortIcons, setSortIcons] = useState<Record<string, 'default' | 'asc' | 'desc'>>(
        tableHeaders.reduce((acc, header) => ({ ...acc, [header]: 'default' }), {})
    );

    const dateOptions: Intl.DateTimeFormatOptions = {
        year: 'numeric',
        month: 'long',
        day: 'numeric',
        hour: '2-digit',
        minute: '2-digit',
        second: '2-digit',
    };

    const getSortIcon = (iconState: 'default' | 'asc' | 'desc'): JSX.Element => {
        if (iconState === 'desc') return <RiArrowDownLine />;
        if (iconState === 'asc') return <RiArrowUpLine />;
        return <LuArrowUpDown />;
    };

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

    const toggleSortIcon = (header: string): void => {
        setSortIcons((prevIcons) => {
            const newIconState: 'default' | 'asc' | 'desc' = prevIcons[header] === 'default' ? 'desc'
                : prevIcons[header] === 'desc' ? 'asc'
                    : 'default';

            const updatedIcons: Record<string, 'default' | 'asc' | 'desc'> = { ...prevIcons, [header]: newIconState };

            // Resetujemy wszystkie inne nag³ówki, które nie zosta³y wybrane, do 'default'
            for (const key in updatedIcons) {
                if (key !== header && updatedIcons[key] !== 'default') {
                    updatedIcons[key] = 'default';
                }
            }

            return updatedIcons;
        });
    };


    return (
        <div className="dashboard">
            <TeamCarousel teamList={teams} />
            
            <div className="table-container">
                <table className="task-table">
                    <thead>
                        <tr>
                            {tableHeaders.map(header => (
                                <th key={header} className="header-cell" onClick={() => toggleSortIcon(header)}>
                                    {header}{getSortIcon(sortIcons[header])}
                                </th>
                            ))}
                        </tr>
                        <tr>
                            {tableHeaders.map(header => (
                                <th key={header} className="filter-cell">
                                    <input
                                        type="text"
                                        placeholder={`Filter by ${header}`}
                                        onChange={(e) => updateFilter(header, e.target.value)}
                                    />
                                </th>
                            ))}
                        </tr>
                    </thead>
                    <tbody>
                        {todoTasks && todoTasks.map((t) => (
                            <tr key={t.taskId} className={`task-row`}>
                                <td className="task-cell">{t.taskId}</td>
                                <Link to={"/tasks/" + t.taskId} className="row-link"><td className="task-cell">{t.taskName}</td></Link>
                                <td className="task-cell">{new Date(t.taskDeadline).toLocaleDateString('en-EN', dateOptions)}</td>
                                {tableHeaders.includes("Team") && <td className="task-cell">{t.team.teamName}</td>}
                                <td className="task-cell">{t.taskStatus.statusName}</td>
                                <td className="task-cell">{t.taskType.typeName}</td>
                                <Link to="" className="row-link"><td className="task-cell">{t.sprint.sprintName}</td></Link>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
        </div>
    );
});

export default DashboardView;