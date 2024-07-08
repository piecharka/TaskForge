
import { useEffect, useState } from 'react'
import apiHandler from './api/apiHandler';
import { Team } from './models/team';

function App() {
    const [teams, setTeams] = useState<Team[]>([]);
    useEffect(() => {
        apiHandler.Teams.list().then(response => {
            setTeams(response);
        })
    }, [])
  return (
      <>
          {teams.map(team => (
              <div key={team.teamId}>
                  <h2>{team.teamName}</h2>
              </div>
          ))}      
    </>
  )
}

export default App
