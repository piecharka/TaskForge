import { observer } from "mobx-react-lite";
import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import apiHandler from "../api/apiHandler";
import { useStore } from "../stores/store";
import { ProjectTask } from "../models/projectTask";


const DashboardView = observer(() => {
    const [todoTasks, setTodoTasks] = useState<ProjectTask[]>([]);
    const { userStore } = useStore();

    useEffect(() => {
        console.log(userStore.user?.username);
        apiHandler.ProjectTasks.todoTasks(userStore.user?.username)
            .then(response => {
                console.log(response);
                setTodoTasks(response)
            });
        
      }, [userStore])

    return (
        <div>
            {todoTasks && todoTasks.map(t => (<div>{ t.taskName}</div>)) }
            <Link to="tasks/team/0">link</Link>
        </div>
    );
});

export default DashboardView;