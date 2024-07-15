
import { Navigate, Route, Routes } from 'react-router-dom';
import TeamView from './views/TeamView';
import ProjectTaskView from './views/ProjectTaskView';
import "./App.css"
import Navbar from './components/Navbar';
import LoginFormView from './views/LoginFormView';
import { useStore } from './stores/store';
import { observer } from 'mobx-react-lite';
import DashboardView from './views/DashboardView';

const App = observer(() => {
    const { userStore } = useStore();

    return (
        <div>
            <Navbar />
            <div className="main">
                <Routes>
                    <Route path="/" element={ <DashboardView />} />
                    <Route path="/login" element={<LoginFormView />} />
                    <Route path="/team/:teamId" element={userStore.isLoggedIn ? <TeamView /> : <Navigate to="/login" />} />
                    <Route path="tasks/team/:teamId" element={<ProjectTaskView />} />
                </Routes>
            </div>
        </div>
    )
});

export default App;