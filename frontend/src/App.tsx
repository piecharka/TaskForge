
import { Navigate, Route, Routes } from 'react-router-dom';
import TeamView from './views/TeamView';
import "./style/App.css"
import Navbar from './components/Navbar';
import LoginFormView from './views/LoginFormView';
import { useStore } from './stores/store';
import { observer } from 'mobx-react-lite';
import DashboardView from './views/DashboardView';
import TeamListView from './views/TeamListView';
import RegisterFormView from './views/RegisterFormView';
import ProjectTaskView from './views/ProjectTaskView';
import ProjectTaskListView from './views/ProjectTaskListView';
import AuthCheck from './components/AuthCheck';

const App = observer(() => {
    const { userStore } = useStore();

    return (
        <div>
            {userStore.isLoggedIn && <Navbar />} 
            <div className="main">
                <Routes>
                    <Route path="/" element={<AuthCheck> <DashboardView /> </AuthCheck>} />
                    <Route path="/login" element={<LoginFormView />} />
                    <Route path="/register" element={<RegisterFormView />} />
                    <Route path="/team" element={<AuthCheck><TeamListView /></AuthCheck> } />
                    <Route path="/team/:teamId" element={<AuthCheck><TeamView /></AuthCheck>} />
                    <Route path="/tasks/team/:teamId" element={<AuthCheck><ProjectTaskListView /> </AuthCheck>} />
                    <Route path="/tasks/:taskId" element={<AuthCheck><ProjectTaskView /> </AuthCheck>} />
                </Routes>
            </div>
        </div>
    )
});

export default App;