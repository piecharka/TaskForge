import { observer } from "mobx-react-lite";
import { Link } from "react-router-dom";


const DashboardView = observer(() => {
    //  const [todoTasks, setTodoTasks] = useState<ProjectTask[]>([]);

    //  useEffect(() => {
    //      apiHandler.ProjectTasks.todoTasks()
    //  }, [])

    return (
        <div>
            <Link to="team/0">link</Link>
        </div>
    );
});

export default DashboardView;