import '../style/TaskTable.css';
import { ProjectTask } from '../models/projectTask';
import { useCallback, useEffect, useState } from 'react';
import apiHandler from '../api/apiHandler';
import { Link, useParams } from 'react-router-dom';
import { LuArrowUpDown } from "react-icons/lu";
import { RiArrowDownLine, RiArrowUpLine } from "react-icons/ri";

function TaskTable() {
    const [taskList, setTaskList] = useState<ProjectTask[]>();
    const [currentSprintMark, setCurrentSprintMark] = useState<boolean>(false);
    const tableHeaders = ["ID", "Title", "Deadline", "Status", "Created by", "Attached to", "Type", "Sprint"];
    const [sortIcons, setSortIcons] = useState(
        tableHeaders.reduce((acc, header) => ({ ...acc, [header]: 'default' }), {})
    );
    const { teamId } = useParams(); 
    const dateOptions = {
        year: 'numeric',
        month: 'long',
        day: 'numeric',
        hour: '2-digit',
        minute: '2-digit',
        second: '2-digit',
    };

    // Funkcja do zmiany sortowania po klikniêciu nag³ówka tabeli
    const toggleSortIcon = (header) => {
        setSortIcons((prevIcons) => {
            const newIconState = prevIcons[header] === 'default' ? 'desc'
                : prevIcons[header] === 'desc' ? 'asc'
                    : 'default';

            // Jeœli inny nag³ówek jest ju¿ sortowany, zresetuj go do 'default'
            const updatedIcons = { ...prevIcons, [header]: newIconState };

            // Resetujemy wszystkie inne nag³ówki, które nie zosta³y wybrane, do 'default'
            for (const key in updatedIcons) {
                if (key !== header && updatedIcons[key] !== 'default') {
                    updatedIcons[key] = 'default';
                }
            }

            return updatedIcons;
        });
    };

    // Funkcja do ustawiania odpowiedniej ikony sortowania
    const getSortIcon = (iconState) => {
        if (iconState === 'desc') return <RiArrowDownLine />;
        if (iconState === 'asc') return <RiArrowUpLine />;
        return <LuArrowUpDown />;
    };

    // Fetch tasks z API uwzglêdniaj¹c sortowanie
    const fetchTasks = useCallback((sortBy, sortOrder) => {
        apiHandler.ProjectTasks.projectTaskListInTeam(Number(teamId), sortBy, sortOrder)
            .then(response => setTaskList(response));
    }, [teamId]);

    useEffect(() => {
        fetchTasks('ID', 'asc');
    }, [fetchTasks]);

    useEffect(() => {
        const activeSortField = Object.keys(sortIcons).find((key) => sortIcons[key] !== 'default');
        console.log(activeSortField);
        if (activeSortField) {
            fetchTasks(activeSortField, sortIcons[activeSortField]);
        }
    }, [sortIcons, fetchTasks]);

    return (
        <div className="table-container">
            <div className="checkbox-input">
            <input
                checked={currentSprintMark}
                type="checkbox"
                onChange={(e) => { setCurrentSprintMark(e.target.checked) }}
            />
                <span>Mark task for current sprint</span>
            </div>
            <table className="task-table">
                <thead>
                    <tr>
                        {tableHeaders.map(header => (
                            <th key={header} className="header-cell" onClick={() => toggleSortIcon(header)}>
                                {header}{getSortIcon(sortIcons[header])}
                            </th>
                        ))}
                    </tr>
                </thead>
                <tbody>
                    {taskList && taskList.map((t) => (
                        <tr key={t.taskId} className={`task-row ${currentSprintMark && new Date(t.sprint.sprintStart) <= new Date() &&
                                new Date(t.sprint.sprintEnd) >= new Date() ? 'highlight-row' : 'fade-out'
                            }`}>
                            <td className="task-cell">{t.taskId}</td>
                            <Link to={"/tasks/" + t.taskId} className="row-link"><td className="task-cell">{t.taskName}</td></Link>
                            <td className="task-cell">{new Date(t.taskDeadline).toLocaleDateString('en-EN', dateOptions)}</td>
                            {tableHeaders.includes("Team") && <td className="task-cell">{t.team.teamName}</td>}
                            <td className="task-cell">{t.taskStatus.statusName}</td>
                            <td className="task-cell">{t.createdByNavigation.username}</td>
                            <td className="task-cell">{t.usersTasks.map(ut => ut.user.username + " ")}</td>
                            <td className="task-cell">{t.taskType.typeName}</td>
                            <Link to="" className="row-link"><td className="task-cell">{t.sprint.sprintName}</td></Link>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
}

export default TaskTable;
