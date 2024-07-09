
import { Route, Routes } from 'react-router-dom';
import TeamView from './UI/TeamView';
import ProjectTaskView from './UI/ProjectTaskView';

function App() {
  return (
          <Routes>
              <Route path="/" element={ <div></div>} />
              <Route path="/team/:teamId" element={ <TeamView />} />
              <Route path="tasks/team/:teamId" element={<ProjectTaskView /> } />
          </Routes> 
  )
}

export default App
