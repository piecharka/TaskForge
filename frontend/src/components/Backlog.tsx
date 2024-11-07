import { Link } from "react-router-dom";
import { ProjectTask } from "../models/projectTask";
import "../style/Backlog.css";

function Backlog({ taskList}: { taskList: ProjectTask[]}) {
    return (
        <div className="backlog">
              <div className="task-list">
                  <h2>To-do tasks</h2>
                {taskList.map(t => {
                    if (t.taskStatus.statusId === 1)
                      return <Link key={t.taskId} to={"/tasks/" + t.taskId} className="task">
                          <p>{t.taskName}</p>
                          <span>{t.taskType.typeName}</span>
                      </Link>
                  }) }
            </div>
              <div className="task-list">
                <h2>In progress tasks</h2>
                {taskList.map(t => {
                    if (t.taskStatus.statusId === 3)
                      return <Link key={ t.taskId} to={"/tasks/" + t.taskId} className="task">
                          <p>{t.taskName}</p>
                          <span>{t.taskType.typeName}</span>
                      </Link>
                  })}
            </div>
              <div className="task-list">
                <h2>Done tasks</h2>
                  {taskList.map(t => {
                      if (t.taskStatus.statusId === 2)
                          return <Link key={t.taskId} to={"/tasks/" + t.taskId} className="task">
                              <p>{t.taskName}</p>
                              <span>{t.taskType.typeName}</span>
                          </Link>
                  })}
            </div>
            <div className="space">
            </div>
        </div>

  );
}

export default Backlog;