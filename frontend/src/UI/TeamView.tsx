import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import apiHandler from "../api/apiHandler";
import { Team } from "../models/team";

function TeamView() {
    const { teamId } = useParams<{ teamId: string }>();
    const [team, setTeam] = useState<Team | null>(null);

    useEffect(() => {
        apiHandler.Teams.first(Number(teamId)).then(response => {
            setTeam(response);
        });
    }, [teamId])

    if (team === null) {
        return <div>Loading...</div>;
    }

  return (
      <div>
          { team.teamName}
      </div>
  );
}

export default TeamView;