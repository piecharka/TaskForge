
import { Route, Routes } from 'react-router-dom';
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
import UserDetailsView from './views/UserDetailsView';

const App = observer(() => {
    const { userStore } = useStore();
    const link = document.createElement('link');
    link.href = 'https://fonts.googleapis.com/css2?family=Inter:ital,opsz,wght@0,14..32,100..900;1,14..32,100..900&display=swap';
    link.rel = 'stylesheet';
    document.head.appendChild(link);

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
                    <Route path="/users/:userId" element={<AuthCheck><UserDetailsView /> </AuthCheck>} />
                    
                </Routes>
            </div>
        </div>
    )
});

export default App;