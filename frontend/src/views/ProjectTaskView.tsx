import { useEffect, useState } from "react";
import apiHandler from "../api/apiHandler";
import { useParams } from "react-router-dom";
import { ProjectTask } from "../models/projectTask";
import { Comment } from "../models/comment";
import "../style/TaskDetails.css"
import CommentBar from "../components/CommentBar";
import AddCommentBar from "../components/AddCommentBar";

function ProjectTaskView() {
    const [task, setTask] = useState<ProjectTask>();
    const [comments, setComments] = useState<Comment[]>();
    const { taskId } = useParams<{taskId: string}>();

    useEffect(() => {
        apiHandler.ProjectTasks.getTask(Number(taskId))
            .then(response => {
                setTask(response)
                console.log(response)
            })

        apiHandler.Comments.taskComments(Number(taskId))
            .then(response => setComments(response));

    }, [taskId])

    return (
      <div>
            <div className="task-card">
              <AddCommentBar />

              {task && 
                  <div>
                      <h1>{task.taskName}</h1>
                      <span>{task.taskDeadline}</span>
                      <p>{task.taskDescription}</p>
                  </div>
              }

            </div>
            {comments && comments.map(c => (<div>
                <CommentBar comment={c} />
            </div>
            ))}
      </div>
  );
}

export default ProjectTaskView;