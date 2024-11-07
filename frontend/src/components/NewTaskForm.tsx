import { useEffect, useState } from "react";
import DatePicker from "react-datepicker";
import "../style/NewTaskForm.css"
import apiHandler from "../api/apiHandler";
import { useStore } from "../stores/store";
import { User } from "../models/user";
import { useParams } from "react-router-dom";

function NewTaskForm() {
    const [taskName, setTaskName] = useState('');
    const [description, setDescription] = useState('');
    const [deadline, setDeadline] = useState<Date>(new Date());
    const [checkedUsersIds, setCheckedUsersIds] = useState<number[]>([]);
    const [usersInTeam, setUsersInTeam] = useState<User[]>([]);
    const [error, setError] = useState<string | null>(null);
    const { userStore } = useStore();
    const { teamId } = useParams<{ teamId: string }>();

    useEffect(() => {
        apiHandler.Users.teamUsers(Number(teamId)).then(response => {
            setUsersInTeam(response);
        });
    }, [teamId])

    const handleUserDropdown = (e: React.ChangeEvent<HTMLSelectElement>) => {
        const userId = Number(e.target.value);

        if (userId && !checkedUsersIds.includes(userId)) {
            setCheckedUsersIds((prevCheckedUsersIds) => [...prevCheckedUsersIds, userId]);
        }
    }

    const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        console.log(userStore.user?.userId);
        if (userStore.user?.userId != null) {
            apiHandler.ProjectTasks.postTask({
                taskName: taskName,
                taskStatusId: 1,
                taskDeadline: deadline,
                taskDescription: description,
                createdBy: userStore.user?.userId,
                taskTypeId: 1,
                sprintId: 2,
                userIds: checkedUsersIds,
                teamId: 0,

            })
                .then(() => {
                    setError('');

                }).catch((error: any) => {
                    if (error.response && error.response.data && error.response.data.message) {
                        setError(error.response.data.message);
                    } else if (error.message) {
                        setError(error.message);
                    } else {
                        setError('An unexpected error occurred');
                    }
                });

        }
    }

  return (
    <div className="form-position">
          <form onSubmit={handleSubmit} className="task-form">
            <div className="task-input">
                <label>Task name</label>
                <input type="text" id="taskName" value={taskName} onChange={(e) => setTaskName(e.target.value)} />
            </div>
            <div className="task-input">
                <label>Task deadline</label>
                 <DatePicker selected={deadline} onChange={(date: Date|null) => setDeadline(date) } />
            </div>
            <div className="task-input">
                <label>Task description</label>
                 <input type="text" id="taskDescription" value={description} onChange={(e) => setDescription(e.target.value)} />
              </div>
              <div className="task-input">
                  <label>Users to task</label>
                  <select id="users" name="users" onChange={handleUserDropdown} defaultValue="">
                      {usersInTeam.map(u => {
                          return (<option key={u.userId} value={u.userId}>{u.username}</option>)
                      }) }
                  </select>
              </div>
              <div>
                  <h3>Checked users</h3>
                <ul>
                      {checkedUsersIds.map(id => {
                          const user = usersInTeam.find(u => u.userId === id);
                          return <li key={user.userId}>{user.username}</li>
                      })}
                  </ul>
              </div>
              {/*<div>*/}
              {/*    <label>Task type</label>*/}
              {/*    <input type="text" id="taskType" value={taskName} onChange={(e) => setTaskName(e.target.value)} />*/}
              {/*</div>*/}
              {error && <div className="error">{error}</div>} {/* Wyœwietl komunikat b³êdu */}
              <button className="submit-button" type="submit">Submit</button>
        </form>
    </div>
  );
}

export default NewTaskForm;