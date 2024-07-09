import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import apiHandler from "../api/apiHandler";
import { Team } from "../models/team";
import { User } from "../models/user";

function TeamView() {
    const { teamId } = useParams<{ teamId: string }>();
    const [team, setTeam] = useState<Team | null>(null);
    const [usersInTeam, setUsersInTeam] = useState<User[]> ([]);

    useEffect(() => {
        apiHandler.Teams.first(Number(teamId)).then(response => {
            setTeam(response);
        });
        apiHandler.Users.teamUsers(Number(teamId)).then(response => {
            setUsersInTeam(response);
        })

    }, [teamId])

    if (team === null) {
        return <div>Loading...</div>;
    }

  return (
      <div>
          {team.teamName}
          {usersInTeam.map(u => (
              <p key={u.userId }>{u.username}</p>))}
      </div>
  );
}

export default TeamView;