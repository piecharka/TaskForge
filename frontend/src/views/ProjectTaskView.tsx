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

function ProjectTaskView() {
    const [task, setTask] = useState<ProjectTask>();
    const [comments, setComments] = useState<Comment[]>();
    const [usersAssigned, setUsersAssigned] = useState<User[]>([]);
    const [teamUsers, setTeamUsers] = useState<User[]>([]);
    const [statuses, setStatuses] = useState<ProjectTaskStatus[]>([]);
    const { taskId } = useParams<{ taskId: string }>();
    const navigate = useNavigate()

    const selectRef = useRef<any>(null);

    const fetchUsersAndUpdateLists = useCallback(async () => {
        const assignedUsers = await apiHandler.ProjectTasks.getUsersAssigned(Number(taskId));
        setUsersAssigned(assignedUsers);

        const taskDetails = await apiHandler.ProjectTasks.getTask(Number(taskId));
        setTask(taskDetails);

        const allTeamUsers = await apiHandler.Users.teamUsers(taskDetails.teamId);
        const filteredUsers = allTeamUsers.filter(
            user => !assignedUsers.some(assigned => assigned.userId === user.userId)
        );
        setTeamUsers(filteredUsers);
    }, [taskId]);

    useEffect(() => {
        fetchUsersAndUpdateLists();

        apiHandler.Comments.taskComments(Number(taskId))
            .then(response => setComments(response));

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

    const handleUserDropdown = async (selectedOption: { value: number; label: string }) => {
        if (selectedOption) {
            await apiHandler.ProjectTasks.assignUserToTask({
                taskId: Number(taskId),
                userIds: [selectedOption.value],
            });

            await fetchUsersAndUpdateLists();
            if (selectRef.current) {
                selectRef.current.clearValue();
            }
        }
    };

    const handleStatusDropdown = async (selectedOption: { value: number; label: string }) => {
        if (selectedOption) {
            await apiHandler.ProjectTasks.updateTaskStatus(Number(taskId), selectedOption.value);

            await fetchUsersAndUpdateLists();
        }
    };

    return (
      <div>
            <button onClick={deleteButtonHandle}> Delete task</button>
              {task && 
                  <div className="">
                    <h1>{task.taskName}</h1>
                    <div>
                        {usersAssigned.map(u => (<span>{u.username}</span>))}
                    </div>
                    <Select
                        ref={selectRef}
                        options={userOptions}
                        onChange={handleUserDropdown}
                        placeholder="Select users"
                    />
                    <Select
                        options={statusOptions}
                        onChange={handleStatusDropdown}
                        placeholder="Select status"
                    />
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
                       
                  </div>
              }
            
            {comments && comments.map(c => (
             <div>
                <CommentBar comment={c} />
            </div>
            ))}
            <AddCommentBar />
      </div>
  );
}

export default ProjectTaskView;
