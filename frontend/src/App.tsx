
import { Route, Routes } from 'react-router-dom';
import TeamView from './views/TeamView';
import ProjectTaskView from './views/ProjectTaskView';
import "./App.css"
import Navbar from './components/Navbar';

function App() {
    return (
        <div>
            <Navbar />
            <div className="main">
            <Routes>
                <Route path="/" element={<div >
                </div>} />
                  <Route path="/team/:teamId" element={ <TeamView />} />
                  <Route path="tasks/team/:teamId" element={<ProjectTaskView /> } />
             </Routes> 
            </div>
        </div>
  )
}

export default App;