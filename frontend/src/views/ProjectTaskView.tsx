import { useEffect, useState } from "react";
import apiHandler from "../api/apiHandler";
import { useParams } from "react-router-dom";
import { ProjectTask } from "../models/projectTask";
import { Comment } from "../models/comment";

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
          {task && 
              <div>
                  <h1>{task.taskName}</h1>
                  <span>{task.taskDeadline}</span>
                  <p>{task.taskDescription}</p>
              </div>
          }

          {comments && comments.map(c => (<div>
              <p>{c.commentText }</p>
          </div>
          ))
        }
      </div>
  );
}

export default ProjectTaskView;