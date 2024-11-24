import { useEffect, useState } from "react";
import DatePicker from "react-datepicker";
import "../style/NewTaskForm.css"
import apiHandler from "../api/apiHandler";
import { useStore } from "../stores/store";
import { User } from "../models/user";
import { useParams } from "react-router-dom";
import Select from "react-select";
import { ProjectTaskType } from "../models/projectTaskType";
import { Sprint } from "../models/sprint";


function NewTaskForm() {
    const [taskName, setTaskName] = useState('');
    const [description, setDescription] = useState('');
    const [deadline, setDeadline] = useState<Date>(new Date());
    const [checkedUsersIds, setCheckedUsersIds] = useState<number[]>([]);
    const [taskTypeId, setTaskTypeId] = useState<number>(0);
    const [sprintId, setSprintId] = useState<number>(0);
    const [usersInTeam, setUsersInTeam] = useState<User[]>([]);
    const [taskTypesList, setTaskTypesList] = useState<ProjectTaskType[]>([]);
    const [sprintList, setSprintList] = useState<Sprint[]>([]);
    const [error, setError] = useState<string | null>(null);
    const { userStore } = useStore();
    const { teamId } = useParams<{ teamId: string }>();

    const customSelectStyles = {
        control: (provided) => ({
            ...provided,
            minHeight: '2rem',
            borderColor: '#ccc',
            boxShadow: 'none',
            boxShadow: 'none', // usuwa automatyczny cieñ przegl¹darki
            outline: 'none',
            '&:hover': {
                borderColor: '#999'
            }
        }),
        menu: (provided) => ({
            ...provided,
            width: '20rem'
        }),
        multiValue: (provided) => ({
            ...provided,
            backgroundColor: '#f1f1f1'
        }),
        multiValueLabel: (provided) => ({
            ...provided,
            color: '#333'
        }),
        multiValueRemove: (provided) => ({
            ...provided,
            color: '#999',
            ':hover': {
                backgroundColor: '#e0e0e0',
                color: '#333'
            }
        })
    };

    const userOptions = usersInTeam.map(user => ({
        value: user.userId,
        label: user.username
    }));

    const taskTypesOptions = taskTypesList.map(type => ({
        value: type.typeId,
        label: type.typeName,
    }));

    const sprintOptions = sprintList.map(sprint => ({
        value: sprint.sprintId,
        label: sprint.sprintName,
    }));

    useEffect(() => {
        apiHandler.Users.teamUsers(Number(teamId)).then(response => {
            setUsersInTeam(response);
        });

        apiHandler.ProjectTaskTypes.projectTaskTypesList()
            .then(response => setTaskTypesList(response));

        apiHandler.Sprints.getTeamsSprints(Number(teamId))
            .then(response => setSprintList(response));


    }, [teamId])

    const handleUserDropdown = (selectedOptions) => {
        setCheckedUsersIds(selectedOptions.map(option => option.value));
    };


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
                taskTypeId: taskTypeId,
                sprintId: sprintId,
                userIds: checkedUsersIds,
                teamId: Number(teamId),

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
                  <Select
                      options={userOptions}
                      isMulti
                      isClearable
                      onChange={handleUserDropdown}
                      placeholder="Select users"
                      styles={customSelectStyles }
                  />
                  {/*<select id="users" name="users" onChange={handleUserDropdown} defaultValue="">*/}
                  {/*    {checkedUsersIds.map(id => {*/}
                  {/*        const user = usersInTeam.find(u => u.userId === id);*/}
                  {/*        return <span key={user.userId}>{user.username}</span>*/}
                  {/*    })}*/}
                  {/*    {usersInTeam.map(u => {*/}
                  {/*        return (<option key={u.userId} value={u.userId}>{u.username}</option>)*/}
                  {/*    }) }*/}
                  {/*</select>*/}
              </div>

              <div className="task-input">
                  <label>Types to task</label>
                  <Select
                      options={taskTypesOptions}
                      onChange={(opt) => setTaskTypeId(opt.value)}
                      //onChange={handleTypeDropdown}
                      placeholder="Select task type"
                      //styles={customSelectStyles}
                  />
              </div>
              <div className="task-input">
                  <label>Sprints</label>
                  <Select
                      options={sprintOptions}
                      onChange={opt => setSprintId(opt.value)}
                      placeholder="Select sprint"
                  styles={customSelectStyles}
                  />
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