import { useCallback, useEffect, useRef, useState } from "react";
import apiHandler from "../api/apiHandler";
import { useNavigate, useParams } from "react-router-dom";
import { ProjectTask } from "../models/projectTask";
import { Comment } from "../models/comment";
import "../style/TaskDetails.css"
import CommentBar from "../components/CommentBar";
import AddCommentBar from "../components/AddCommentBar";
import { User } from "../models/user";
import Select from "react-select";
import { ProjectTaskStatus } from "../models/projectTaskStatus";
import UploadAttachment from "../components/UploadAttachment";
import TaskAttachments from "../components/TaskAttachments";

function ProjectTaskView() {
    const [task, setTask] = useState<ProjectTask>();
    const [usersAssigned, setUsersAssigned] = useState<User[]>([]);
    const [teamUsers, setTeamUsers] = useState<User[]>([]);
    const [statuses, setStatuses] = useState<ProjectTaskStatus[]>([]);
    const [statusChange, setStatusChange] = useState<boolean>(false); 
    const [assigning, setAssigning] = useState<boolean>(false);
    const [selectedStatus, setSelectedStatus] = useState<{ value: number; label: string } | null>(null);
    const [selectedUsers, setSelectedUsers] = useState<{ value: number; label: string }[]>([]);
    const { taskId } = useParams<{ taskId: string }>();
    const navigate = useNavigate()

    const selectRef = useRef<any>(null);

    const fetchUsersAndUpdateLists = useCallback(async () => {
        const assignedUsers = await apiHandler.ProjectTasks.getUsersAssigned(Number(taskId));
        setUsersAssigned(assignedUsers);

        setSelectedUsers(assignedUsers.map(user => ({
            value: user.userId,
            label: user.username
        })));

        const taskDetails = await apiHandler.ProjectTasks.getTask(Number(taskId));
        setTask(taskDetails);

        setSelectedStatus({
            value: taskDetails.taskStatus.statusId,
            label: taskDetails.taskStatus.statusName
        });

        const allTeamUsers = await apiHandler.Users.teamUsers(taskDetails.teamId);
        const filteredUsers = allTeamUsers.filter(
            user => !assignedUsers.some(assigned => assigned.userId === user.userId)
        );
        setTeamUsers(filteredUsers);
    }, [taskId]);

    useEffect(() => {
        fetchUsersAndUpdateLists();

        apiHandler.ProjectTaskStatuses.projectTaskStatusesList()
            .then(response => setStatuses(response));

    }, [taskId, fetchUsersAndUpdateLists]);

    const deleteButtonHandle = () => {
        apiHandler.ProjectTasks.deleteTask(Number(taskId));
        navigate(-1);
    }

    const userOptions = teamUsers.map((user) => ({
        value: user.userId,
        label: user.username
    }))

    const statusOptions = statuses.map(status => ({
        value: status.statusId,
        label: status.statusName
    })) 

    const handleUserDropdown = async (selectedOptions: { value: number; label: string }[]) => {
        const newUserIds = selectedOptions.map(option => option.value);
        const removedUser = selectedUsers.find(user => !newUserIds.includes(user.value));

        // Dodanie nowego u¿ytkownika (jeœli s¹ ró¿nice)
        if (newUserIds.length > selectedUsers.length) {
            const addedUser = newUserIds.find(id => !selectedUsers.some(user => user.value === id));
            if (addedUser) {
                await apiHandler.ProjectTasks.assignUserToTask({
                    taskId: Number(taskId),
                    userIds: [addedUser]
                });
            }
        }

        // Usuniêcie pojedynczego u¿ytkownika
        if (removedUser) {
            await apiHandler.ProjectTasks.deleteUserFromTask(Number(taskId), removedUser.value);
        }

        // Aktualizacja stanu
        setSelectedUsers(selectedOptions);
        await fetchUsersAndUpdateLists();
    };

    const handleStatusDropdown = async (selectedOption: { value: number; label: string }) => {
        if (selectedOption) {
            await apiHandler.ProjectTasks.updateTaskStatus(Number(taskId), selectedOption.value);

            await fetchUsersAndUpdateLists();
        }
    };

    return (
      <div>
              {task && 
                  <div className="">
                    <div className="task-title">
                        <h1>{task.taskName}</h1>
                        <button className="btn status-btn" onClick={() => setStatusChange(flag => !flag)}>Change status</button>
                        <button className="btn assign-btn" onClick={() => setAssigning(flag => !flag) }>Change assignees</button>
                        <button className="btn delete-btn" onClick={deleteButtonHandle}> Delete task</button>
                    </div>
                    <div>
                        {usersAssigned.map(u => (<span>{u.username}</span>))}
                    </div>
                    {assigning && <Select
                        options={userOptions}
                        isMulti
                        isClearable
                        value={selectedUsers} // Wybrane wartoœci
                        onChange={handleUserDropdown}
                        placeholder="Select users"
                    />}
                    {statusChange && <Select
                        options={statusOptions}
                        onChange={handleStatusDropdown}
                        value={selectedStatus}
                    />}
                    <span>{new Date(task.taskDeadline).toLocaleString("pl-PL", {
                        year: "numeric",
                        month: "2-digit",
                        day: "2-digit",
                        hour: "2-digit",
                        minute: "2-digit",
                        second: "2-digit"
                    })}</span>
                    <p>{task.taskStatus.statusName}</p>
                    <p>{task.taskDescription}</p>
                    <TaskAttachments taskId={Number(taskId)} />
                    <UploadAttachment taskId={Number(taskId)} />
                  </div>
              }
                <CommentBar/>
      </div>
  );
}

export default ProjectTaskView;
