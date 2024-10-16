
import '../style/TaskTable.css';
import { ProjectTask } from '../models/projectTask';

function TaskTable({ taskList, tableHeaders }: {taskList: ProjectTask[], tableHeaders : string[]}) {
  return (
    <div className="table-container">
      <table className="task-table">
        <thead>
          <tr>
             {tableHeaders.map(h => <th className="header-cell">{ h }</th>)}
          </tr>
        </thead>
        <tbody>
          {taskList.map((t) => (
            <tr key={t.taskId} className="task-row">
              <td className="task-cell">{t.taskId}</td>
              <td className="task-cell">{t.taskName}</td>
              <td className="task-cell">{t.taskDeadline}</td>
              {tableHeaders.includes("Team") && <td className="task-cell">{t.team.teamName}</td>}
              <td className="task-cell">{t.taskStatus.statusName}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default TaskTable;
