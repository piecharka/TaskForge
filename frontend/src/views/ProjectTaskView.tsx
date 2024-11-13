import { useEffect, useState } from "react";
import apiHandler from "../api/apiHandler";
import { useNavigate, useParams } from "react-router-dom";
import { ProjectTask } from "../models/projectTask";
import { Comment } from "../models/comment";
import "../style/TaskDetails.css"
import CommentBar from "../components/CommentBar";
import AddCommentBar from "../components/AddCommentBar";

function ProjectTaskView() {
    const [task, setTask] = useState<ProjectTask>();
    const [comments, setComments] = useState<Comment[]>();
    const { taskId } = useParams<{ taskId: string }>();
    const navigate = useNavigate()

    useEffect(() => {
        apiHandler.ProjectTasks.getTask(Number(taskId))
            .then(response => {
                setTask(response)
                console.log(response)
            })

        apiHandler.Comments.taskComments(Number(taskId))
            .then(response => setComments(response));

    }, [taskId])

    const deleteButtonHandle = () => {
        apiHandler.ProjectTasks.deleteTask(Number(taskId));
        navigate(-1);
    }

    return (
      <div>
            <button onClick={deleteButtonHandle}> Delete task</button>
              {task && 
                  <div className="">
                    <h1>{task.taskName}</h1>
                    <span>{new Date(task.taskDeadline).toLocaleString("pl-PL", {
                        year: "numeric",
                        month: "2-digit",
                        day: "2-digit",
                        hour: "2-digit",
                        minute: "2-digit",
                        second: "2-digit"
                    })}</span>
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