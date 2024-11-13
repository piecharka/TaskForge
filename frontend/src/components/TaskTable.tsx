import '../style/TaskTable.css';
import { ProjectTask } from '../models/projectTask';
import { useCallback, useEffect, useState } from 'react';
import apiHandler from '../api/apiHandler';
import { useParams } from 'react-router-dom';
import { LuArrowUpDown } from "react-icons/lu";
import { RiArrowDownLine, RiArrowUpLine } from "react-icons/ri";

function TaskTable() {
    const [taskList, setTaskList] = useState<ProjectTask[]>();
    const tableHeaders = ["ID", "Title", "Deadline", "Status", "Created by", "Attached to", "Type"];
    const [sortIcons, setSortIcons] = useState(
        tableHeaders.reduce((acc, header) => ({ ...acc, [header]: 'default' }), {})
    );
    const { teamId } = useParams();

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

    // UseEffect - wywo³anie na pocz¹tku i po zmianie teamId
    useEffect(() => {
        // Na pocz¹tku domyœlnie sortowanie po ID rosn¹co
        fetchTasks('ID', 'asc');
    }, [fetchTasks]);

    // U¿ycie useEffect do za³adowania danych po ka¿dej zmianie sortowania
    useEffect(() => {
        const activeSortField = Object.keys(sortIcons).find((key) => sortIcons[key] !== 'default');
        console.log(activeSortField);
        if (activeSortField) {
            fetchTasks(activeSortField, sortIcons[activeSortField]);
        }
    }, [sortIcons, fetchTasks]);

    return (
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
                </thead>
                <tbody>
                    {taskList && taskList.map((t) => (
                        <tr key={t.taskId} className="task-row">
                            <td className="task-cell">{t.taskId}</td>
                            <td className="task-cell">{t.taskName}</td>
                            <td className="task-cell">{t.taskDeadline}</td>
                            {tableHeaders.includes("Team") && <td className="task-cell">{t.team.teamName}</td>}
                            <td className="task-cell">{t.taskStatus.statusName}</td>
                            <td className="task-cell">{t.createdByNavigation.username}</td>
                            <td className="task-cell">{t.usersTasks.map(ut => ut.user.username + " ")}</td>
                            <td className="task-cell">{t.taskType.typeName}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
}

export default TaskTable;
