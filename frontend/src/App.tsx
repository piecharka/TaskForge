
import { Route, Routes } from 'react-router-dom';
import TeamView from './UI/TeamView';

function App() {
  return (
          <Routes>
              <Route path="/" element={ <div></div>} />
              <Route path="/team/:teamId" element={ <TeamView />} />
          </Routes> 
  )
}

export default App
